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
    public class ClientClaimsBusiness
    {
        private readonly ClientClaimsSentences _clientClaimsSentences;
        private readonly ILogger<ClientClaimsBusiness> _logger;

        public ClientClaimsBusiness(
            ClientClaimsSentences clientClaimsSentences,
            ILogger<ClientClaimsBusiness> logger)
        {
            _clientClaimsSentences = clientClaimsSentences;
            _logger = logger;
        }

        public IEnumerable<ClientClaims> Get(int client_Id)
        {
            try
            {
                return ClientClaims.Read(_clientClaimsSentences.AddCliteriByClient(client_Id)
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
