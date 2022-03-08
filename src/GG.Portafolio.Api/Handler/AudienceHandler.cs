using GG.Portafolio.Api.Model;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace GG.Portafolio.Api.Handler
{
    public class AudienceHandler : AuthorizationHandler<AudienceRequirement>
    {
        const string Audience = "aud";
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AudienceRequirement requirement)
        {
            if (context.User.HasClaim(x => x.Type == Audience && x.Value == requirement.Audience))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
