using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using GG.SSO.BusinesLogic.IdentityServer;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GG.SSO.Store.IdentityServer
{
    public class PersistedGrantStore : IPersistedGrantStore
    {
        private readonly ILogger<PersistedGrantStore> _logger;
        private readonly PersistedGrantsManagement _persistedGrantsManagement;

        public PersistedGrantStore(ILogger<PersistedGrantStore> logger, 
            PersistedGrantsManagement persistedGrantsManagement)
        {
            _logger = logger;
            _persistedGrantsManagement = persistedGrantsManagement;
        }

        public Task<IEnumerable<PersistedGrant>> GetAllAsync(PersistedGrantFilter filter)
        {
            try
            {
                return Task.FromResult(_persistedGrantsManagement.GetAll(filter));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllAsync {filter}", filter);
                return Task.FromResult(Array.Empty<PersistedGrant>().AsEnumerable());
            }
        }

        public Task<PersistedGrant> GetAsync(string key)
        {
            try
            {
                return Task.FromResult(_persistedGrantsManagement.Get(key));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAsync {key}", key);
                return Task.FromResult<PersistedGrant>(null);
            }
        }

        public Task RemoveAllAsync(PersistedGrantFilter filter)
        {
            try
            {
                _persistedGrantsManagement.RemoveAll(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RemoveAllAsync {filter}", filter);
            }

            return Task.CompletedTask;
        }

        public Task RemoveAsync(string key)
        {
            try
            {
                _persistedGrantsManagement.Remove(key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RemoveAsync {key}", key);
            }

            return Task.CompletedTask;
        }

        public Task StoreAsync(PersistedGrant grant)
        {

            try
            {
                var grantExists = _persistedGrantsManagement.Get(grant.Key);

                if (grantExists == null)
                {
                    _persistedGrantsManagement.Insert(grant);
                }
                else
                {
                    _persistedGrantsManagement.Remove(grantExists.Key);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "StoreAsync {grant}", grant);
            }

            return Task.CompletedTask;
        }
    }
}
