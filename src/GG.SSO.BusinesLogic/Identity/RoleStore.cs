using GG.SSO.DataBaseBusiness.Business;
using GG.SSO.BusinesLogic.Helpers;
using GG.SSO.BusinesLogic.Model.Identity;
using GG.SSO.DataBaseBusiness.Business;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace GG.SSO.BusinesLogic.Identity
{
    public class RoleStore : IRoleStore<ApplicationAreaRole>, IRoleClaimStore<ApplicationAreaRole>
    {
        private readonly RolesBusiness _rolesBusiness;
        private readonly RolesAreasBusiness _rolesAreasBusiness;
        private readonly AreasBusiness _areasBusiness;
        private readonly ILogger<RoleStore> _logger;
        private readonly RoleClaimsBusiness _roleClaimsBusiness;

        public RoleStore(RolesBusiness rolesBusiness, RolesAreasBusiness rolesAreasBusiness, AreasBusiness areasBusiness,
            ILogger<RoleStore> logger, RoleClaimsBusiness roleClaimsBusiness)
        {
            _rolesBusiness = rolesBusiness;
            _rolesAreasBusiness = rolesAreasBusiness;
            _areasBusiness = areasBusiness;
            _logger = logger;
            _roleClaimsBusiness = roleClaimsBusiness;
        }

        private void Validated(ApplicationAreaRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            if (role.Area == null)
            {
                throw new ArgumentNullException(nameof(role.Area));
            }

            if (role.Role == null)
            {
                throw new ArgumentNullException(nameof(role.Role));
            }
        }

        private void SaveLog(Exception ex, ApplicationAreaRole role, string methodName)
        {
            _logger.LogError(ex, "{methodName} {Role} {Area}", methodName, role.Role, role.Area);
        }

        private IdentityResult GetIdentityError(Exception ex)
        {
            return IdentityResult.Failed(new IdentityError[]
                {
                    new IdentityError { Code = $"{ex.GetHashCode()}", Description = ex.Message }
                });
        }

        public Task<IdentityResult> CreateAsync(ApplicationAreaRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Validated(role);

            try
            {
                using TransactionScope scope = new();

                role.Role.ConcurrencyStamp = Guid.NewGuid().ToString();
                role.Role.Id = Guid.NewGuid().ToString();

                _rolesBusiness.Insert(role.Role);
                _rolesAreasBusiness.Insert(role.Area, role.Role);

                scope.Complete();
            }
            catch (Exception ex)
            {
                SaveLog(ex, role, MethodBase.GetCurrentMethod().Name);
                return Task.FromResult(GetIdentityError(ex));
            }


            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(ApplicationAreaRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Validated(role);

            try
            {
                using TransactionScope scope = new();
                _rolesBusiness.Delete(role.Role);
                scope.Complete();
            }
            catch (Exception ex)
            {
                SaveLog(ex, role, MethodBase.GetCurrentMethod().Name);
                return Task.FromResult(GetIdentityError(ex));
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<ApplicationAreaRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ApplicationAreaRole result = null;

            try
            {
                var role = _rolesBusiness.Get(roleId);
                if (role != null)
                {
                    var roleArea = _rolesAreasBusiness.GetByRole(role.Id);
                    var area = _areasBusiness.Get(roleArea.Areas_Id);
                    result = new ApplicationAreaRole(role, area);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{methodName} {roleId}", MethodBase.GetCurrentMethod().Name, roleId);
            }

            return Task.FromResult(result);
        }

        public Task<ApplicationAreaRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ApplicationAreaRole result = null;
            try
            {
                var role = _rolesBusiness.GetByNormalizedRoleName(normalizedRoleName);
                if (role != null)
                {
                    var roleArea = _rolesAreasBusiness.GetByRole(role.Id);
                    var area = _areasBusiness.Get(roleArea.Areas_Id);
                    result = new ApplicationAreaRole(role, area);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{methodName} {normalizedRoleName}", MethodBase.GetCurrentMethod().Name, normalizedRoleName);
            }

            return Task.FromResult(result);
        }

        public Task<string> GetNormalizedRoleNameAsync(ApplicationAreaRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Validated(role);
            return Task.FromResult(role.Role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(ApplicationAreaRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Validated(role);
            return Task.FromResult(role.Role.Id);
        }

        public Task<string> GetRoleNameAsync(ApplicationAreaRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Validated(role);
            return Task.FromResult(role.Role.Name);
        }

        public Task SetNormalizedRoleNameAsync(ApplicationAreaRole role, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Validated(role);
            role.Role.NormalizedName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(ApplicationAreaRole role, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Validated(role);
            role.Role.Name = roleName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(ApplicationAreaRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Validated(role);

            try
            {
                role.Role.ConcurrencyStamp = Guid.NewGuid().ToString();
                using TransactionScope scope = new();
                _rolesBusiness.Update(role.Role);
                scope.Complete();
            }
            catch (Exception ex)
            {
                SaveLog(ex, role, MethodBase.GetCurrentMethod().Name);
                return Task.FromResult(GetIdentityError(ex));
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {

        }

        public Task<IList<Claim>> GetClaimsAsync(ApplicationAreaRole role, CancellationToken cancellationToken = default)
        {
            if (cancellationToken != default)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }

            List<Claim> result = new();
            Validated(role);

            try
            {
                var claimsDb = _roleClaimsBusiness.Get(role.Role.Id);
                result.AddRange(claimsDb.Select(x => Converts.ToClaim(x)));
            }
            catch (Exception ex)
            {
                SaveLog(ex, role, MethodBase.GetCurrentMethod().Name);
            }

            return Task.FromResult((IList<Claim>)result);
        }

        public Task AddClaimAsync(ApplicationAreaRole role, Claim claim, CancellationToken cancellationToken = default)
        {
            if (cancellationToken != default)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }

            Validated(role);
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            try
            {
                _roleClaimsBusiness.Insert(
                    new Entity.Table.Sso.RoleClaims(0, role.Role.Id, claim.Type, claim.Value, claim.ValueType));
            }
            catch (Exception ex)
            {
                SaveLog(ex, role, MethodBase.GetCurrentMethod().Name);
                throw;
            }

            return Task.FromResult(false);
        }

        public Task RemoveClaimAsync(ApplicationAreaRole role, Claim claim, CancellationToken cancellationToken = default)
        {
            if (cancellationToken != default)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }

            Validated(role);
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            try
            {
                _roleClaimsBusiness.Delete(
                    new Entity.Table.Sso.RoleClaims(0, role.Role.Id, claim.Type, claim.Value, claim.ValueType));
            }
            catch (Exception ex)
            {
                SaveLog(ex, role, MethodBase.GetCurrentMethod().Name);
                throw;
            }

            return Task.FromResult(false);
        }
    }
}
