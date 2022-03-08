using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GGPuntoYComa.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","persistedgrants")]
	public class PersistedGrants : Entity<PersistedGrants>
	{
		[DataMemberCRUD("Key", MySqlDbType.VarChar, 200, true)]
		public string Key { get; set; }

		[DataMemberCRUD("Type", MySqlDbType.VarChar, 50)]
		public string Type { get; set; }

		[DataMemberCRUD("SubjectId", MySqlDbType.VarChar, 200)]
		public string SubjectId { get; set; }

		[DataMemberCRUD("SessionId", MySqlDbType.VarChar, 100)]
		public string SessionId { get; set; }

		[DataMemberCRUD("ClientId", MySqlDbType.VarChar, 200)]
		public string ClientId { get; set; }

		[DataMemberCRUD("Description", MySqlDbType.VarChar, 200)]
		public string Description { get; set; }

		[DataMemberCRUD("CreationTime", MySqlDbType.DateTime, 0)]
		public DateTime CreationTime { get; set; }

		[DataMemberCRUD("Expiration", MySqlDbType.DateTime, 0)]
		public DateTime? Expiration { get; set; }

		[DataMemberCRUD("ConsumedTime", MySqlDbType.DateTime, 0)]
		public DateTime? ConsumedTime { get; set; }

		[DataMemberCRUD("Data", MySqlDbType.VarChar, 20000)]
		public string Data { get; set; }

		[DataMemberCRUD("IsDeleted", MySqlDbType.Bit, 1)]
		public bool IsDeleted { get; set; }

        public PersistedGrants()
		{}

		public PersistedGrants(string key,string type,string subjectId,string sessionId,string clientId,string description,DateTime creationTime,DateTime? expiration,DateTime? consumedTime,string data, bool isDeleted)
		{
			Key = key;
			Type = type;
			SubjectId = subjectId;
			SessionId = sessionId;
			ClientId = clientId;
			Description = description;
			CreationTime = creationTime;
			Expiration = expiration;
			ConsumedTime = consumedTime;
			Data = data;
			IsDeleted = isDeleted;
		}
	}
}
