using GGPuntoYComa.SSO.DataBaseBusiness.Business;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Extensions.Caching.Memory;
using GGPuntoYComa.SSO.DataBaseBusiness;

namespace GGPuntoYComa.SSO.BusinesLogic.IdentityServer
{
    public class ClientCorsOriginsManagement
    {
        const string Key = "ClientCorsOrigins";
        private readonly ClientCorsOriginsBusiness _clientCorsOriginsBusiness;
        private readonly ILogger<ClientCorsOriginsManagement> _logger;
        private readonly IMemoryCache _cache;
        private readonly DataBaseOptions _dataBaseOptions;

        public ClientCorsOriginsManagement(ClientCorsOriginsBusiness clientCorsOriginsBusiness, 
            ILogger<ClientCorsOriginsManagement> logger, IMemoryCache cache, DataBaseOptions dataBaseOptions)
        {
            _clientCorsOriginsBusiness = clientCorsOriginsBusiness;
            _logger = logger;
            _cache = cache;
            _dataBaseOptions = dataBaseOptions;
        }

        public bool IsOriginAllowed(string origin)
        {
            bool result = false;
            try
            {
                if (!_cache.TryGetValue($"{Key}{origin}", out result))
                {
                    result = _clientCorsOriginsBusiness.Get(origin).Any();
                    _cache.Set($"{Key}{origin}", result, TimeSpan.FromMinutes(_dataBaseOptions.AbsoluteExpirationRelativeToNowCache));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {clientId}", MethodBase.GetCurrentMethod().Name,origin);
            }

            return result;
        }
    }
}
