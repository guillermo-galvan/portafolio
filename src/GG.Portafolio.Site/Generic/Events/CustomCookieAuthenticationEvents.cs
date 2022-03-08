using GG.Portafolio.Site.Generic.Interfaces;
using GG.Portafolio.Site.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GG.Portafolio.Site.Generic.Events
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly IAccessToken _accessToken;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly OAuthConfigurationUser _oAuthConfigurationUser;

        public CustomCookieAuthenticationEvents(IAccessToken accessToken, IHttpClientFactory httpClientFactory, IOptions<OAuthConfigurationUser> option)
        {
            _accessToken = accessToken;
            _httpClientFactory = httpClientFactory;
            _oAuthConfigurationUser = option.Value;
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            if (context.Principal.Identity.IsAuthenticated)
            {
                var tokens = context.Properties.GetTokens();
                var refreshToken = tokens.FirstOrDefault(t => t.Name == "refresh_token");
                var accessToken = tokens.FirstOrDefault(t => t.Name == "access_token");
                var exp = tokens.FirstOrDefault(t => t.Name == "expires_at");
                var expires = DateTime.Parse(exp.Value);

                if (expires < DateTime.Now)
                {
                    var tokenResponse = await _httpClientFactory.CreateClient(nameof(ConfigurationValues.SsoUrl)).RequestRefreshTokenAsync(new RefreshTokenRequest
                    {
                        Address = _accessToken.DiscoveryDocumentResponse.TokenEndpoint,
                        ClientId = _oAuthConfigurationUser.ClientId,
                        ClientSecret = _oAuthConfigurationUser.ClientSecret,
                        RefreshToken = refreshToken.Value
                    });

                    if (tokenResponse.IsError)
                    {
                        context.RejectPrincipal();
                    }
                    else
                    {
                        refreshToken.Value = tokenResponse.RefreshToken;
                        accessToken.Value = tokenResponse.AccessToken;
                        var newExpires = DateTime.UtcNow + TimeSpan.FromSeconds(tokenResponse.ExpiresIn);
                        exp.Value = newExpires.ToString("o", CultureInfo.InvariantCulture);
                        context.Properties.StoreTokens(tokens);
                        context.ShouldRenew = true;
                    }
                }
            }
        }

    }
}
