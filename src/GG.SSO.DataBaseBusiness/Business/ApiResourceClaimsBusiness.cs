using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using GG.SSO.Entity.Table.Sso;
using GG.SSO.DataBaseBusiness.Sentences;

namespace GG.SSO.DataBaseBusiness.Business
{
    public class ApiResourceClaimsBusiness
    {
        private readonly ApiResourceClaimsSentences _apiResourceClaimsSentences;
        private readonly ILogger<ApiResourceClaimsBusiness> _logger;

        public ApiResourceClaimsBusiness(ApiResourceClaimsSentences apiResourceClaimsSentences, 
            ILogger<ApiResourceClaimsBusiness> logger)
        {
            _apiResourceClaimsSentences = apiResourceClaimsSentences;
            _logger = logger;
        }

        public IEnumerable<ApiResourceClaims> Get(IEnumerable<int> ids)
        {
            try
            {
                return ApiResourceClaims.Read(_apiResourceClaimsSentences.AddInCliteriByApiResources_Id(ids)
                                                                         .GetCriteriaCollection());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} Ids", MethodBase.GetCurrentMethod().Name, ids);
                throw;
            }
        }
    }
}
