using Duende.IdentityServer.Models;
using GG.SSO.Entity.Table.Sso;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.BusinesLogic.Helpers
{
	public static class Converts
    {
		public static PersistedGrant ToPersistedGrant(PersistedGrants persistedGrants)
		{
			if (persistedGrants == default)
			{
				return default;
			}
			else
			{
				return new()
				{
					ClientId = persistedGrants.ClientId,
					ConsumedTime = persistedGrants.ConsumedTime,
					CreationTime = persistedGrants.CreationTime,
					Data = persistedGrants.Data,
					Description = persistedGrants.Description,
					Expiration = persistedGrants.Expiration,
					Key = persistedGrants.Key,
					SessionId = persistedGrants.SessionId,
					SubjectId = persistedGrants.SubjectId,
					Type = persistedGrants.Type
				};
			}
		}

		public static PersistedGrants ToPersistedGrants(PersistedGrant persistedGrant)
		{
			if (persistedGrant == default)
			{
				return default;
			}
			else
			{
				return new PersistedGrants(persistedGrant.Key, persistedGrant.Type, persistedGrant.SubjectId, persistedGrant.SessionId,
					persistedGrant.ClientId, persistedGrant.Description, persistedGrant.CreationTime, persistedGrant.Expiration,
					persistedGrant.ConsumedTime, persistedGrant.Data, false);
			}
		}

		public static IdentityResource ToIdentityResource(Entity.Table.Sso.IdentityResources identityResources, 
			IDictionary<string, string> properties, ICollection<string> claims)
		{
			if (identityResources == null)
			{
				return default;
			}
			else
			{
				return new IdentityResource
				{
					Description = identityResources.Description,
					DisplayName = identityResources.DisplayName,
					Emphasize = identityResources.Emphasize,
					Enabled = identityResources.Enabled,
					Name = identityResources.Name,
					Properties = properties,
					Required = identityResources.Required,
					ShowInDiscoveryDocument = identityResources.ShowInDiscoveryDocument,
					UserClaims = claims
				};
			}
		}

		public static ApiScope ToApiScope(ApiScopes apiScopes, IDictionary<string, string> properties, 
			ICollection<string> claims)
		{
			if (apiScopes == null)
			{
				return default;
			}
			else
			{
				return new ApiScope 
				{
					Description = apiScopes.Description,
					DisplayName = apiScopes.DisplayName,
					Emphasize = apiScopes.Emphasize,
					Enabled = apiScopes.Enabled,
					Name = apiScopes.Name,
					Properties = properties,
					Required = apiScopes.Required,
					ShowInDiscoveryDocument = apiScopes.ShowInDiscoveryDocument,
					UserClaims = claims
				};
			}
		}

		public static Secret ToSecret(ApiResourceSecrets apiResourceSecrets)
		{
			if (apiResourceSecrets == null)
			{
				return null;
			}
			else
			{
				return new Secret 
				{
					Description = apiResourceSecrets.Description,
					Expiration = apiResourceSecrets.Expiration,
					Type = apiResourceSecrets.Type,
					Value = apiResourceSecrets.Value
				};
			}
		}

		public static ApiResource ToApiResource(ApiResources apiResources, ICollection<string> scope,
			IDictionary<string, string> properties,ICollection<Secret> secrets, ICollection<string> claims)
		{
			if (apiResources == null)
			{
				return null;
			}
			else
			{
				return new ApiResource 
				{
					AllowedAccessTokenSigningAlgorithms = apiResources.AllowedAccessTokenSigningAlgorithms?.Split("|", StringSplitOptions.RemoveEmptyEntries),
					ApiSecrets = secrets,
					Description = apiResources.Description,
					DisplayName = apiResources.DisplayName,
					Enabled = apiResources.Enabled,
					Name = apiResources.Name,
					Properties = properties,
					RequireResourceIndicator = apiResources.RequireResourceIndicator,
					Scopes = scope,
					ShowInDiscoveryDocument = apiResources.ShowInDiscoveryDocument,
					UserClaims = claims
				};
			}
		
		}

        public static UserLoginInfo ToUserLoginInfo(UserExternalLogins userExternalLogins)
        {
            if (userExternalLogins == default)
            {
                return default;
            }
            else
            {
                return new UserLoginInfo(userExternalLogins.LoginProvider, userExternalLogins.ProviderKey, userExternalLogins.ProviderDisplayName);
            }
        }

		public static Claim ToClaim(UserClaims userClaims)
		{
			if (userClaims == default)
			{
				return default;
			}
			else
			{
				return new Claim(userClaims.Type, userClaims.Value, userClaims.ValueType);
			}
		}

		public static Claim ToClaim(RoleClaims claim)
		{
			if (claim == null)
			{
				return default;
			}
			else
			{
				return new(claim.Type, claim.Value, claim.ValueType);
			}
		}

		public static Secret ToSecret(ClientSecrets clientSecrets)
		{
			Secret result = null;

			if (clientSecrets != null)
			{
				result = new Secret
				{
					Description = clientSecrets.Description,
					Expiration = clientSecrets.Expiration,
					Type = clientSecrets.Type,
					Value = clientSecrets.Value
				};
			}

			return result;
		}

		public static ClientClaim ToClientClaim(ClientClaims clientClaims)
		{
			ClientClaim result = null;

			if (clientClaims != null)
			{
				result = new ClientClaim(clientClaims.Type, clientClaims.Value);
			}

			return result;
		}

		public static SerializedKey ToSerializedKey(Keys keys)
		{ 
			SerializedKey result = null;

			if (keys != null)
			{
				result = new SerializedKey 
				{
					Algorithm = keys.Algorithm,
					Created = keys.Created,
					Data = keys.Data,
					DataProtected = keys.DataProtected,
					Id = keys.Id,
					IsX509Certificate = keys.IsX509Certificate,
					Version = keys.Version,
				};
			}

			return result;
		}

		public static Keys ToKey(SerializedKey serializedKey)
		{
			Keys result = null;

			if (serializedKey != null)
			{
				result = new Keys
				{
					Algorithm = serializedKey.Algorithm,
					Created = serializedKey.Created,
					Data = serializedKey.Data,
					DataProtected = serializedKey.DataProtected,
					Id = serializedKey.Id,
					IsX509Certificate = serializedKey.IsX509Certificate,
					Version = serializedKey.Version,
				};
			}

			return result;
		}
	}
}
