using Duende.IdentityServer.Models;
using GG.SSO.BusinesLogic.Helpers;
using GG.SSO.DataBaseBusiness;
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
    public class ApiScopesManagement
    {
        const string Key = "ApiScopes";
        private readonly ILogger<ApiScopesManagement> _logger;
        private readonly IMemoryCache _cache;
        private readonly DataBaseOptions _dataBaseOptions;
        private readonly ApiScopesBusiness _apiScopesBusiness;
        private readonly ApiScopeClaimsBusiness _apiScopeClaimsBusiness;
        private readonly ApiScopePropertiesBusiness _apiScopePropertiesBusiness;

        public ApiScopesManagement(ILogger<ApiScopesManagement> logger, IMemoryCache cache, DataBaseOptions dataBaseOptions,
            ApiScopesBusiness apiScopesBusiness, ApiScopeClaimsBusiness apiScopeClaimsBusiness,
            ApiScopePropertiesBusiness apiScopePropertiesBusiness)
        {
            _logger = logger;
            _cache = cache;
            _dataBaseOptions = dataBaseOptions;
            _apiScopesBusiness = apiScopesBusiness;
            _apiScopeClaimsBusiness = apiScopeClaimsBusiness;
            _apiScopePropertiesBusiness = apiScopePropertiesBusiness;
        }

        public IEnumerable<ApiScope> GetApiScope()
        {
            List<ApiScope> result = _cache.Get<List<ApiScope>>(Key) ?? new();

            try
            {
                if (!result.Any())
                {
                    var apiScopes = _apiScopesBusiness.Get().ToList();

                    if (apiScopes.Any())
                    {
                        var ids = apiScopes.GroupBy(x => x.Id).Select(x => x.Key);
                        var scopClaim = _apiScopeClaimsBusiness.Get(ids);

                        var scopeProperties = _apiScopePropertiesBusiness.Get(ids);

                        apiScopes.ForEach(x => {

                            var propertiesById = scopeProperties.Where(y => y.ApiScopes_Id == x.Id).ToList();
                            IDictionary<string, string> properties = new Dictionary<string, string>();
                            propertiesById.ForEach(y => properties.Add(y.Key, y.Value));

                            ICollection<string> claims = scopClaim.Where(y => y.ApiScopes_Id == x.Id)
                                                                  .Select(y => y.Type)
                                                                  .ToList();

                            result.Add(Converts.ToApiScope(x, properties, claims));
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

        public IEnumerable<ApiScope> GetApiScope(IEnumerable<string> scopeNames)
        {
            List<ApiScope> result = new();

            try
            {
                var apiScopes = GetApiScope();

                result.AddRange(apiScopes.Where(x => scopeNames.Contains(x.Name)));

                var a = apiScopes.Select(x => new { x.UserClaims, x.Name }).ToList();
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

                result.AddRange(apiScopes.Where(x => names.Contains(x.Name)));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {scopeNames}", MethodBase.GetCurrentMethod().Name, scopeNames);
                throw;
            }

            return result.GroupBy(x => x.Name).Select(x => x.First()).ToList();
        }
    }
}
