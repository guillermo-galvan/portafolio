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
    public class CompanyApiScopesBusiness
    {
        private readonly CompanyApiScopesSentences _companyApiscopesSentences;
        private readonly ILogger<CompanyApiScopesBusiness> _logger;

        public CompanyApiScopesBusiness(CompanyApiScopesSentences companyApiscopesSentences, ILogger<CompanyApiScopesBusiness> logger)
        {
            _logger = logger;
            _companyApiscopesSentences = companyApiscopesSentences;
        }

        public void Insert(CompanyApiScopes companyApiscopes)
        {
            try
            {
                companyApiscopes.Insert();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Insert {companyApiscopes}", companyApiscopes);
                throw;
            }
        }

        public IEnumerable<CompanyApiScopes> Get(int company_Id)
        {
            try
            {
                return CompanyApiScopes.Read(_companyApiscopesSentences.AddCliteriByCompany_Id(company_Id)
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
