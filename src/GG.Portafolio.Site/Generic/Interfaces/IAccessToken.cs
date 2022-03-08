using GG.Portafolio.Site.Models;
using IdentityModel.Client;
using Microsoft.Extensions.Logging;

namespace GG.Portafolio.Site.Generic.Interfaces
{
    public interface IAccessToken
    {
        DiscoveryDocumentResponse DiscoveryDocumentResponse { get; set; }

        TokenInfo TokenInfo { get; set; }

        void VerifyAutentificacion(ILogger logger);
    }
}
