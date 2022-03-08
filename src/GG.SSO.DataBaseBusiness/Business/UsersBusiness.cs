using GG.SSO.DataBaseBusiness.Sentences;
using GG.SSO.Entity.Table.Sso;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Business
{
    public class UsersBusiness
    {
        private readonly UsersSentences _usersSentences;
        private readonly ILogger<UsersBusiness> _logger;

        public UsersBusiness(UsersSentences usersSentences, ILogger<UsersBusiness> logger)
        {
            _logger = logger;
            _usersSentences = usersSentences;
        }

        public void Insert(Users user)
        {
            try
            {
                user.Insert();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Insert {user}", user);
                throw;
            }
        }

        public void Delete(Users user)
        {
            try
            {
                user.Delete();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete {user}", user);
                throw;
            }
        }

        public void Update(Users user)
        {
            try
            {
                user.Update();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update {user}", user);
                throw;
            }
        }

        public Users Get(string usersId)
        {
            try
            {
                return Users.Read(_usersSentences.AddCliteriById(usersId).GetCriteriaCollection())
                            .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get {usersId}", usersId);
                throw;
            }
        }

        public Users GetByNormalizedUserName(string normalizedEmail)
        {
            try
            {
                return Users.Read(_usersSentences.AddCliteriByNormalizedUserName(normalizedEmail).GetCriteriaCollection())
                            .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetByNormalizedUserName {usersId}", normalizedEmail);
                throw;
            }
        }

        public Users GetByNormalizedEmail(string normalizedEmail)
        {
            try
            {
                return Users.Read(_usersSentences.AddCliteriByNormalizedEmail(normalizedEmail).GetCriteriaCollection())
                            .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetByNormalizedEmail {normalizedEmail}", normalizedEmail);
                throw;
            }
        }

        public IEnumerable<Users> Get(IEnumerable<string> usersId)
        {
            try
            {
                return usersId.Any() ? Users.Read(_usersSentences.AddInCliteriById(usersId).GetCriteriaCollection())
                    : new List<Users>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get {usersId}", usersId);
                throw;
            }
        }

    }
}
