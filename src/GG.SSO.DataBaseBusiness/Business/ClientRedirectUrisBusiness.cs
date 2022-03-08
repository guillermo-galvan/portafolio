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
    public class ClientRedirectUrisBusiness
    {
        private readonly ClientRedirectUrisSentences _clientRedirectUrisSentences;
        private readonly ILogger<ClientRedirectUrisBusiness> _logger;

        public ClientRedirectUrisBusiness(
            ClientRedirectUrisSentences clientRedirectUrisSentences,
            ILogger<ClientRedirectUrisBusiness> logger)
        {
            _clientRedirectUrisSentences = clientRedirectUrisSentences;
            _logger = logger;
        }

        public IEnumerable<ClientRedirectUris> Get(int client_Id)
        {
            try
            {
                return ClientRedirectUris.Read(_clientRedirectUrisSentences.AddCliteriByClient(client_Id)
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
