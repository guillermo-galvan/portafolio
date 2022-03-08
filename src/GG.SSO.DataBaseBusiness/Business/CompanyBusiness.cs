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
    public class CompanyBusiness
    {
        private readonly CompanySentences _companySentences;
        private readonly ILogger<CompanyBusiness> _logger;

        public CompanyBusiness(CompanySentences  companySentences, ILogger<CompanyBusiness> logger)
        {
            _companySentences = companySentences;
            _logger = logger;
        }

        public void Insert(Company company)
        {
            try
            {
                company.Insert();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Insert {company}", company);
                throw;
            }
        }

        public IEnumerable<Company> Get()
        {
            try
            {
                return Company.Read(_companySentences.AddCliteriByIsDeleted(false)
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
