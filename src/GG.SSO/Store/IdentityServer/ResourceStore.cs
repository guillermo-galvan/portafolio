using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using GGPuntoYComa.SSO.BusinesLogic.IdentityServer;
using GGPuntoYComa.SSO.DataBaseBusiness.Business;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GGPuntoYComa.SSO.Store.IdentityServer
{
    public class ResourceStore : IResourceStore
    {
        private readonly ILogger _logger;
        private readonly IdentityResourcesManagement _identityResourcesManagement;
        private readonly ApiScopesManagement _apiScopesManagement;
        private readonly ApiResourcesManagement _apiResourcesManagement;

        public ResourceStore(ILogger<ResourceStore> logger, IdentityResourcesManagement identityResourcesManagement,
            ApiScopesManagement apiScopesManagement, ApiResourcesManagement apiResourcesManagement)
        {
            _logger = logger;
            _identityResourcesManagement = identityResourcesManagement;
            _apiScopesManagement = apiScopesManagement;
            _apiResourcesManagement = apiResourcesManagement;
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            try
            {
                return Task.FromResult(_apiResourcesManagement.GetApiResourceByName(apiResourceNames));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FindApiResourcesByNameAsync {0}", apiResourceNames);
                return Task.FromResult(Array.Empty<ApiResource>().AsEnumerable());
            }
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            try
            {
                return Task.FromResult(_apiResourcesManagement.GetApiResource(scopeNames));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FindApiResourcesByScopeNameAsync {0}", scopeNames);
                return Task.FromResult(Array.Empty<ApiResource>().AsEnumerable());
            }
        }

        public Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            try
            {
                return Task.FromResult(_apiScopesManagement.GetApiScope(scopeNames));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FindIdentityResourcesByScopeNameAsync {0}", scopeNames);
                return Task.FromResult(Array.Empty<ApiScope>().AsEnumerable());
            }
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            try
            {
                return Task.FromResult(_identityResourcesManagement.Get(scopeNames));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FindIdentityResourcesByScopeNameAsync {0}", scopeNames);
                return Task.FromResult(Array.Empty<IdentityResource>().AsEnumerable());
            }
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            try
            {
                var result = new Resources(_identityResourcesManagement.Get(),
                    _apiResourcesManagement.GetApiResource(), _apiScopesManagement.GetApiScope());
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllResourcesAsync");
                return Task.FromResult(new Resources());
            }
        }
    }
}
