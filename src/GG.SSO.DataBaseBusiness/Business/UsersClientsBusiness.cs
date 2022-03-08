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
    public class UsersClientsBusiness
    {
        private readonly UsersClientsSentences _usersClientsSentences;
        private readonly ILogger<UsersClientsBusiness> _logger;

        public UsersClientsBusiness(ILogger<UsersClientsBusiness> logger, UsersClientsSentences usersClientsSentences)
        {
            _usersClientsSentences = usersClientsSentences;
            _logger = logger;
        }

        public void Insert(UsersClients usersClients)
        {
            try
            {
                usersClients.Insert();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Insert {usersClients}", usersClients);
                throw;
            }
        }

        public void Delete(UsersClients usersClients)
        {
            try
            {
                usersClients.Delete();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete {usersClients}", usersClients);
                throw;
            }
        }

        public void Update(UsersClients usersClients)
        {
            try
            {
                usersClients.Update();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update {usersClients}", usersClients);
                throw;
            }
        }

        public IEnumerable<UsersClients> Get(string usersId)
        {
            try
            {
                return UsersClients.Read(_usersClientsSentences.AddCliteriByUsers_Id(usersId).GetCriteriaCollection());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get {usersId}", usersId);
                throw;
            }
        }

        public IEnumerable<UsersClients> Get(IEnumerable<string> usersId)
        {
            try
            {
                return usersId.Any() ? UsersClients.Read(_usersClientsSentences.AddInCliteriByUsers_Id(usersId)
                                                                               .GetCriteriaCollection())
                    : new List<UsersClients>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get {usersId}", usersId);
                throw;
            }
        }

    }
}
