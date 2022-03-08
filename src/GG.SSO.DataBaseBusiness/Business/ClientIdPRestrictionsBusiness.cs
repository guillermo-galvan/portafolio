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
    public class ClientIdPRestrictionsBusiness
    {
        private readonly ClientIdPRestrictionsSentences _clientIdPRestrictionsSentences;
        private readonly ILogger<ClientIdPRestrictionsBusiness> _logger;

        public ClientIdPRestrictionsBusiness(ClientIdPRestrictionsSentences clientIdPRestrictionsSentences, 
            ILogger<ClientIdPRestrictionsBusiness> logger)
        {
            _clientIdPRestrictionsSentences = clientIdPRestrictionsSentences;
            _logger = logger;
        }

        public IEnumerable<ClientIdPRestrictions> Get(int client_Id)
        {
            try
            {
                return ClientIdPRestrictions.Read(_clientIdPRestrictionsSentences.AddCliteriByClient(client_Id)
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
