using Duende.IdentityServer.Services;
using GGPuntoYComa.SSO.BusinesLogic.IdentityServer;
using GGPuntoYComa.SSO.DataBaseBusiness.Business;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GGPuntoYComa.SSO.Store.IdentityServer
{
    public class CorsPolicyService : ICorsPolicyService
    {
        private readonly ClientCorsOriginsManagement _clientCorsOriginsManagement;
        private readonly ILogger _logger;

        public CorsPolicyService(ClientCorsOriginsManagement clientCorsOriginsManagement, ILogger<CorsPolicyService> logger)
        {
            _clientCorsOriginsManagement = clientCorsOriginsManagement;
            _logger = logger;
        }

        public Task<bool> IsOriginAllowedAsync(string origin)
        {
            try
            {
                return Task.FromResult(_clientCorsOriginsManagement.IsOriginAllowed(origin));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "IsOriginAllowedAsync {clientId}", origin);
                return Task.FromResult(false);
            }
        }
    }
}
