using GG.SSO.DataBaseBusiness.Sentences;
using GG.SSO.Entity.Table.Sso;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace GG.SSO.DataBaseBusiness.Business
{
    public class ClientPropertiesBusiness
    {
        private readonly ClientPropertiesSentences _clientPropertiesSentences;
        private readonly ILogger<ClientPropertiesBusiness> _logger;

        public ClientPropertiesBusiness(ClientPropertiesSentences clientPropertiesSentences, 
            ILogger<ClientPropertiesBusiness> logger)
        {
            _clientPropertiesSentences = clientPropertiesSentences;
            _logger = logger;
        }

        public IEnumerable<ClientProperties> Get(int client_Id)
        {
            try
            {
                return ClientProperties.Read(_clientPropertiesSentences.AddCliteriByClient(client_Id)
                                                                       .GetCriteriaCollection());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {client_Id}", MethodBase.GetCurrentMethod().Name,client_Id);
                throw;
            }
        }
    }
}
