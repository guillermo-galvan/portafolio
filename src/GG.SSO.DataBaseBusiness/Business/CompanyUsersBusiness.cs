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
    public class CompanyUsersBusiness
    {
        private readonly CompanyUsersSentences _companyUsersSentences;
        private readonly ILogger<CompanyUsersBusiness> _logger;

        public CompanyUsersBusiness(CompanyUsersSentences companyUsersSentences, ILogger<CompanyUsersBusiness> logger)
        {
            _logger = logger;
            _companyUsersSentences = companyUsersSentences;
        }

        public void Insert(CompanyUsers companyUsers)
        {
            try
            {
                companyUsers.Insert();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Insert {companyUsers}", companyUsers);
                throw;
            }
        }
    }
}
