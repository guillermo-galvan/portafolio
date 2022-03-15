using GG.SSO.DataBaseBusiness.Sentences;
using GG.SSO.Entity.Table.Sso;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GG.SSO.DataBaseBusiness.Business
{
    public class PersistedGrantsBusiness
    {
        readonly PersistedGrantsSentences _persistedGrantsSentences;
        readonly ILogger<PersistedGrantsBusiness> _logger;

        public PersistedGrantsBusiness(PersistedGrantsSentences persistedGrantsSentences, 
            ILogger<PersistedGrantsBusiness> logger)
        {
            _persistedGrantsSentences = persistedGrantsSentences;
            _logger = logger;
        }

        private void GetCriteria((string SubjectId, string SessionId, string ClientId, string Type) filter)
        {
            _persistedGrantsSentences.AddCliteriByClienteId(filter.ClientId)
                                     .AddCliteriBySessionId(filter.SessionId)
                                     .AddCliteriBySubjectId(filter.SubjectId)
                                     .AddCliteriByType(filter.Type);
        }

        private IDictionary<string, object> GetKeyValuesByDelete()
        { 
            return new Dictionary<string, object>
                {
                    { nameof(PersistedGrants.IsDeleted), true }
                };
        }

        public IEnumerable<PersistedGrants> Get((string SubjectId, string SessionId, string ClientId, string Type) filter)
        {
            try
            {
                GetCriteria(filter);               
                return PersistedGrants.Read(_persistedGrantsSentences.AddCliteriByIsDeleted(false).GetCriteriaCollection());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {filter}", MethodBase.GetCurrentMethod().Name,filter);
                throw;
            }
        }

        public PersistedGrants Get(string key)
        {
            try
            {
               return PersistedGrants.Read(_persistedGrantsSentences.AddCliteriByKey(key)
                                                                    .AddCliteriByIsDeleted(false)
                                                                    .GetCriteriaCollection())
                                     .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {key}", MethodBase.GetCurrentMethod().Name, key);
                throw;
            }
        }

        public void Remove((string SubjectId, string SessionId, string ClientId, string Type) filter)
        {
            try
            {
                GetCriteria(filter);
                PersistedGrants.Update(GetKeyValuesByDelete(), _persistedGrantsSentences.GetCriteriaCollection());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Nmae} {filter}", MethodBase.GetCurrentMethod().Name,filter);
                throw;
            }
        }

        public void Remove(string key)
        {
            try
            {
                _persistedGrantsSentences.AddCliteriByKey(key)
                                         .AddCliteriByIsDeleted(false);
                PersistedGrants.Update(GetKeyValuesByDelete(), _persistedGrantsSentences.GetCriteriaCollection());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {key}", MethodBase.GetCurrentMethod().Name,key);
                throw;
            }
        }

        public void Insert(PersistedGrants grant)
        {
            try
            {
                grant.Insert();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {grant}",MethodBase.GetCurrentMethod().Name, grant);
                throw;
            }
        }
    }
}
