using GG.SSO.DataBaseBusiness.Sentences;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using GG.SSO.Entity.Table.Sso;

namespace GG.SSO.DataBaseBusiness.Business
{
    public class ApiResourceScopesBusiness
    {
        private readonly ApiResourceScopesSentences _apiResourceScopesSentences;
        private readonly ILogger<ApiResourceScopesBusiness> _logger;

        public ApiResourceScopesBusiness(ApiResourceScopesSentences apiResourceScopesSentences, ILogger<ApiResourceScopesBusiness> logger)
        {
            _apiResourceScopesSentences = apiResourceScopesSentences;
            _logger = logger;
        }

        public IEnumerable<ApiResourceScopes> Get(IEnumerable<int> ids)
        {
            try
            {
                return ApiResourceScopes.Read(_apiResourceScopesSentences.AddInCliteriByApiResources_Id(ids)
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
