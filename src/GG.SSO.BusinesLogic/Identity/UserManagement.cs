using GG.SSO.BusinesLogic.Model.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Security.Claims;
using GG.SSO.DataBaseBusiness.Business;

namespace GG.SSO.BusinesLogic.Identity
{
    public class UserManagement
    {
        private readonly UserManager<ApplicationUser> _userManager;        
        private readonly ILogger<UserManagement> _logger;
        private readonly CompanyClientsBusiness _companyClientsBusiness;
        private readonly CompanyAreasBusiness _companyAreasBusiness;
        private readonly AreasBusiness _areasBusiness;
        private readonly RolesAreasBusiness _rolesAreasBusiness;
        private readonly RolesBusiness _rolesBusiness;
        private readonly ClientsBasicBusiness _clientsBasicBusiness;

        public UserManagement(UserManager<ApplicationUser> userManager, 
            ILogger<UserManagement> logger, CompanyClientsBusiness companyClientsBusiness,
            CompanyAreasBusiness companyAreasBusiness, AreasBusiness areasBusiness, RolesAreasBusiness rolesAreasBusiness,
            RolesBusiness rolesBusiness, ClientsBasicBusiness clientsBasicBusiness)
        {
            _logger = logger;            
            _userManager = userManager;
            _companyClientsBusiness = companyClientsBusiness;
            _companyAreasBusiness = companyAreasBusiness;
            _areasBusiness = areasBusiness;
            _rolesAreasBusiness = rolesAreasBusiness;
            _rolesBusiness = rolesBusiness;
            _clientsBasicBusiness = clientsBasicBusiness;
        }

        public async Task<IdentityResult> SaveUserExternarlAsync(ApplicationUser user, IEnumerable<Claim> claim, 
            UserLoginInfo userLoginInfo)
        {
            IdentityResult identityResult;
            try
            {
                var clientId = user.Clients.First().Id;

                var companyClients = _companyClientsBusiness.GetByClient_Id(clientId);
                var companyAreas = _companyAreasBusiness.Get(companyClients.Company_Id);
                var areas = _areasBusiness.Get(companyAreas.Select(x => x.Areas_Id));
                var areaDefatult = areas.FirstOrDefault(x => x.IsDefault) ?? areas.First();
                var rolesArea = _rolesAreasBusiness.GetByArea(areaDefatult.Id);
                var roles = _rolesBusiness.Get(rolesArea.Select(x => x.Roles_Id));

                identityResult = await _userManager.CreateAsync(user);
                if (!identityResult.Succeeded) return identityResult;

                if (claim.Any())
                {
                    identityResult = await _userManager.AddClaimsAsync(user, claim);
                    if (!identityResult.Succeeded) return identityResult;
                }

                identityResult = await _userManager.AddLoginAsync(user, userLoginInfo);
                if (!identityResult.Succeeded) return identityResult;

                identityResult = await _userManager.AddToRolesAsync(user, roles.Select(x => x.Name));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {user} {claim} {userLoginInfo}",
                    MethodBase.GetCurrentMethod().Name, user,claim,userLoginInfo);
                identityResult =  IdentityResult.Failed(new IdentityError[]
                {
                    new IdentityError { Code = $"{ex.GetHashCode()}", Description = ex.Message }
                });
            }

            return identityResult;
        }

        public void UpdateClientUser(ApplicationUser user, string clientId)
        {
            try
            {
                var client = _clientsBasicBusiness.Get(clientId);
                user.Clients.Add(client);
                _userManager.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"", MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }
    }
}
