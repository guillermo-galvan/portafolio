using GGPuntoYComa.SSO.DataBaseBusiness.Sentences;
using GGPuntoYComa.SSO.Entity.Table.Sso;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GGPuntoYComa.SSO.DataBaseBusiness.Business
{
    public class ClientCorsOriginsBusiness
    {
        private readonly ILogger<ClientCorsOriginsBusiness> _logger;       
        private readonly ClientCorsOriginsSentences  _clientCorsOriginsSentences;        

        public ClientCorsOriginsBusiness(ILogger<ClientCorsOriginsBusiness> logger,
           ClientCorsOriginsSentences clientCorsOriginsSentences)
        {
            _logger = logger;            
            _clientCorsOriginsSentences = clientCorsOriginsSentences;            
        }

        public IEnumerable<ClientCorsOrigins> Get(string origin)
        {
            try
            {

                return ClientCorsOrigins.Read(_clientCorsOriginsSentences.AddCliteriByOrigin(origin)
                                                                       .GetCriteriaCollection());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {origin}", MethodBase.GetCurrentMethod().Name,origin);
                throw;
            }
        }

        public IEnumerable<ClientCorsOrigins> Get(int clientId)
        {
            IEnumerable<ClientCorsOrigins> result;

            try
            {
                result = ClientCorsOrigins.Read(_clientCorsOriginsSentences.AddCliteriByClient(clientId)
                                          .GetCriteriaCollection());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
                throw;
            }

            return result;
        }
    }
}
