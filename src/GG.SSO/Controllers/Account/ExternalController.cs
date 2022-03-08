using Duende.IdentityServer;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using GG.SSO.DataBaseBusiness.Business;
using GG.SSO.BusinesLogic.Helpers;
using GG.SSO.BusinesLogic.Identity;
using GG.SSO.BusinesLogic.IdentityServer;
using GG.SSO.BusinesLogic.Model.Identity;
using GG.SSO.Entity.Table.Sso;
using GG.SSO.Filters;
using GG.SSO.Helpers;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GG.SSO.Controllers.Account
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class ExternalController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly ILogger<ExternalController> _logger;
        private readonly IEventService _events;
        private readonly ClientsBasicManagement _clientsManagement;
        private readonly UserManagement _userManagement;
        private readonly UserClaimsBusiness _userClaimsBusiness;

        public ExternalController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IEventService events,
            ILogger<ExternalController> logger, ClientsBasicManagement clientsManagement,
            UserManagement userManagement, UserClaimsBusiness userClaimsBusiness
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
            _clientStore = clientStore;
            _logger = logger;
            _events = events;
            _clientsManagement = clientsManagement;
            _userManagement = userManagement;
            _userClaimsBusiness = userClaimsBusiness;
    }

        /// <summary>
        /// initiate roundtrip to external authentication provider
        /// </summary>
        [HttpGet]
        public IActionResult Challenge(string scheme, string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl)) returnUrl = "~/";

            // validate returnUrl - either it is a valid OIDC URL or back to a local page
            if (Url.IsLocalUrl(returnUrl) == false && _interaction.IsValidReturnUrl(returnUrl) == false)
            {
                // user might have clicked on a malicious link - should be logged
                throw new Exception("invalid return URL");
            }

            // start challenge and roundtrip the return URL and scheme 
            var props = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(Callback)),
                Items =
                {
                    { "returnUrl", returnUrl },
                    { "scheme", scheme },
                }
            };

            return Challenge(props, scheme);

        }

        /// <summary>
        /// Post processing of external authentication
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Callback()
        {
            // read external identity from the temporary cookie
            var result = await HttpContext.AuthenticateAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);

            if (result?.Succeeded != true)
            {
                throw new Exception("External authentication error");
            }

            // retrieve return URL
            var returnUrl = result.Properties.Items["returnUrl"] ?? "~/";
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);

            if (context == null || context.Client == null)
            {
                throw new Exception("External authentication error");
            }

            if (_logger.IsEnabled(LogLevel.Debug))
            {
                var externalClaims = result.Principal.Claims.Select(c => $"{c.Type}: {c.Value}");
                _logger.LogDebug("External claims: {@claims}", externalClaims);
            }

            // lookup our user and external provider info
            var (user, provider, providerUserId, claims) = await FindUserFromExternalProviderAsync(result);
            var client = _clientsManagement.Get(context.Client.ClientId);

            if (user == null)
            {
                // this might be where you might initiate a custom workflow for user registration
                // in this sample we don't show how that would be done, as our sample implementation
                // simply auto-provisions new external user
                user = await AutoProvisionUserAsync(provider, providerUserId, claims, client);
            }
            else
            { 
                var userClaims = _userClaimsBusiness.Get(user.User.Id);
                var validClaims = GetValidClaims(claims);

                foreach (var item in userClaims)
                {
                    var newClaim = validClaims.FirstOrDefault(x => item.Type ==x.Type);

                    if (newClaim == null)
                    { 
                        await _userManager.AddClaimAsync(user, Converts.ToClaim(item));
                    }
                    else if(item.Value != newClaim.Value)
                    {
                        await _userManager.ReplaceClaimAsync(user, Converts.ToClaim(item), newClaim);
                    }
                }
            }

            if (!user.Clients.Any(x => x.ClientId == context.Client.ClientId))
            {
                _userManagement.UpdateClientUser(user, context.Client.ClientId);
            }

            // this allows us to collect any additional claims or properties
            // for the specific protocols used and store them in the local auth cookie.
            // this is typically used to store data needed for signout from those protocols.
            var additionalLocalClaims = new List<Claim>();
            var localSignInProps = new AuthenticationProperties();
            ProcessLoginCallback(result, additionalLocalClaims, localSignInProps);

            // issue authentication cookie for user
            // we must issue the cookie maually, and can't use the SignInManager because
            // it doesn't expose an API to issue additional claims from the login workflow
            var principal = await _signInManager.CreateUserPrincipalAsync(user);
            additionalLocalClaims.AddRange(principal.Claims);
            var name = principal.FindFirst(JwtClaimTypes.Name)?.Value ?? user.User.Id;

            var isuser = new IdentityServerUser(user.User.Id)
            {
                DisplayName = name,
                IdentityProvider = provider,
                AdditionalClaims = additionalLocalClaims
            };

            await HttpContext.SignInAsync(isuser, localSignInProps);

            // delete temporary cookie used during external authentication
            await HttpContext.SignOutAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);            

            // check if external login is in the context of an OIDC request            
            await _events.RaiseAsync(new UserLoginSuccessEvent(provider, providerUserId, user.User.Id, name, true, context?.Client.ClientId));

            if (context != null)
            {
                if (context.IsNativeClient())
                {
                    // The client is native, so this change in how to
                    // return the response is for better UX for the end user.
                    return this.LoadingPage("Redirect", returnUrl);
                }
            }

            return Redirect(returnUrl);
        }

        private async Task<(ApplicationUser user, string provider, string providerUserId, IEnumerable<Claim> claims)>
            FindUserFromExternalProviderAsync(AuthenticateResult result)
        {
            var externalUser = result.Principal;

            // try to determine the unique id of the external user (issued by the provider)
            // the most common claim type for that are the sub claim and the NameIdentifier
            // depending on the external provider, some other claim type might be used
            var userIdClaim = externalUser.FindFirst(JwtClaimTypes.Subject) ??
                              externalUser.FindFirst(ClaimTypes.NameIdentifier) ??
                              throw new Exception("Unknown userid");

            // remove the user id claim so we don't include it as an extra claim if/when we provision the user
            var claims = externalUser.Claims.ToList();
            claims.Remove(userIdClaim);

            var provider = result.Properties.Items["scheme"];
            var providerUserId = userIdClaim.Value;

            // find external user
            var user = await _userManager.FindByLoginAsync(provider, providerUserId);

            return (user, provider, providerUserId, claims);
        }

        private async Task<ApplicationUser> AutoProvisionUserAsync(string provider, string providerUserId, IEnumerable<Claim> claims, ClientsBasic clientsBasic)
        {
            // create a list of claims that we want to transfer into our store
            var filtered = new List<Claim>(GetValidClaims(claims));
            
            var user = new ApplicationUser
            {
                User = new Users
                {
                    UserName = Guid.NewGuid().ToString(),
                },
                Clients = new List<ClientsBasic>()
                {
                    clientsBasic
               }
            };
            var identityResult = await 
                _userManagement.SaveUserExternarlAsync(user, filtered, new UserLoginInfo(provider, providerUserId, provider));
            if (!identityResult.Succeeded) throw new Exception(identityResult.Errors.First().Description);

            return user;
        }

        // if the external login is OIDC-based, there are certain things we need to preserve to make logout work
        // this will be different for WS-Fed, SAML2p or other protocols
        private void ProcessLoginCallback(AuthenticateResult externalResult, List<Claim> localClaims, AuthenticationProperties localSignInProps)
        {
            // if the external system sent a session id claim, copy it over
            // so we can use it for single sign-out
            var sid = externalResult.Principal.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.SessionId);
            if (sid != null)
            {
                localClaims.Add(new Claim(JwtClaimTypes.SessionId, sid.Value));
            }

            // if the external provider issued an id_token, we'll keep it for signout
            var idToken = externalResult.Properties.GetTokenValue("id_token");
            if (idToken != null)
            {
                localSignInProps.StoreTokens(new[] { new AuthenticationToken { Name = "id_token", Value = idToken } });
            }
        }

        private IEnumerable<Claim> GetValidClaims(IEnumerable<Claim> claims)
        {
            List<Claim> result = new ();

            foreach (var item in claims)
            {
                switch (item.Type)
                {
                    case JwtClaimTypes.Name:
                    case ClaimTypes.Name:
                        result.Add(new Claim(JwtClaimTypes.Name, item.Value));
                        break;

                    case JwtClaimTypes.Email:
                    case ClaimTypes.Email:
                        result.Add(new Claim(JwtClaimTypes.Email, item.Value));
                        break;

                    case JwtClaimTypes.GivenName:
                    case ClaimTypes.GivenName:
                        result.Add(new Claim(JwtClaimTypes.GivenName, item.Value));
                        break;

                    case JwtClaimTypes.FamilyName:
                    case ClaimTypes.Surname:
                        result.Add(new Claim(JwtClaimTypes.FamilyName, item.Value));
                        break;

                    default:
                        break;
                }
            }

            return result;
        }
    }
}
