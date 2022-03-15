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
    public class UserTokensBusiness
    {
        private readonly UserTokensSentences _userTokensSentences;
        private readonly ILogger<UserTokensBusiness> _logger;

        public UserTokensBusiness(UserTokensSentences userTokensSentences, ILogger<UserTokensBusiness> logger )
        {
            _logger = logger;
            _userTokensSentences = userTokensSentences;
        }

        public void Insert(UserTokens userTokens)
        {
            try
            {
                userTokens.Insert();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{methodName} {userTokens}",
                    MethodBase.GetCurrentMethod().Name, userTokens);
                throw;
            }
        }

        public void Delete(UserTokens userTokens)
        {
            try
            {
                userTokens.Delete();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{methodName} {userTokens}",
                    MethodBase.GetCurrentMethod().Name, userTokens);
                throw;
            }
        }

        public void Update(UserTokens userTokens)
        {
            try
            {
                userTokens.Update();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{methodName} {userTokens}",
                    MethodBase.GetCurrentMethod().Name, userTokens);
                throw;
            }
        }

        public UserTokens Get(string user_id, string loginProvider, string name, string value)
        {
            try
            {
                return UserTokens.Read(_userTokensSentences.AddCliteriByUsers_Id(user_id)
                                                           .AddCliteriByLoginProvider(loginProvider)
                                                           .AddCliteriByName(name)
                                                           .AddCliteriByValue(value)
                                                           .GetCriteriaCollection())
                                 .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{methodName} {user_id} {loginProvider} {name} {value}",
                    MethodBase.GetCurrentMethod().Name, user_id, loginProvider, name, value);
                throw;
            }
        }

        public UserTokens Get(string user_id, string loginProvider, string name)
        {
            try
            {
                return UserTokens.Read(_userTokensSentences.AddCliteriByUsers_Id(user_id)
                                                           .AddCliteriByLoginProvider(loginProvider)
                                                           .AddCliteriByName(name)
                                                           .GetCriteriaCollection())
                                 .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{methodName} {user_id} {loginProvider} {name}",
                    MethodBase.GetCurrentMethod().Name, user_id, loginProvider, name);
                throw;
            }
        }
    }
}
