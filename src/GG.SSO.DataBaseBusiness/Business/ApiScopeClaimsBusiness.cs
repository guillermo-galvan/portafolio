using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using GG.SSO.Entity.Table.Sso;
using GG.SSO.DataBaseBusiness.Sentences;

namespace GG.SSO.DataBaseBusiness.Business
{
    public class ApiScopeClaimsBusiness
    {
        private readonly ApiScopeClaimsSentences _apiScopeClaimsSentences;
        private readonly ILogger<ApiScopeClaimsBusiness> _logger;

        public ApiScopeClaimsBusiness(ApiScopeClaimsSentences apiScopeClaimsSentences, ILogger<ApiScopeClaimsBusiness> logger)
        {
            _apiScopeClaimsSentences = apiScopeClaimsSentences;
            _logger = logger;
        }

        public IEnumerable<ApiScopeClaims> Get(IEnumerable<int> ids)
        {
            try
            {
                return ApiScopeClaims.Read(_apiScopeClaimsSentences.AddInCliteriByApiScopes_Id(ids)
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
