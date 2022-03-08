using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","devicecodes")]
	public class Devicecodes : Entity<Devicecodes>
	{
		[DataMemberCRUD("UserCode", MySqlDbType.VarChar, 200, true)]
		public string UserCode { get; set; }

		[DataMemberCRUD("DeviceCode", MySqlDbType.VarChar, 200)]
		public string DeviceCode { get; set; }

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
		public DateTime Expiration { get; set; }

		[DataMemberCRUD("Data", MySqlDbType.VarChar, 20000)]
		public string Data { get; set; }

		public Devicecodes()
		{}

		public Devicecodes(string userCode,string deviceCode,string subjectId,string sessionId,string clientId,string description,DateTime creationTime,DateTime expiration,string data)
		{
			UserCode = userCode;
			DeviceCode = deviceCode;
			SubjectId = subjectId;
			SessionId = sessionId;
			ClientId = clientId;
			Description = description;
			CreationTime = creationTime;
			Expiration = expiration;
			Data = data;
		}
	}
}
