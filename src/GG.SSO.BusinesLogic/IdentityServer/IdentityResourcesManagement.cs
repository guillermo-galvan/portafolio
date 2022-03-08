using Duende.IdentityServer.Models;
using GG.SSO.DataBaseBusiness;
using GG.SSO.DataBaseBusiness.Business;
using GG.SSO.BusinesLogic.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.BusinesLogic.IdentityServer
{
    public class IdentityResourcesManagement
    {
        const string Key = "IdentityResources";

        private readonly IdentityResourcesBusiness _identityResourcesBusiness;
        private readonly ILogger<IdentityResourcesManagement> _logger;
        private readonly IMemoryCache _cache;
        private readonly DataBaseOptions _dataBaseOptions;
        private readonly IdentityResourcesPropertiesBusiness _identityResourcesPropertiesBusiness;
        private readonly IdentityResourceClaimsBusiness _identityResourceClaimsBusiness;

        public IdentityResourcesManagement(IdentityResourcesBusiness identityResourcesBusiness, 
            ILogger<IdentityResourcesManagement> logger, IMemoryCache cache, DataBaseOptions dataBaseOptions,
            IdentityResourcesPropertiesBusiness identityResourcesPropertiesBusiness,
            IdentityResourceClaimsBusiness identityResourceClaimsBusiness)
        {
            _identityResourcesBusiness = identityResourcesBusiness;
            _logger = logger;
            _cache = cache;
            _dataBaseOptions = dataBaseOptions;
            _identityResourcesPropertiesBusiness = identityResourcesPropertiesBusiness;
            _identityResourceClaimsBusiness = identityResourceClaimsBusiness;
        }

        public IEnumerable<IdentityResource> Get()
        {
            List<IdentityResource> result = _cache.Get<List<IdentityResource>>(Key) ?? new();
            try
            {
                if (!result.Any())
                {
                    var identityResources = _identityResourcesBusiness.Get().ToList();

                    if (identityResources.Any())
                    {
                        var identityResourceProperties = _identityResourcesPropertiesBusiness.Get();

                        var identityResourceClaims = _identityResourceClaimsBusiness.Get();

                        identityResources.ForEach(x =>
                        {

                            var propertiesById = identityResourceProperties.Where(y => y.IdentityResources_Id == x.Id).ToList();
                            IDictionary<string, string> properties = new Dictionary<string, string>();
                            propertiesById.ForEach(y => properties.Add(y.Key, y.Value));

                            ICollection<string> claims = identityResourceClaims.Where(y => y.IdentityResources_Id == x.Id)
                                                                               .Select(y => y.Type)
                                                                               .ToList();

                            result.Add(Converts.ToIdentityResource(x,properties, claims));
                        });

                        _cache.Set(Key, result, TimeSpan.FromMinutes(_dataBaseOptions.AbsoluteExpirationRelativeToNowCache));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
                throw;
            }

            return result;
        }

        public IEnumerable<IdentityResource> Get(IEnumerable<string> scopeNames)
        {
            List<IdentityResource> result = new();

            try
            {
                var identityResource = Get();
                result.AddRange(identityResource.Where(x => scopeNames.Contains(x.Name)));

                var a = identityResource.Select(x => new { x.UserClaims, x.Name }).ToList();
                List<string> names = new();

                a.ForEach(x =>
                {
                    var v = (from db in x.UserClaims
                             join scope in scopeNames on db equals scope
                             select true).FirstOrDefault();
                    if (v)
                    {
                        names.Add(x.Name);
                    }
                });

                result.AddRange(identityResource.Where(x => names.Contains(x.Name)));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {scopesNames}", MethodBase.GetCurrentMethod().Name, scopeNames);
                throw;
            }

            return result.GroupBy(x => x.Name).Select(x => x.First()).ToList();
        }


    }
}
