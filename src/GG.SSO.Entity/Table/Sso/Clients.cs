using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","clients")]
	public class Clients : Entity<Clients>
	{
		[DataMemberCRUD("Id", MySqlDbType.Int32, 10, true, true)]
		public int Id { get; set; }

		[DataMemberCRUD("Enabled", MySqlDbType.Bit, 1)]
		public bool Enabled { get; set; }

		[DataMemberCRUD("ClientId", MySqlDbType.VarChar, 200)]
		public string ClientId { get; set; }

		[DataMemberCRUD("ProtocolType", MySqlDbType.VarChar, 200)]
		public string ProtocolType { get; set; }

		[DataMemberCRUD("RequireClientSecret", MySqlDbType.Bit, 1)]
		public bool RequireClientSecret { get; set; }

		[DataMemberCRUD("ClientName", MySqlDbType.VarChar, 200)]
		public string ClientName { get; set; }

		[DataMemberCRUD("Description", MySqlDbType.VarChar, 1000)]
		public string Description { get; set; }

		[DataMemberCRUD("ClientUri", MySqlDbType.VarChar, 2000)]
		public string ClientUri { get; set; }

		[DataMemberCRUD("LogoUri", MySqlDbType.VarChar, 2000)]
		public string LogoUri { get; set; }

		[DataMemberCRUD("RequireConsent", MySqlDbType.Bit, 1)]
		public bool RequireConsent { get; set; }

		[DataMemberCRUD("AllowRememberConsent", MySqlDbType.Bit, 1)]
		public bool AllowRememberConsent { get; set; }

		[DataMemberCRUD("AlwaysIncludeUserClaimsInIdToken", MySqlDbType.Bit, 1)]
		public bool AlwaysIncludeUserClaimsInIdToken { get; set; }

		[DataMemberCRUD("RequirePkce", MySqlDbType.Bit, 1)]
		public bool RequirePkce { get; set; }

		[DataMemberCRUD("AllowPlainTextPkce", MySqlDbType.Bit, 1)]
		public bool AllowPlainTextPkce { get; set; }

		[DataMemberCRUD("RequireRequestObject", MySqlDbType.Bit, 1)]
		public bool RequireRequestObject { get; set; }

		[DataMemberCRUD("AllowAccessTokensViaBrowser", MySqlDbType.Bit, 1)]
		public bool AllowAccessTokensViaBrowser { get; set; }

		[DataMemberCRUD("FrontChannelLogoutUri", MySqlDbType.VarChar, 2000)]
		public string FrontChannelLogoutUri { get; set; }

		[DataMemberCRUD("FrontChannelLogoutSessionRequired", MySqlDbType.Bit, 1)]
		public bool FrontChannelLogoutSessionRequired { get; set; }

		[DataMemberCRUD("BackChannelLogoutUri", MySqlDbType.VarChar, 2000)]
		public string BackChannelLogoutUri { get; set; }

		[DataMemberCRUD("BackChannelLogoutSessionRequired", MySqlDbType.Bit, 1)]
		public bool BackChannelLogoutSessionRequired { get; set; }

		[DataMemberCRUD("AllowOfflineAccess", MySqlDbType.Bit, 1)]
		public bool AllowOfflineAccess { get; set; }

		[DataMemberCRUD("IdentityTokenLifetime", MySqlDbType.Int32, 10)]
		public int IdentityTokenLifetime { get; set; }

		[DataMemberCRUD("AllowedIdentityTokenSigningAlgorithms", MySqlDbType.VarChar, 100)]
		public string AllowedIdentityTokenSigningAlgorithms { get; set; }

		[DataMemberCRUD("AccessTokenLifetime", MySqlDbType.Int32, 10)]
		public int AccessTokenLifetime { get; set; }

		[DataMemberCRUD("AuthorizationCodeLifetime", MySqlDbType.Int32, 10)]
		public int AuthorizationCodeLifetime { get; set; }

		[DataMemberCRUD("ConsentLifetime", MySqlDbType.Int32, 10)]
		public int? ConsentLifetime { get; set; }

		[DataMemberCRUD("AbsoluteRefreshTokenLifetime", MySqlDbType.Int32, 10)]
		public int AbsoluteRefreshTokenLifetime { get; set; }

		[DataMemberCRUD("SlidingRefreshTokenLifetime", MySqlDbType.Int32, 10)]
		public int SlidingRefreshTokenLifetime { get; set; }

		[DataMemberCRUD("RefreshTokenUsage", MySqlDbType.Int32, 10)]
		public int RefreshTokenUsage { get; set; }

		[DataMemberCRUD("UpdateAccessTokenClaimsOnRefresh", MySqlDbType.Bit, 1)]
		public bool UpdateAccessTokenClaimsOnRefresh { get; set; }

		[DataMemberCRUD("RefreshTokenExpiration", MySqlDbType.Int32, 10)]
		public int RefreshTokenExpiration { get; set; }

		[DataMemberCRUD("AccessTokenType", MySqlDbType.Int32, 10)]
		public int AccessTokenType { get; set; }

		[DataMemberCRUD("EnableLocalLogin", MySqlDbType.Bit, 1)]
		public bool EnableLocalLogin { get; set; }

		[DataMemberCRUD("IncludeJwtId", MySqlDbType.Bit, 1)]
		public bool IncludeJwtId { get; set; }

		[DataMemberCRUD("AlwaysSendClientClaims", MySqlDbType.Bit, 1)]
		public bool AlwaysSendClientClaims { get; set; }

		[DataMemberCRUD("ClientClaimsPrefix", MySqlDbType.VarChar, 200)]
		public string ClientClaimsPrefix { get; set; }

		[DataMemberCRUD("PairWiseSubjectSalt", MySqlDbType.VarChar, 200)]
		public string PairWiseSubjectSalt { get; set; }

		[DataMemberCRUD("Created", MySqlDbType.DateTime, 0)]
		public DateTime Created { get; set; }

		[DataMemberCRUD("Updated", MySqlDbType.DateTime, 0)]
		public DateTime? Updated { get; set; }

		[DataMemberCRUD("LastAccessed", MySqlDbType.DateTime, 0)]
		public DateTime? LastAccessed { get; set; }

		[DataMemberCRUD("UserSsoLifetime", MySqlDbType.Int32, 10)]
		public int? UserSsoLifetime { get; set; }

		[DataMemberCRUD("UserCodeType", MySqlDbType.VarChar, 100)]
		public string UserCodeType { get; set; }

		[DataMemberCRUD("DeviceCodeLifetime", MySqlDbType.Int32, 10)]
		public int DeviceCodeLifetime { get; set; }

		[DataMemberCRUD("IsEditable", MySqlDbType.Bit, 1)]
		public bool IsEditable { get; set; }

		[DataMemberCRUD("IsDeleted", MySqlDbType.Bit, 1)]
		public bool IsDeleted { get; set; }

		public Clients()
		{}

		public Clients(int id,bool enabled,string clientId,string protocolType,bool requireClientSecret,string clientName,string description,string clientUri,string logoUri,bool requireConsent,bool allowRememberConsent,bool alwaysIncludeUserClaimsInIdToken,bool requirePkce,bool allowPlainTextPkce,bool requireRequestObject,bool allowAccessTokensViaBrowser,string frontChannelLogoutUri,bool frontChannelLogoutSessionRequired,string backChannelLogoutUri,bool backChannelLogoutSessionRequired,bool allowOfflineAccess,int identityTokenLifetime,string allowedIdentityTokenSigningAlgorithms,int accessTokenLifetime,int authorizationCodeLifetime,int? consentLifetime,int absoluteRefreshTokenLifetime,int slidingRefreshTokenLifetime,int refreshTokenUsage,bool updateAccessTokenClaimsOnRefresh,int refreshTokenExpiration,int accessTokenType,bool enableLocalLogin,bool includeJwtId,bool alwaysSendClientClaims,string clientClaimsPrefix,string pairWiseSubjectSalt,DateTime created,DateTime? updated,DateTime? lastAccessed,int? userSsoLifetime,string userCodeType,int deviceCodeLifetime,bool isEditable, bool isDeleted)
		{
			Id = id;
			Enabled = enabled;
			ClientId = clientId;
			ProtocolType = protocolType;
			RequireClientSecret = requireClientSecret;
			ClientName = clientName;
			Description = description;
			ClientUri = clientUri;
			LogoUri = logoUri;
			RequireConsent = requireConsent;
			AllowRememberConsent = allowRememberConsent;
			AlwaysIncludeUserClaimsInIdToken = alwaysIncludeUserClaimsInIdToken;
			RequirePkce = requirePkce;
			AllowPlainTextPkce = allowPlainTextPkce;
			RequireRequestObject = requireRequestObject;
			AllowAccessTokensViaBrowser = allowAccessTokensViaBrowser;
			FrontChannelLogoutUri = frontChannelLogoutUri;
			FrontChannelLogoutSessionRequired = frontChannelLogoutSessionRequired;
			BackChannelLogoutUri = backChannelLogoutUri;
			BackChannelLogoutSessionRequired = backChannelLogoutSessionRequired;
			AllowOfflineAccess = allowOfflineAccess;
			IdentityTokenLifetime = identityTokenLifetime;
			AllowedIdentityTokenSigningAlgorithms = allowedIdentityTokenSigningAlgorithms;
			AccessTokenLifetime = accessTokenLifetime;
			AuthorizationCodeLifetime = authorizationCodeLifetime;
			ConsentLifetime = consentLifetime;
			AbsoluteRefreshTokenLifetime = absoluteRefreshTokenLifetime;
			SlidingRefreshTokenLifetime = slidingRefreshTokenLifetime;
			RefreshTokenUsage = refreshTokenUsage;
			UpdateAccessTokenClaimsOnRefresh = updateAccessTokenClaimsOnRefresh;
			RefreshTokenExpiration = refreshTokenExpiration;
			AccessTokenType = accessTokenType;
			EnableLocalLogin = enableLocalLogin;
			IncludeJwtId = includeJwtId;
			AlwaysSendClientClaims = alwaysSendClientClaims;
			ClientClaimsPrefix = clientClaimsPrefix;
			PairWiseSubjectSalt = pairWiseSubjectSalt;
			Created = created;
			Updated = updated;
			LastAccessed = lastAccessed;
			UserSsoLifetime = userSsoLifetime;
			UserCodeType = userCodeType;
			DeviceCodeLifetime = deviceCodeLifetime;
			IsEditable = isEditable;
			IsDeleted = isDeleted;
		}
	}
}
