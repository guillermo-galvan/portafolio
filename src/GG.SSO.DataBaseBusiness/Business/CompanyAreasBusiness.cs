using GG.SSO.DataBaseBusiness.Sentences;
using GG.SSO.Entity.Table.Sso;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GG.SSO.DataBaseBusiness.Business
{
    public class CompanyAreasBusiness
    {
        private readonly CompanyAreasSentences _companyAreasSentences;
        private readonly ILogger<CompanyAreasBusiness> _logger;

        public CompanyAreasBusiness(CompanyAreasSentences companyAreasSentences, ILogger<CompanyAreasBusiness> logger)
        {
            _logger = logger;
            _companyAreasSentences = companyAreasSentences;
        }

        public void Insert(CompanyAreas companyAreas)
        {
            try
            {
                companyAreas.Insert();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {companyAreas}", MethodBase.GetCurrentMethod().Name,companyAreas);
                throw;
            }
        }

        public IEnumerable<CompanyAreas> Get(int company_Id)
        {
            try
            {
                return CompanyAreas.Read(_companyAreasSentences.AddCliteriByCompany_Id(company_Id)
                                                               .GetCriteriaCollection());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {company_Id}", MethodBase.GetCurrentMethod().Name, company_Id);
                throw;
            }
        }
    }
}
