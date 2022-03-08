using GG.SSO.DataBaseBusiness.Sentences;
using GG.SSO.Entity.Table.Sso;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;

namespace GG.SSO.DataBaseBusiness.Business
{
    public class ClientsBusiness
    {
        private readonly ILogger<ClientsBusiness> _logger;        
        private readonly ClientsSentences _clientsSentences;

        public ClientsBusiness(ILogger<ClientsBusiness> logger,
           ClientsSentences clientsSentences)
        {
            _logger = logger;            
            _clientsSentences = clientsSentences;
        }

        public Clients Get(string clientId)
        {
            try
            {
                return Clients.Read(_clientsSentences.AddCliteriByClient(clientId)
                                                     .AddCliteriByIsDeleted(false)
                                                     .GetCriteriaCollection()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {clientId}", MethodInfo.GetCurrentMethod().Name, clientId);
                throw;
            }
        }

        public void Update(Clients clients)
        {
            try
            {
                clients.Update();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {clients}", MethodInfo.GetCurrentMethod().Name, clients);
                throw;
            }
        }
    }
}
