using GG.SSO.DataBaseBusiness.Sentences;
using GG.SSO.Entity.Table.Sso;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Business
{
    public class UserRolesBusiness
    {
        private readonly UserRolesSentencias _userRolesSentencias;
        private readonly ILogger<UserRolesBusiness> _logger;

        public UserRolesBusiness(UserRolesSentencias userRolesSentencias, ILogger<UserRolesBusiness> logger)
        {
            _logger = logger;
            _userRolesSentencias = userRolesSentencias;
        }

        public void Insert(UserRoles userRoles)
        {
            try
            {
                userRoles.Insert();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Insert {userRoles}", userRoles);
                throw;
            }
        }

        public void Delete(UserRoles userRoles)
        {
            try
            {
                userRoles.Delete();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete {userRoles}", userRoles);
                throw;
            }
        }

        public UserRoles Get(string users_Id, string roles_Id)
        {
            UserRoles result;

            try
            {
                result = UserRoles.Read(_userRolesSentencias.AddCliteriByUsers_Id(users_Id)
                                                            .AddCliteriByRoles_Id(roles_Id)
                                                            .GetCriteriaCollection())
                                  .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get {roles_Id} {users_Id}", roles_Id, users_Id);
                throw;
            }

            return result;
        }

        public IEnumerable<UserRoles> Get(string users_Id)
        {
            try
            {
                return UserRoles.Read(_userRolesSentencias.AddCliteriByUsers_Id(users_Id).GetCriteriaCollection());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get {users_Id}",  users_Id);
                throw;
            }
        }

        public IEnumerable<UserRoles> GetByRolesId(string roles_Id)
        {
            try
            {
                return UserRoles.Read(_userRolesSentencias.AddCliteriByRoles_Id(roles_Id).GetCriteriaCollection());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get {roles_Id}", roles_Id);
                throw;
            }
        }
    }
}
