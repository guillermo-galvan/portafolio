using GGPuntoYComa.SSO.DataBaseBusiness.Sentences;
using GGPuntoYComa.SSO.Entity.Table.Sso;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GGPuntoYComa.SSO.DataBaseBusiness.Business
{
    public class UserExternalLoginsBussines
    {
        private readonly UserExternalLoginsSentences _userExternalLoginsSentences;
        private readonly ILogger<UserExternalLoginsBussines> _logger;

        public UserExternalLoginsBussines(UserExternalLoginsSentences userExternalLoginsSentences, 
            ILogger<UserExternalLoginsBussines> logger)
        {
            _logger = logger;
            _userExternalLoginsSentences = userExternalLoginsSentences;
        }

        public void Insert(UserExternalLogins userExternalLogins)
        {
            try
            {
                userExternalLogins.Insert();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{methodName} {userExternalLogins}", 
                    MethodBase.GetCurrentMethod().Name,userExternalLogins);
                throw;
            }
        }

        public void Delete(UserExternalLogins userExternalLogins)
        {
            try
            {
                userExternalLogins.Delete();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{methodName} {userExternalLogins}",
                    MethodBase.GetCurrentMethod().Name, userExternalLogins);
                throw;
            }
        }

        public void Update(UserExternalLogins userExternalLogins)
        {
            try
            {
                userExternalLogins.Update();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{methodName} {userExternalLogins}",
                    MethodBase.GetCurrentMethod().Name, userExternalLogins);
                throw;
            }
        }

        public UserExternalLogins Get(string user_id, string loginProvider, string providerKey)
        {
            try
            {
                return UserExternalLogins.Read(_userExternalLoginsSentences.AddCliteriByUsers_Id(user_id)
                                                                           .AddCliteriByLoginProvider(loginProvider)
                                                                           .AddCliteriByProviderKey(providerKey)
                                                                           .GetCriteriaCollection())
                                         .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{methodName} {user_id} {loginProvider} {providerKey}", 
                    MethodBase.GetCurrentMethod().Name, user_id, loginProvider, providerKey);
                throw;
            }
        }

        public IEnumerable<UserExternalLogins> Get(string user_id)
        {
            try
            {
                return UserExternalLogins.Read(_userExternalLoginsSentences.AddCliteriByUsers_Id(user_id)
                                                                           .GetCriteriaCollection());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{methodName} {user_id}",
                    MethodBase.GetCurrentMethod().Name, user_id);
                throw;
            }
        }

        public UserExternalLogins Get(string loginProvider, string providerKey)
        {
            try
            {
                return UserExternalLogins.Read(_userExternalLoginsSentences.AddCliteriByLoginProvider(loginProvider)
                                                                           .AddCliteriByProviderKey(providerKey)
                                                                           .GetCriteriaCollection()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{methodName} {loginProvider} {providerKey}",
                    MethodBase.GetCurrentMethod().Name, loginProvider, providerKey);
                throw;
            }
        }

    }
}
