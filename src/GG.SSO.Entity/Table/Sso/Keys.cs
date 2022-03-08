using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","keys")]
	public class Keys : Entity<Keys>
	{
		[DataMemberCRUD("Id", MySqlDbType.VarChar, 450, true)]
		public string Id { get; set; }

		[DataMemberCRUD("Version", MySqlDbType.Int32, 10)]
		public int Version { get; set; }

		[DataMemberCRUD("Created", MySqlDbType.DateTime, 0)]
		public DateTime Created { get; set; }

		[DataMemberCRUD("Use", MySqlDbType.VarChar, 450)]
		public string Use { get; set; }

		[DataMemberCRUD("Algorithm", MySqlDbType.VarChar, 100)]
		public string Algorithm { get; set; }

		[DataMemberCRUD("IsX509Certificate", MySqlDbType.Bit, 1)]
		public bool IsX509Certificate { get; set; }

		[DataMemberCRUD("DataProtected", MySqlDbType.Bit, 1)]
		public bool DataProtected { get; set; }

		[DataMemberCRUD("Data", MySqlDbType.VarChar, 20000)]
		public string Data { get; set; }

		public Keys()
		{}

		public Keys(string id,int version,DateTime created,string use,string algorithm,bool isX509Certificate,bool dataProtected,string data)
		{
			Id = id;
			Version = version;
			Created = created;
			Use = use;
			Algorithm = algorithm;
			IsX509Certificate = isX509Certificate;
			DataProtected = dataProtected;
			Data = data;
		}
	}
}
