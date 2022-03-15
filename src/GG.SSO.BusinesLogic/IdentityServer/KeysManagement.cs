using Duende.IdentityServer.Models;
using GG.SSO.BusinesLogic.Helpers;
using GG.SSO.DataBaseBusiness.Business;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.BusinesLogic.IdentityServer
{
    public class KeysManagement
    {
        private readonly KeysBusiness _keysBusiness;
        private readonly ILogger<KeysManagement> _logger;

        public KeysManagement(ILogger<KeysManagement> logger, KeysBusiness keysBusiness)
        {
            _logger = logger;
            _keysBusiness = keysBusiness;
        }

        public SerializedKey Get(string id)
        {
            try
            {
                var result = _keysBusiness.Get(id);

                return result == null ? null : Converts.ToSerializedKey(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {id}", MethodBase.GetCurrentMethod().Name, id);
                throw;
            }
        }

        public IEnumerable<SerializedKey> Get()
        {
            try
            {
                return _keysBusiness.Get().Select(x => Converts.ToSerializedKey(x));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        public void Insert(SerializedKey serializedKey)
        {
            try
            {
                _keysBusiness.Insert(Converts.ToKey(serializedKey));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {serializedKey}", MethodBase.GetCurrentMethod().Name, serializedKey);
                throw;
            }
        }

        public void Delete(SerializedKey serializedKey)
        {
            try
            {
                _keysBusiness.Delete(Converts.ToKey(serializedKey));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {serializedKey}", MethodBase.GetCurrentMethod().Name, serializedKey);
                throw;
            }
        }

    }
}
