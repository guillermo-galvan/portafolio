using GG.Portafolio.DataBaseBusiness.Sentences;
using GG.Portafolio.Entity.Table.Blog;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;

namespace GG.Portafolio.DataBaseBusiness.Business
{
    public class UserBusiness
    {
        private readonly UserSentences _userSentences;
        private readonly ILogger<UserBusiness> _logger;

        public UserBusiness(UserSentences userSentences, ILogger<UserBusiness> logger)
        {
            _userSentences = userSentences;
            _logger = logger;
        }

        public User GetUser(string id, string email, string name)
        {
            try
            {
                return User.Read(_userSentences.AddCliteriById(id)
                                               .AddCliteriByEmail(email)
                                               .AddCliteriByName(name)
                                               .GetCriteriaCollection()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} Id:{id} Email:{email} Name:{name}", MethodBase.GetCurrentMethod().Name, id, email, name);
                throw;
            }
        }

        public User GetUserById(string id)
        {
            try
            {
                return User.Read(_userSentences.AddCliteriById(id).GetCriteriaCollection()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} Id:{id}", MethodBase.GetCurrentMethod().Name, id);
                throw;
            }
        }
    }
}
