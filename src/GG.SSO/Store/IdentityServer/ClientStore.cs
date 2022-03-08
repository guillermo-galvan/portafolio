using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using GGPuntoYComa.SSO.BusinesLogic.IdentityServer;
using GGPuntoYComa.SSO.DataBaseBusiness.Business;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GGPuntoYComa.SSO.Store.IdentityServer
{
    public class ClientStore : IClientStore
    {
        private readonly ILogger<ClientStore> _logger;
        private readonly ClientsManagement _clientsManagement;

        public ClientStore(ILogger<ClientStore> logger, ClientsManagement clientsManagement)
        {
            _logger = logger;
            _clientsManagement = clientsManagement;
        }

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            try
            {
                return Task.FromResult(_clientsManagement.FindClientById(clientId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FindClientByIdAsync {clientId}", clientId);
                return Task.FromResult<Client>(null);
            }
        }
    }
}
