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
    public class CompanyAddressesBusiness
    {
        private readonly CompanyAddressesSentences _companyAddressesSentences;
        private readonly ILogger<CompanyAddressesBusiness> _logger;

        public CompanyAddressesBusiness(CompanyAddressesSentences  companyAddressesSentences, 
            ILogger<CompanyAddressesBusiness> logger)
        {
            _companyAddressesSentences = companyAddressesSentences;
            _logger = logger;
        }


        public void Insert(CompanyAddresses companyAddresses)
        {
            try
            {
                companyAddresses.Insert();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Insert {companyAddresses}", companyAddresses);
                throw;
            }
        }

        public IEnumerable<CompanyAddresses> Get(int company_Id)
        {
            try
            {
                return CompanyAddresses.Read(_companyAddressesSentences.AddCliteriByCompany_Id(company_Id)
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
