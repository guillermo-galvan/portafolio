using GG.SSO.Entity.Table.Sso;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using GG.SSO.DataBaseBusiness.Sentences;

namespace GG.SSO.DataBaseBusiness.Business
{
    public class RolesBusiness
    {
        private readonly RolesSentences _rolesSentences;
        private readonly ILogger<RolesBusiness> _logger;

        public RolesBusiness(RolesSentences rolesSentences, ILogger<RolesBusiness> logger)
        {
            _rolesSentences = rolesSentences;
            _logger = logger;
        }

        public void Insert(Roles role)
        {
            try
            {
                role.Insert();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {role}", MethodBase.GetCurrentMethod().Name, role);
                throw;
            }
        }

        public void Delete(Roles role)
        {
            try
            {
                role.Delete();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {role}", MethodBase.GetCurrentMethod().Name, role);
                throw;
            }
        }

        public void Update(Roles role)
        {
            try
            {
                role.Update();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {role}", MethodBase.GetCurrentMethod().Name, role);
                throw;
            }
        }

        public Roles Get(string roleId)
        {
            Roles result;
            try
            {
                result = Roles.Read(_rolesSentences.AddCliteriById(roleId).GetCriteriaCollection()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {role}", MethodBase.GetCurrentMethod().Name, roleId);
                throw;
            }

            return result;
        }

        public Roles GetByNormalizedRoleName(string normalizedRoleName)
        {
            Roles result;
            try
            {
                result = Roles.Read(_rolesSentences.AddCliteriByNormalizedName(normalizedRoleName).GetCriteriaCollection())
                              .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {normalizedRoleName}", MethodBase.GetCurrentMethod().Name, normalizedRoleName);
                throw;
            }

            return result;
        }

        public IEnumerable<Roles> Get(IEnumerable<string> rolesIds)
        {
            try
            {
                return rolesIds.Any() ? Roles.Read(_rolesSentences.AddInCliteriaByRoles_Id(rolesIds).GetCriteriaCollection())
                    : new List<Roles>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {rolesIds}", MethodBase.GetCurrentMethod().Name, rolesIds);
                throw;
            }
        }

        public IEnumerable<Roles> Get()
        {
            try
            {
                return Roles.Read();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

    }
}
