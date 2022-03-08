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
    public class CompanyApiResourcesBusiness
    {
        private readonly CompanyApiResourcesSentences _companyApyresourcesSentences;
        private readonly ILogger<CompanyClientsBusiness> _logger;

        public CompanyApiResourcesBusiness(CompanyApiResourcesSentences companyApyresourcesSentences, ILogger<CompanyClientsBusiness> logger)
        {
            _companyApyresourcesSentences = companyApyresourcesSentences;
            _logger = logger;
        }

        public void Insert(CompanyApiResources companyApyresources)
        {
            try
            {
                companyApyresources.Insert();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Insert {companyApyresources}", companyApyresources);
                throw;
            }
        }

        public IEnumerable<CompanyApiResources> Get(int company_Id)
        {
            try
            {
                return CompanyApiResources.Read(_companyApyresourcesSentences.AddCliteriByCompany_Id(company_Id)
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
