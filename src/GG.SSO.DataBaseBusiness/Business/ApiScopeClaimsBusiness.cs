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
