using GGPuntoYComa.SSO.DataBaseBusiness.Sentences;
using GGPuntoYComa.SSO.Entity.Table.Sso;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace GGPuntoYComa.SSO.DataBaseBusiness.Business
{
    public class ApiScopesBusiness
    {
        private readonly ILogger<ApiScopesBusiness> _logger;
        private readonly ApiScopesSentences _apiScopesSentences;

        public ApiScopesBusiness(ILogger<ApiScopesBusiness> logger,
            ApiScopesSentences apiScopesSentences)
        {
            _logger = logger;            
            _apiScopesSentences = apiScopesSentences;
        }

        public IEnumerable<ApiScopes> Get()
        {
            try
            {
                return ApiScopes.Read(_apiScopesSentences.AddCliteriByIsDeleted(false).GetCriteriaCollection());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }
    }
}
