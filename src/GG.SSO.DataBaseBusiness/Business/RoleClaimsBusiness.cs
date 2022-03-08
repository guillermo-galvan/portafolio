using GG.SSO.DataBaseBusiness.Sentences;
using GG.SSO.Entity.Table.Sso;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Business
{
    public class RoleClaimsBusiness
    {
        private readonly RoleClaimsSentences _roleClaimsSentences;
        private readonly ILogger<RoleClaimsBusiness> _logger;

        public RoleClaimsBusiness(RoleClaimsSentences roleClaimsSentences, ILogger<RoleClaimsBusiness> logger)
        {
            _roleClaimsSentences = roleClaimsSentences;
            _logger = logger;
        }

        public void Insert(RoleClaims roleClaims)
        {
            try
            {
                roleClaims.Insert();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Insert {roleClaims}", roleClaims);
                throw;
            }
        }

        public void Delete(RoleClaims roleClaims)
        {
            try
            {
                _roleClaimsSentences.AddCliteriByRoles_Id(roleClaims.Roles_Id)
                                        .AddCliteriByType(roleClaims.Type)
                                        .AddCliteriByValue(roleClaims.Value);

                RoleClaims.Delete(_roleClaimsSentences.GetCriteriaCollection(), ConnectionDataBaseCollection.Connections[0]);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete {roleClaims}", roleClaims);
                throw;
            }
        }

        public IEnumerable<RoleClaims> Get(string roleId)
        {
            List<RoleClaims> result = new();
            try
            {
                result.AddRange(RoleClaims.Read(_roleClaimsSentences.AddCliteriByRoles_Id(roleId).GetCriteriaCollection()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get {roleId}", roleId);
                throw;
            }

            return result;
        }
    }
}
