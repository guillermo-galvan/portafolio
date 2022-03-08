using IdentityModel.Client;
using System;

namespace GG.Portafolio.Site.Models
{
    public class TokenInfo
    {
        public TokenResponse TokenResponse { get; set; }

        public DateTime TokenExpiration { get; set; }
    }
}
