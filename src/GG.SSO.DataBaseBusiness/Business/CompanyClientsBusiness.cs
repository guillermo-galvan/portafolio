using GG.SSO.DataBaseBusiness.Sentences;
using GG.SSO.Entity.Table.Sso;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace GG.SSO.DataBaseBusiness.Business
{
    public class CompanyClientsBusiness
    {
        private readonly CompanyClientsSentences _companyClientsSentences;
        private readonly ILogger<CompanyClientsBusiness> _logger;

        public CompanyClientsBusiness(CompanyClientsSentences companyClientsSentences, ILogger<CompanyClientsBusiness> logger)
        {
            _logger = logger;
            _companyClientsSentences = companyClientsSentences;
        }

        public void Insert(CompanyClients companyClients)
        {
            try
            {
                companyClients.Insert();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Insert {companyClients}", companyClients);
                throw;
            }
        }

        public IEnumerable<CompanyClients> Get(int company_Id)
        {
            try
            {
                return CompanyClients.Read(_companyClientsSentences.AddCliteriByCompany_Id(company_Id)
                                                                   .GetCriteriaCollection());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {company_Id}",MethodBase.GetCurrentMethod().Name, company_Id);
                throw;
            }
        }

        public CompanyClients GetByClient_Id(int client_Id)
        {
            try
            {
                return CompanyClients.Read(_companyClientsSentences.AddCliteriByClients_Id(client_Id)
                                                                   .GetCriteriaCollection()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {client_id}", MethodBase.GetCurrentMethod().Name, client_Id);
                throw;
            }
        }
    }
}
