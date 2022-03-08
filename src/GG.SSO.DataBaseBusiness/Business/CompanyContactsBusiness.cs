using GGPuntoYComa.SSO.DataBaseBusiness.Sentences;
using GGPuntoYComa.SSO.Entity.Table.Sso;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGPuntoYComa.SSO.DataBaseBusiness.Business
{
    public class CompanyContactsBusiness
    {
        private readonly CompanyContactsSentences _companyContactsSentences;
        private readonly ILogger<CompanyContactsBusiness> _logger;

        public CompanyContactsBusiness(CompanyContactsSentences companyContactsSentences, 
            ILogger<CompanyContactsBusiness> logger)
        {
            _companyContactsSentences = companyContactsSentences;
            _logger = logger;
        }

        public void Insert(CompanyContacts companyContacts)
        {
            try
            {
                companyContacts.Insert();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Insert {companyContacts}", companyContacts);
                throw;
            }
        }

        public IEnumerable<CompanyContacts> Get(int company_Id)
        {
            try
            {
                return CompanyContacts.Read(_companyContactsSentences.AddCliteriByCompany_Id(company_Id)
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
