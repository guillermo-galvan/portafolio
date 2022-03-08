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
    public class CompanyRolesBusiness
    {
        private readonly CompanyRolesSentences _companyRolesSentences;
        private readonly ILogger<CompanyRolesBusiness> _logger;

        public CompanyRolesBusiness(CompanyRolesSentences companyRolesSentences, ILogger<CompanyRolesBusiness> logger)
        {
            _logger = logger;
            _companyRolesSentences = companyRolesSentences;
        }

        public void Insert(CompanyRoles companyRoles)
        {
            try
            {
                companyRoles.Insert();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Insert {companyRoles}", companyRoles);
                throw;
            }
        }

        public IEnumerable<CompanyRoles> Get(int company_Id)
        {
            try
            {
                return CompanyRoles.Read(_companyRolesSentences.AddCliteriByCompany_Id(company_Id)
                                                                             .GetCriteriaCollection());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get");
                throw;
            }
        }
    }
}
