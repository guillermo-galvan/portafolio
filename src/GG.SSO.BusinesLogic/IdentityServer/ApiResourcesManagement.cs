using Duende.IdentityServer.Models;
using GG.SSO.DataBaseBusiness;
using GG.SSO.BusinesLogic.Helpers;
using GG.SSO.DataBaseBusiness.Business;
using GG.SSO.DataBaseBusiness.Sentences;
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
    public class ApiResourcesManagement
    {
        const string key = "ApiResources";
        private readonly ILogger<ApiResourcesManagement> _logger;
        private readonly IMemoryCache _cache;
        private readonly DataBaseOptions _dataBaseOptions;
        private readonly ApiResourcesBusiness _apiResourcesBusiness;
        private readonly ApiResourceScopesBusiness _apiResourceScopesBusiness;
        private readonly ApiResourceSecretsBusiness _apiResourceSecretsBusiness;
        private readonly ApiResourceClaimsBusiness _apiResourceClaimsBusiness;
        private readonly ApiResourcePropertiesBusiness _apiResourcePropertiesBusiness;

        public ApiResourcesManagement(ILogger<ApiResourcesManagement> logger, IMemoryCache cache, DataBaseOptions dataBaseOptions,
            ApiResourcesBusiness apiResourcesBusiness, ApiResourceScopesBusiness apiResourceScopesBusiness,
            ApiResourceSecretsBusiness apiResourceSecretsBusiness, ApiResourceClaimsBusiness apiResourceClaimsBusiness,
            ApiResourcePropertiesBusiness apiResourcePropertiesBusiness)
        {
            _logger = logger;
            _cache = cache;
            _dataBaseOptions = dataBaseOptions;
            _apiResourcesBusiness = apiResourcesBusiness;
            _apiResourceScopesBusiness = apiResourceScopesBusiness;
            _apiResourceSecretsBusiness = apiResourceSecretsBusiness;
            _apiResourceClaimsBusiness = apiResourceClaimsBusiness;
            _apiResourcePropertiesBusiness = apiResourcePropertiesBusiness;
        }

        public IEnumerable<ApiResource> GetApiResource()
        {
            List<ApiResource> result = _cache.Get<List<ApiResource>>(key) ?? new();

            try
            {
                if (!result.Any())
                {
                    var apiResources = _apiResourcesBusiness.Get().ToList();

                    if (apiResources.Any())
                    {
                        var ids = apiResources.GroupBy(x => x.Id).Select(x => x.Key);
                        var apiResourceScopes = _apiResourceScopesBusiness.Get(ids);

                        var apiResourceSecrets = _apiResourceSecretsBusiness.Get(ids);

                        var apiResourceClaims = _apiResourceClaimsBusiness.Get(ids);

                        var apiResourceProperties = _apiResourcePropertiesBusiness.Get(ids);

                        apiResources.ForEach(x => {

                            var propertiesById = apiResourceProperties.Where(y => y.ApiResources_Id == x.Id).ToList();
                            IDictionary<string, string> properties = new Dictionary<string, string>();
                            propertiesById.ForEach(y => properties.Add(y.Key, y.Value));


                            ICollection<string> scope = apiResourceScopes.Where(y => y.ApiResources_Id == x.Id)
                                                                .Select(y => y.Scope)
                                                                .ToList();

                            ICollection<Secret> secrets = apiResourceSecrets.Where(y => y.ApiResources_Id == x.Id)
                                                                            .Select(y => Converts.ToSecret(y))
                                                                            .ToList();

                            ICollection<string> claims = apiResourceClaims.Where(y => y.ApiResources_Id == x.Id)
                                                                 .Select(y => y.Type)
                                                                 .ToList();


                            result.Add(Converts.ToApiResource(x,scope,properties,secrets,claims));
                        });

                        _cache.Set(key, result, TimeSpan.FromMinutes(_dataBaseOptions.AbsoluteExpirationRelativeToNowCache));
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

        public IEnumerable<ApiResource> GetApiResource(IEnumerable<string> scopeNames)
        {
            List<ApiResource> result = new();

            try
            {
                var apiResources = GetApiResource();

                result.AddRange(apiResources.Where(x => x.Scopes.Where(y => scopeNames.Contains(y)).Any()));

                var a = apiResources.Select(x => new { x.UserClaims, x.Name }).ToList();
                List<string> names = new();

                a.ForEach(x => {
                    var v = (from db in x.UserClaims
                             join scope in scopeNames on db equals scope
                             select true).FirstOrDefault();
                    if (v)
                    {
                        names.Add(x.Name);
                    }
                });

                result.AddRange(apiResources.Where(x => names.Contains(x.Name)));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {scopeNames}", MethodBase.GetCurrentMethod().Name,scopeNames);
                throw;
            }

            return result.GroupBy(x => x.Name).Select(x => x.First()).ToList();
        }

        public IEnumerable<ApiResource> GetApiResourceByName(IEnumerable<string> apiResourceNames)
        {
            List<ApiResource> result = new();

            try
            {
                var apiResources = GetApiResource();

                result.AddRange(apiResources.Where(x => apiResourceNames.Contains(x.Name)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {scopeNames}", MethodBase.GetCurrentMethod().Name,apiResourceNames);
                throw;
            }

            return result.GroupBy(x => x.Name).Select(x => x.First()).ToList();
        }

    }
}
