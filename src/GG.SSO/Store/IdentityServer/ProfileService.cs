using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using GG.SSO.BusinesLogic.Model.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GG.SSO.Store.IdentityServer
{
    public class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationAreaRole> _roleManager;

        public ProfileService(UserManager<ApplicationUser> userManager, 
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, RoleManager<ApplicationAreaRole> roleManager)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
            _roleManager = roleManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            var principal = await _claimsFactory.CreateAsync(user);

            var claims = principal.Claims.ToList();

            if (claims.Any(x => x.Type == "role"))
            {
                claims.FindAll(x => x.Type == "role").ForEach(async x => {
                    var a = await _roleManager.FindByNameAsync(x.Value);
                    if(a != null)
                        claims.Add(new Claim("area", a.Area.Name));
                });
            }

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
