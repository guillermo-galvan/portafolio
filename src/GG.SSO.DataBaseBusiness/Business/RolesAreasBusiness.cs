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
    public class RolesAreasBusiness
    {
        private readonly RolesAreasSentences _rolesAreasSentences;
        private readonly ILogger<RolesAreasBusiness> _logger;

        public RolesAreasBusiness(RolesAreasSentences rolesAreasSentences, ILogger<RolesAreasBusiness> logger)
        {
            _rolesAreasSentences = rolesAreasSentences;
            _logger = logger;
        }

        public void Insert(Areas areas, Roles roles)
        {
            try
            {
                new RolesAreas(roles.Id, areas.Id).Insert();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {areas} {roles}", MethodBase.GetCurrentMethod().Name, areas, roles);
                throw;
            }
        }

        public void Delete(Areas areas, Roles roles)
        {
            try
            {
                new RolesAreas(roles.Id, areas.Id).Delete();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {areas} {roles}", MethodBase.GetCurrentMethod().Name, areas, roles);
                throw;
            }
        }

        public RolesAreas GetByRole(string roleId)
        {
            try
            {
                return RolesAreas.Read(_rolesAreasSentences.AddCliteriByRoles_Id(roleId).GetCriteriaCollection())
                                   .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {roleId}", MethodBase.GetCurrentMethod().Name, roleId);
                throw;
            }
        }

        public IEnumerable<RolesAreas> GetByArea(int area_Id)
        {
            try
            {
                return RolesAreas.Read(_rolesAreasSentences.AddCliteriByAreas_Id(area_Id).GetCriteriaCollection());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {roleId}", MethodBase.GetCurrentMethod().Name, area_Id);
                throw;
            }
        }
    }
}
