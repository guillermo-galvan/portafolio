using Duende.IdentityServer.Models;
using GGPuntoYComa.SSO.BusinesLogic.Helpers;
using GGPuntoYComa.SSO.DataBaseBusiness;
using GGPuntoYComa.SSO.DataBaseBusiness.Business;
using GGPuntoYComa.SSO.Entity.Table.Sso;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace GGPuntoYComa.SSO.BusinesLogic.IdentityServer
{
    public class ClientsManagement
    {
        const string Key = "Clients";
        private readonly ILogger<ClientsManagement> _logger;
        private readonly ClientsBusiness _clientsBusiness;
        private readonly IMemoryCache _cache;
        private readonly ClientPropertiesBusiness _clientPropertiesBusiness;
        private readonly ClientIdPRestrictionsBusiness _clientIdPRestrictionsBusiness;
        private readonly ClientGrantTypesBusiness _clientGrantTypesBusiness;
        private readonly ClientCorsOriginsBusiness _clientCorsOriginsBusiness;
        private readonly ClientPostLogoutRedirectUrisBusiness _clientPostLogoutRedirectUrisBusiness;
        private readonly ClientScopesBusiness _clientScopesBusiness;
        private readonly ClientClaimsBusiness _clientClaimsBusiness;
        private readonly ClientRedirectUrisBusiness _clientRedirectUrisBusiness;
        private readonly ClientSecretsBusiness _clientSecretsBusiness;
        private readonly DataBaseOptions _dataBaseOptions;

        public ClientsManagement(ILogger<ClientsManagement> logger, ClientsBusiness clientsBusiness, IMemoryCache cache,
            ClientPropertiesBusiness clientPropertiesBusiness, ClientIdPRestrictionsBusiness clientIdPRestrictionsBusiness,
            ClientGrantTypesBusiness clientGrantTypesBusiness, ClientPostLogoutRedirectUrisBusiness clientPostLogoutRedirectUrisBusiness,
            ClientCorsOriginsBusiness clientCorsOriginsBusiness,
            ClientScopesBusiness clientScopesBusiness, ClientClaimsBusiness clientClaimsBusiness,
            ClientRedirectUrisBusiness clientRedirectUrisBusiness, ClientSecretsBusiness clientSecretsBusiness,
            DataBaseOptions dataBaseOptions)
        {
            _logger = logger;
            _clientsBusiness = clientsBusiness;
            _cache = cache;
            _clientPropertiesBusiness = clientPropertiesBusiness;
            _clientIdPRestrictionsBusiness = clientIdPRestrictionsBusiness;
            _clientGrantTypesBusiness = clientGrantTypesBusiness;
            _clientCorsOriginsBusiness = clientCorsOriginsBusiness;
            _clientPostLogoutRedirectUrisBusiness = clientPostLogoutRedirectUrisBusiness;
            _clientScopesBusiness = clientScopesBusiness;
            _clientClaimsBusiness = clientClaimsBusiness;
            _clientRedirectUrisBusiness = clientRedirectUrisBusiness;
            _clientSecretsBusiness = clientSecretsBusiness;
            _dataBaseOptions = dataBaseOptions;
        }

        private Client GetClient(Clients clients, ICollection<string> identityProviderRestrictions, ICollection<string> allowedGrantTypes,
           ICollection<string> allowedCorsOrigins, ICollection<string> postLogoutRedirectUris, ICollection<string> allowedScopes,
           ICollection<ClientClaim> claims, ICollection<string> redirectUris, ICollection<Secret> clientSecrets,
           IDictionary<string, string> properties) => new()
           {
               AbsoluteRefreshTokenLifetime = clients.AbsoluteRefreshTokenLifetime,
               AccessTokenLifetime = clients.AccessTokenLifetime,
               AccessTokenType = (AccessTokenType)clients.AccessTokenType,
               AllowAccessTokensViaBrowser = clients.AllowAccessTokensViaBrowser,
               AllowedCorsOrigins = allowedCorsOrigins,
               AllowedGrantTypes = allowedGrantTypes,
               AllowedIdentityTokenSigningAlgorithms = clients.AllowedIdentityTokenSigningAlgorithms?.Split("|", StringSplitOptions.RemoveEmptyEntries),
               AllowedScopes = allowedScopes,
               AllowOfflineAccess = clients.AllowOfflineAccess,
               AllowPlainTextPkce = clients.AllowPlainTextPkce,
               AllowRememberConsent = clients.AllowRememberConsent,
               AlwaysIncludeUserClaimsInIdToken = clients.AlwaysIncludeUserClaimsInIdToken,
               AlwaysSendClientClaims = clients.AlwaysSendClientClaims,
               AuthorizationCodeLifetime = clients.AuthorizationCodeLifetime,
               BackChannelLogoutSessionRequired = clients.BackChannelLogoutSessionRequired,
               BackChannelLogoutUri = clients.BackChannelLogoutUri,
               Claims = claims,
               ClientClaimsPrefix = clients.ClientClaimsPrefix,
               ClientId = clients.ClientId,
               ClientName = clients.ClientName,
               ClientSecrets = clientSecrets,
               ClientUri = clients.ClientUri,
               ConsentLifetime = clients.ConsentLifetime,
               Description = clients.Description,
               DeviceCodeLifetime = clients.DeviceCodeLifetime,
               Enabled = clients.Enabled,
               EnableLocalLogin = clients.EnableLocalLogin,
               FrontChannelLogoutSessionRequired = clients.FrontChannelLogoutSessionRequired,
               FrontChannelLogoutUri = clients.FrontChannelLogoutUri,
               IdentityProviderRestrictions = identityProviderRestrictions,
               IdentityTokenLifetime = clients.IdentityTokenLifetime,
               IncludeJwtId = clients.IncludeJwtId,
               LogoUri = clients.LogoUri,
               PairWiseSubjectSalt = clients.PairWiseSubjectSalt,
               PostLogoutRedirectUris = postLogoutRedirectUris,
               Properties = properties,
               ProtocolType = clients.ProtocolType,
               RedirectUris = redirectUris,
               RefreshTokenExpiration = (TokenExpiration)clients.RefreshTokenExpiration,
               RefreshTokenUsage = (TokenUsage)clients.RefreshTokenUsage,
               RequireClientSecret = clients.RequireClientSecret,
               RequireConsent = clients.RequireConsent,
               RequirePkce = clients.RequirePkce,
               RequireRequestObject = clients.RequireRequestObject,
               SlidingRefreshTokenLifetime = clients.SlidingRefreshTokenLifetime,
               UpdateAccessTokenClaimsOnRefresh = clients.UpdateAccessTokenClaimsOnRefresh,
               UserCodeType = clients.UserCodeType,
               UserSsoLifetime = clients.UserSsoLifetime
           };


        public Client FindClientById(string clientId)
        {
            try
            {
                Client result = _cache.Get<Client>($"{Key}{clientId}");

                if (result == null)
                {
                    var client = _clientsBusiness.Get(clientId);

                    if (client != null)
                    {
                        IDictionary<string, string> properties = new Dictionary<string, string>();
                        _clientPropertiesBusiness.Get(client.Id).ToList().ForEach(y => properties.Add(y.Key, y.Value));

                        ICollection<string> identityProviderRestrictions = 
                            _clientIdPRestrictionsBusiness.Get(client.Id).Select(y => y.Provider).ToList();

                        ICollection<string> allowedGrantTypes = 
                            _clientGrantTypesBusiness.Get(client.Id).Select(y => y.GrantType).ToList();

                        ICollection<string> allowedCorsOrigins = 
                            _clientCorsOriginsBusiness.Get(client.Id).Select(y => y.Origin).ToList();

                        ICollection<string> postLogoutRedirectUris =
                            _clientPostLogoutRedirectUrisBusiness.Get(client.Id).Select(y => y.PostLogoutRedirectUri).ToList();

                        ICollection<string> allowedScopes = 
                            _clientScopesBusiness.Get(client.Id).Select(y => y.Scope).ToList();

                        ICollection<ClientClaim> claims = 
                            _clientClaimsBusiness.Get(client.Id).Select(y => Converts.ToClientClaim(y)).ToList();

                        ICollection<string> redirectUris = 
                            _clientRedirectUrisBusiness.Get(client.Id).Select(y => y.RedirectUri).ToList();

                        ICollection<Secret> clientSecrets = 
                            _clientSecretsBusiness.Get(client.Id).Select(y => Converts.ToSecret(y)).ToList();

                        client.LastAccessed = DateTime.Now;
                        _clientsBusiness.Update(client);

                        result = GetClient(client, identityProviderRestrictions, allowedGrantTypes,
                            allowedCorsOrigins, postLogoutRedirectUris, allowedScopes, claims, redirectUris,
                            clientSecrets, properties);

                        _cache.Set($"{Key}{clientId}", result,
                            TimeSpan.FromMinutes(_dataBaseOptions.AbsoluteExpirationRelativeToNowCache));
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {clientId}", MethodBase.GetCurrentMethod().Name,clientId);
                return null;
            }
        }

        public Clients FindClientsById(string clientId)
        {
            try
            {
                return _clientsBusiness.Get(clientId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {clientId}", MethodBase.GetCurrentMethod().Name, clientId);
                return null;
            }
        }
    }
}
