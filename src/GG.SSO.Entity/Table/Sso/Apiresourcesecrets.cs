using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GGPuntoYComa.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","apiresourcesecrets")]
	public class ApiResourceSecrets : Entity<ApiResourceSecrets>
	{
		[DataMemberCRUD("Id", MySqlDbType.Int32, 10, true, true)]
		public int Id { get; set; }

		[DataMemberCRUD("ApiResources_Id", MySqlDbType.Int32, 10, true)]
		public int ApiResources_Id { get; set; }

		[DataMemberCRUD("Description", MySqlDbType.VarChar, 1000)]
		public string Description { get; set; }

		[DataMemberCRUD("Value", MySqlDbType.VarChar, 4000)]
		public string Value { get; set; }

		[DataMemberCRUD("Expiration", MySqlDbType.DateTime, 0)]
		public DateTime? Expiration { get; set; }

		[DataMemberCRUD("Type", MySqlDbType.VarChar, 250)]
		public string Type { get; set; }

		[DataMemberCRUD("Created", MySqlDbType.DateTime, 0)]
		public DateTime Created { get; set; }

		public ApiResourceSecrets()
		{}

		public ApiResourceSecrets(int id,int apiResources_Id,string description,string value,DateTime? expiration,string type,DateTime created)
		{
			Id = id;
			ApiResources_Id = apiResources_Id;
			Description = description;
			Value = value;
			Expiration = expiration;
			Type = type;
			Created = created;
		}
	}
}
