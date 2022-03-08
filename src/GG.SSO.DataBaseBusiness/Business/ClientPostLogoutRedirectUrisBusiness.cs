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
    public class ClientPostLogoutRedirectUrisBusiness
    {
        private readonly ClientPostLogoutRedirectUrisSentences _clientPostLogoutRedirectUrisSentences;
        private readonly ILogger<ClientPostLogoutRedirectUrisBusiness> _logger;

        public ClientPostLogoutRedirectUrisBusiness(
            ClientPostLogoutRedirectUrisSentences clientPostLogoutRedirectUrisSentences,
            ILogger<ClientPostLogoutRedirectUrisBusiness> logger)
        {
            _clientPostLogoutRedirectUrisSentences = clientPostLogoutRedirectUrisSentences;
            _logger = logger;
        }

        public IEnumerable<ClientPostLogoutRedirectUris> Get(int client_Id)
        {
            try
            {
                return ClientPostLogoutRedirectUris.Read(_clientPostLogoutRedirectUrisSentences.AddCliteriByClient(client_Id)
                                                                                               .GetCriteriaCollection());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {client_Id}", MethodBase.GetCurrentMethod().Name, client_Id);
                throw;
            }
        }
    }
}
