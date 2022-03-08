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
    public class ClientGrantTypesBusiness
    {
        private readonly ClientGrantTypesSentences _clientGrantTypesSentences;
        private readonly ILogger<ClientGrantTypesBusiness> _logger;

        public ClientGrantTypesBusiness(ClientGrantTypesSentences clientGrantTypesSentences, 
            ILogger<ClientGrantTypesBusiness> logger)
        {
            _clientGrantTypesSentences = clientGrantTypesSentences;
            _logger = logger;
        }

        public IEnumerable<ClientGrantTypes> Get(int client_Id)        
        {
            try
            {
                return ClientGrantTypes.Read(_clientGrantTypesSentences.AddCliteriByClient(client_Id)
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
