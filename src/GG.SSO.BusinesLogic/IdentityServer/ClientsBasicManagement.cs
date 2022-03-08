using GG.SSO.DataBaseBusiness.Business;
using GG.SSO.Entity.Table.Sso;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.BusinesLogic.IdentityServer
{
    public class ClientsBasicManagement
    {
        private readonly ClientsBasicBusiness _clientsBasicBusiness;
        private readonly ILogger<ClientsBasicManagement> _logger;

        public ClientsBasicManagement(ClientsBasicBusiness clientsBasicBusines, ILogger<ClientsBasicManagement> logger)
        {
            _clientsBasicBusiness = clientsBasicBusines;
            _logger = logger;
        }

        public ClientsBasic Get(string clientId)
        {
            try
            {
                return _clientsBasicBusiness.Get(clientId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {clientId}", MethodBase.GetCurrentMethod().Name, clientId);
                return null;
            }
        }
    }
}
