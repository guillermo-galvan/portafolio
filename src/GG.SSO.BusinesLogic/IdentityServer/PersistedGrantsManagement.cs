using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using GG.SSO.BusinesLogic.Helpers;
using GG.SSO.DataBaseBusiness.Business;

namespace GG.SSO.BusinesLogic.IdentityServer
{
    public class PersistedGrantsManagement
    {
        private readonly ILogger<PersistedGrantsManagement> _logger;
        private readonly PersistedGrantsBusiness _persistedGrantsBusiness;

        public PersistedGrantsManagement(ILogger<PersistedGrantsManagement> logger, PersistedGrantsBusiness business)
        {
            _logger = logger;
            _persistedGrantsBusiness = business;
        }

        private (string SubjectId, string SessionId, string ClientId, string Type) ConvertToFiler(PersistedGrantFilter filter)
        {
            return (filter.SubjectId, filter.SessionId, filter.ClientId, filter.Type);
        }

        public IEnumerable<PersistedGrant> GetAll(PersistedGrantFilter filter)
        {
            try
            {
               return _persistedGrantsBusiness.Get(ConvertToFiler(filter)).Select(x => Converts.ToPersistedGrant(x));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {filter}", MethodBase.GetCurrentMethod().Name,filter);
                return Array.Empty<PersistedGrant>().AsEnumerable();
            }
        }

        public PersistedGrant Get(string key)
        {
            try
            {
                return Converts.ToPersistedGrant(_persistedGrantsBusiness.Get(key));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {key}", MethodBase.GetCurrentMethod().Name,key);
                throw;
            }
        }

        public void RemoveAll(PersistedGrantFilter filter)
        {
            try
            {
                _persistedGrantsBusiness.Remove(ConvertToFiler(filter));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {filter}", MethodBase.GetCurrentMethod().Name,filter);
                throw;
            }
        }

        public void Remove(string key)
        {
            try
            {
                _persistedGrantsBusiness.Remove(key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {key}", MethodBase.GetCurrentMethod().Name,key);
                throw;
            }
        }

        public void Insert(PersistedGrant grant)
        {

            try
            {
                _persistedGrantsBusiness.Insert(Converts.ToPersistedGrants(grant));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {grant}", MethodBase.GetCurrentMethod().Name,grant);
                throw;
            }
        }
    }
}
