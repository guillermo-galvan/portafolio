using GG.Portafolio.Shared;
using GG.Portafolio.Shared.User;
using GG.Portafolio.Site.Generic.Interfaces;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace GG.Portafolio.Site.Generic.Events
{
    public class CustomOpenIdConnectEvents : OpenIdConnectEvents
    {
        private readonly IHttpClient _httpClient;
        private readonly ILogger<CustomOpenIdConnectEvents> _logger;

        public CustomOpenIdConnectEvents(IHttpClient httpClient, ILogger<CustomOpenIdConnectEvents> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public override async Task RedirectToIdentityProvider(RedirectContext context)
        {
            if (context.Request.Cookies[".ggpuntoycoma.session"] != null)
            {
                AuthenticateResult authenticateResult = await context.HttpContext.AuthenticateAsync();

                if (!authenticateResult.Succeeded && authenticateResult.Failure != null &&
                     authenticateResult.Failure.Message == "Ticket expired" &&
                     context.Properties.Items.ContainsKey(".redirect"))
                {
                    var logout = context.Properties.Items[".redirect"];

                    if (logout != "/Account/Logout")
                    {
                        context.HttpContext.Response.Redirect("/Account/Logout");
                        context.Response.Redirect("/Account/Logout");
                        context.HandleResponse();
                    }
                }
            }
        }

        public override Task RemoteFailure(RemoteFailureContext context)
        {
            context.HttpContext.Response.Redirect("/Account/AccessDenied");
            context.Response.Redirect("/Account/AccessDenied");
            context.HandleResponse();

            return Task.CompletedTask;
        }

        public override async Task UserInformationReceived(UserInformationReceivedContext context)
        {
            UserRequest userRequest = new();
            userRequest.Subject = context.User.RootElement.GetString(JwtClaimTypes.Subject) ?? context.User.RootElement.GetString(ClaimTypes.NameIdentifier);
            userRequest.Email = context.User.RootElement.GetString(JwtClaimTypes.Email) ?? context.User.RootElement.GetString(ClaimTypes.Email);
            userRequest.Name = context.User.RootElement.GetString(JwtClaimTypes.Name) ?? context.User.RootElement.GetString(ClaimTypes.Name);

            (UserResponse Ok, _, HttpStatusCode statusCode) =
            await _httpClient.GetAsync<UserResponse, ErrorApi>(
            $"User/validate?Subject={HttpUtility.ParseQueryString(userRequest.Subject)}&Name={HttpUtility.ParseQueryString(userRequest.Name)}&Email={HttpUtility.ParseQueryString(userRequest.Email)}",
            _logger);

            if (statusCode != HttpStatusCode.OK || !Ok.Success)
            {
                context.HttpContext.Response.Redirect("/Account/Logout");
                context.Response.Redirect("/Account/Logout");
                context.HandleResponse();
            }
        }
    }
}
