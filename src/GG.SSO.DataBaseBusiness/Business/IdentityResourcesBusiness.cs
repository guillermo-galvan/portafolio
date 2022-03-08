using GG.SSO.DataBaseBusiness.Sentences;
using GG.SSO.Entity.Table.Sso;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GG.SSO.DataBaseBusiness.Business
{
    public class IdentityResourcesBusiness
    {
        private readonly ILogger<IdentityResourcesBusiness> _logger;
        private readonly IdentityResourcesSentences _identityResourcesSentences;

        public IdentityResourcesBusiness(ILogger<IdentityResourcesBusiness> logger,
            IdentityResourcesSentences identityResourcesSentences)
        {
            _logger = logger;
            _identityResourcesSentences = identityResourcesSentences;
        }

        public IEnumerable<IdentityResources> Get()
        {
            try
            {
                return IdentityResources.Read();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        public IEnumerable<IdentityResources> Get(IEnumerable<string> names)
        {
            try
            {
                return IdentityResources.Read(_identityResourcesSentences.AddInCliteriaByName(names)
                                                                         .GetCriteriaCollection());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {names}", MethodBase.GetCurrentMethod().Name, names);
                throw;
            }
        }


    }
}
