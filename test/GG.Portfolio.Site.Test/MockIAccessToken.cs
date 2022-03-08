using GG.Portafolio.Site.Generic.Interfaces;
using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GG.Portafolio.Site.Test
{
    internal static class MockIAccessToken
    {
        private static readonly object _objectLock = new();
        private static Mock<IAccessToken> _mockIAccessToken;

        internal static Mock<IAccessToken> GetMockIAccessToken()
        {
            lock (_objectLock)
            {

                if (_mockIAccessToken == null)
                {
                    _mockIAccessToken = new();
                    var path = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Documents", "success_token_response.json");
                    var content = File.ReadAllText(path);

                    NetworkHandler handler = new(content, HttpStatusCode.OK);
                    HttpClient client = new(handler)
                    {
                        BaseAddress = new Uri("http://server/token")
                    };
                    TokenClient tokenClient = new(client, new TokenClientOptions { ClientId = "client" });

                    _mockIAccessToken.Setup(m => m.VerifyAutentificacion(It.IsAny<ILogger>()));

                    _mockIAccessToken.Setup(m => m.TokenInfo).Returns(new Models.TokenInfo
                    {
                        TokenExpiration = DateTime.UtcNow.AddDays(1),
                        TokenResponse = tokenClient.RequestAuthorizationCodeTokenAsync(code: "code", redirectUri: "uri", codeVerifier: "verifier").Result,
                    });

                    path = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Documents", "discovery.json");
                    content = File.ReadAllText(path);
                    path = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Documents", "discovery_jwks.json");
                    var jwks = File.ReadAllText(path);

                    handler = new(request =>
                   {
                       if (request.RequestUri.AbsoluteUri.EndsWith("jwks"))
                       {
                           return jwks;
                       }

                       return content;
                   }, HttpStatusCode.OK);

                    client = new(handler);
                    var cache = new DiscoveryCache("https://demo.identityserver.io", () => client);
                    var document = cache.GetAsync().Result;
                    _mockIAccessToken.Setup(m => m.DiscoveryDocumentResponse).Returns(document);

                }
            }

            return _mockIAccessToken;
        }
    }
}
