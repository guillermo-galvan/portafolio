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
    public class ClientScopesBusiness
    {
        private readonly ClientScopesSentences _clientScopesSentences;
        private readonly ILogger<ClientScopesBusiness> _logger;

        public ClientScopesBusiness(
            ClientScopesSentences clientScopesSentences,
            ILogger<ClientScopesBusiness> logger)
        {
            _clientScopesSentences = clientScopesSentences;
            _logger = logger;
        }

        public IEnumerable<ClientScopes> Get(int client_Id)
        {
            try
            {
                return ClientScopes.Read(_clientScopesSentences.AddCliteriByClient(client_Id)
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
