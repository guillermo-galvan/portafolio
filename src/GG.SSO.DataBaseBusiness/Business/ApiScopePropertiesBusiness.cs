using GGPuntoYComa.SSO.DataBaseBusiness.Sentences;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using GGPuntoYComa.SSO.Entity.Table.Sso;

namespace GGPuntoYComa.SSO.DataBaseBusiness.Business
{
    public class ApiScopePropertiesBusiness
    {
        private readonly ApiScopePropertiesSentences _apiScopePropertiesSentences;
        private readonly ILogger<ApiScopePropertiesBusiness> _logger;

        public ApiScopePropertiesBusiness(ApiScopePropertiesSentences apiScopePropertiesSentences, ILogger<ApiScopePropertiesBusiness> logger)
        {
            _apiScopePropertiesSentences = apiScopePropertiesSentences;
            _logger = logger;
        }

        public IEnumerable<ApiScopeProperties> Get(IEnumerable<int> ids)
        {
            try
            {
                return ApiScopeProperties.Read(_apiScopePropertiesSentences.AddInCliteriByApiScopes_Id(ids)
                                                                           .GetCriteriaCollection());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }
    }
}
