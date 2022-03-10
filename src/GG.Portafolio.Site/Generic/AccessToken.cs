using GG.Portafolio.Site.Generic.Interfaces;
using GG.Portafolio.Site.Models;
using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Reflection;

namespace GG.Portafolio.Site.Generic
{
    public class AccessToken : IAccessToken
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly OAuthConfiguration _oAuthConfiguration;

        public AccessToken(IHttpClientFactory httpClientFactory, ILogger<AccessToken> logger, IOptions<OAuthConfiguration> options)
        {
            _httpClientFactory = httpClientFactory;
            _oAuthConfiguration = options.Value;
            GenerateAccessToken(logger);
        }

        public DiscoveryDocumentResponse DiscoveryDocumentResponse { get; set; }

        public TokenInfo TokenInfo { get; set; }

        public void VerifyAutentificacion(ILogger logger)
        {
            lock (TokenInfo)
            {
                if (TokenInfo.TokenExpiration < DateTime.UtcNow)
                {
                    GenerateAccessToken(logger);
                }
            }
        }

        private async void GenerateAccessToken(ILogger logger)
        {
            try
            {
                TokenInfo ??= new TokenInfo() { TokenExpiration = DateTime.UtcNow };

                HttpClient client = _httpClientFactory.CreateClient(nameof(ConfigurationValues.SsoUrl));

                DiscoveryDocumentResponse ??= await client.GetDiscoveryDocumentAsync();

                if (DiscoveryDocumentResponse.IsError)
                {
                    throw new Exception(DiscoveryDocumentResponse.Error);
                }

                TokenInfo.TokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = DiscoveryDocumentResponse.TokenEndpoint,
                    ClientId = _oAuthConfiguration.ClientId,
                    ClientSecret = _oAuthConfiguration.ClientSecret,
                    Scope = _oAuthConfiguration.Scope,
                });

                if (TokenInfo.TokenResponse.IsError)
                {
                    throw new Exception(TokenInfo.TokenResponse.Error);
                }
                else
                {
                    TokenInfo.TokenExpiration = DateTime.UtcNow + TimeSpan.FromSeconds(TokenInfo.TokenResponse.ExpiresIn);
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Method {name}", MethodBase.GetCurrentMethod().Name);
            }
        }
    }
}
