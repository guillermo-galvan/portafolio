using GGPuntoYComa.SSO.DataBaseBusiness.Sentences;
using GGPuntoYComa.SSO.Entity.Table.Sso;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GGPuntoYComa.SSO.DataBaseBusiness.Business
{
    public class ClientSecretsBusiness
    {
        private readonly ClientSecretsSentences _clientSecretsSentences;
        private readonly ILogger<ClientSecretsBusiness> _logger;

        public ClientSecretsBusiness(
            ClientSecretsSentences clientSecretsSentences,
            ILogger<ClientSecretsBusiness> logger)
        {
            _clientSecretsSentences = clientSecretsSentences;
            _logger = logger;
        }

        public IEnumerable<ClientSecrets> Get(int client_Id)
        {
            try
            {
                return ClientSecrets.Read(_clientSecretsSentences.AddCliteriByClient(client_Id)
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
