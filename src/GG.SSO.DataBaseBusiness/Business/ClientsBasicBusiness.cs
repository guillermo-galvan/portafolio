using GGPuntoYComa.SSO.DataBaseBusiness.Sentences;
using GGPuntoYComa.SSO.Entity.Table.Sso;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGPuntoYComa.SSO.DataBaseBusiness.Business
{
    public class ClientsBasicBusiness
    {
        //ClientsBasic
        private readonly ClientsBasicSentences _clientsBasicSentences;
        private readonly ILogger<ClientsBasicBusiness> _logger;

        public ClientsBasicBusiness(ILogger<ClientsBasicBusiness> logger, ClientsBasicSentences clientsBasicSentences)
        {
            _logger = logger;
            _clientsBasicSentences = clientsBasicSentences;
        }

        public IEnumerable<ClientsBasic> Get(IEnumerable<int> ids)
        {
            try
            {
                return ids.Any() ? ClientsBasic.Read(_clientsBasicSentences.AddInCliteriById(ids)
                                                                                 .GetCriteriaCollection())
                    : new List<ClientsBasic>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get {clientIds}", ids);
                throw;
            }
        }

        public ClientsBasic Get(string clientId)
        {
            try
            {
                return ClientsBasic.Read(_clientsBasicSentences.AddCliteriByClientId(clientId)
                                                               .GetCriteriaCollection())
                                   .FirstOrDefault();
                    
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get {clientId}", clientId);
                throw;
            }
        }

        public IEnumerable<ClientsBasic> Get()
        {
            try
            {
                return ClientsBasic.Read(_clientsBasicSentences.AddCliteriByIsDeleted(false)
                                                               .GetCriteriaCollection());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get");
                throw;
            }
        }
    }
}
