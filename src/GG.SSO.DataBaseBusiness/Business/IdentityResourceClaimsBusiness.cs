using GGPuntoYComa.SSO.DataBaseBusiness.Sentences;
using GGPuntoYComa.SSO.Entity.Table.Sso;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GGPuntoYComa.SSO.DataBaseBusiness.Business
{
    public class IdentityResourceClaimsBusiness
    {
        private readonly IdentityResourceClaimsSentences _identityResourceClaimsSentences;
        private readonly ILogger<IdentityResourceClaimsBusiness> _logger;

        public IdentityResourceClaimsBusiness(IdentityResourceClaimsSentences identityResourceClaimsSentences, 
            ILogger<IdentityResourceClaimsBusiness> logger)
        {
            _identityResourceClaimsSentences = identityResourceClaimsSentences;
            _logger = logger;
        }


        public IEnumerable<IdentityResourceClaims> Get()
        {
            try
            {
                return IdentityResourceClaims.Read();
            }
            catch (Exception  ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

    }
}
