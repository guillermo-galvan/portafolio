using GGPuntoYComa.SSO.DataBaseBusiness.Sentences;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GGPuntoYComa.SSO.Entity.Table.Sso;

namespace GGPuntoYComa.SSO.DataBaseBusiness.Business
{
    public class ApiResourceSecretsBusiness
    {
        private readonly ApiResourceSecretsSentences _apiResourceSecretsSentences;
        private readonly ILogger<ApiResourceSecretsBusiness> _logger;

        public ApiResourceSecretsBusiness(ApiResourceSecretsSentences apiResourceSecretsSentences, 
            ILogger<ApiResourceSecretsBusiness> logger)
        {
            _apiResourceSecretsSentences = apiResourceSecretsSentences;
            _logger = logger;
        }

        public IEnumerable<ApiResourceSecrets> Get(IEnumerable<int> ids)
        {
            try
            {
                return ApiResourceSecrets.Read(_apiResourceSecretsSentences.AddInCliteriByApiResources_Id(ids)
                                                                           .GetCriteriaCollection());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {ids}", MethodBase.GetCurrentMethod().Name, ids);
                throw;
            }
        }
    }
}
