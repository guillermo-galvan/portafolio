using GG.SSO.DataBaseBusiness.Sentences;
using GG.SSO.Entity.Table.Sso;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GG.SSO.DataBaseBusiness.Business
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

    }
}
