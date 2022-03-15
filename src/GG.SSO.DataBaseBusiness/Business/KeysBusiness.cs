using GG.SSO.DataBaseBusiness.Sentences;
using GG.SSO.Entity.Table.Sso;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Business
{
    public class KeysBusiness
    {
        private readonly ILogger<KeysBusiness> _logger;
        private readonly KeysSentences _keysSentences;

        public KeysBusiness(ILogger<KeysBusiness> logger, KeysSentences keysSentences)
        {
            _keysSentences = keysSentences;
            _logger = logger;
        }

        public IEnumerable<Keys> Get()
        {
            try
            {
                return Keys.Read();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
                throw;
            }
        }

        public Keys Get(string id)
        {
            try
            {
                return Keys.Read(_keysSentences.AddCliteriById(id).GetCriteriaCollection()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {id}", MethodBase.GetCurrentMethod().Name, id);
                throw;
            }
        }

        public void Insert(Keys keys)
        {
            try
            {
                keys.Insert();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {role}", MethodBase.GetCurrentMethod().Name, keys);
                throw;
            }
        }

        public void Delete(Keys keys)
        {
            try
            {
                keys.Delete();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {role}", MethodBase.GetCurrentMethod().Name, keys);
                throw;
            }
        }
    }
}
