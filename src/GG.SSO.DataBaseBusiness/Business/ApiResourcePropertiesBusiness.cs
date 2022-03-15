using GG.SSO.DataBaseBusiness.Sentences;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GG.SSO.Entity.Table.Sso;

namespace GG.SSO.DataBaseBusiness.Business
{
    public class ApiResourcePropertiesBusiness
    {
        private readonly ApiResourcePropertiesSentences _apiResourcePropertiesSentences;
        private readonly ILogger<ApiResourcePropertiesBusiness> _logger;


        public ApiResourcePropertiesBusiness(ApiResourcePropertiesSentences apiResourcePropertiesSentences,
            ILogger<ApiResourcePropertiesBusiness> logger)
        {
            _logger = logger;
            _apiResourcePropertiesSentences = apiResourcePropertiesSentences;
        }

        public IEnumerable<ApiResourceProperties> Get(IEnumerable<int> ids)
        {
            try
            {
                return ApiResourceProperties.Read(_apiResourcePropertiesSentences.AddInCliteriByApiResources_Id(ids)
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
