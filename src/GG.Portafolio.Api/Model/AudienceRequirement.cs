using Microsoft.AspNetCore.Authorization;
using System;

namespace GG.Portafolio.Api.Model
{
    public class AudienceRequirement : IAuthorizationRequirement
    {
        public string Audience { get; set; }

        public AudienceRequirement(string audience)
        { 
            Audience = audience ?? throw new ArgumentNullException(nameof(audience));
        }
    }
}
