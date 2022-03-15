using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using GG.SSO.BusinesLogic.IdentityServer;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GG.SSO.Store.IdentityServer
{
    public class SigningKeyStore : ISigningKeyStore
    {
        private readonly KeysManagement _keysManagement;
        private readonly ILogger<SigningKeyStore> _logger;

        public SigningKeyStore(KeysManagement keysManagement, ILogger<SigningKeyStore> logger)
        {
            _keysManagement = keysManagement;
            _logger = logger;
        }

        public Task DeleteKeyAsync(string id)
        {
            try
            {
                SerializedKey serializedKey = _keysManagement.Get(id);

                if (serializedKey == null)
                { 
                    _keysManagement.Delete(serializedKey);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {clientId}", MethodBase.GetCurrentMethod().Name, id);
            }

            return Task.CompletedTask;
        }

        public Task<IEnumerable<SerializedKey>> LoadKeysAsync()
        {
            try
            {
                return Task.FromResult(_keysManagement.Get());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
                return Task.FromResult(Array.Empty<SerializedKey>().AsEnumerable());
            }
        }

        public Task StoreKeyAsync(SerializedKey key)
        {
            try
            {
                _keysManagement.Insert(key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {key}", MethodBase.GetCurrentMethod().Name, key);
            }

            return Task.CompletedTask;
        }
    }
}
