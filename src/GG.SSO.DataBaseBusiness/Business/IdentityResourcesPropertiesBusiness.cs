using GGPuntoYComa.SSO.DataBaseBusiness.Sentences;
using GGPuntoYComa.SSO.Entity.Table.Sso;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GGPuntoYComa.SSO.DataBaseBusiness.Business
{
    public class IdentityResourcesPropertiesBusiness
    {
        private readonly IdentityResourcesPropertiesSentences _identityResourcesPropertiesSentences;
        private readonly ILogger<IdentityResourcesPropertiesBusiness> _logger;

        public IdentityResourcesPropertiesBusiness(IdentityResourcesPropertiesSentences identityResourcesPropertiesSentences,
            ILogger<IdentityResourcesPropertiesBusiness> logger)
        {
            _identityResourcesPropertiesSentences = identityResourcesPropertiesSentences;
            _logger = logger;
        }

        public IEnumerable<IdentityResourceProperties> Get()
        {
            try
            {
                return IdentityResourceProperties.Read();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }
    }
}
