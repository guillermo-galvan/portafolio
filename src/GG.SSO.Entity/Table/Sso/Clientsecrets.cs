using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","clientsecrets")]
	public class ClientSecrets : Entity<ClientSecrets>
	{
		[DataMemberCRUD("Id", MySqlDbType.Int32, 10, true, true)]
		public int Id { get; set; }

		[DataMemberCRUD("Clients_Id", MySqlDbType.Int32, 10, true)]
		public int Clients_Id { get; set; }

		[DataMemberCRUD("Description", MySqlDbType.VarChar, 2000)]
		public string Description { get; set; }

		[DataMemberCRUD("Value", MySqlDbType.VarChar, 4000)]
		public string Value { get; set; }

		[DataMemberCRUD("Expiration", MySqlDbType.DateTime, 0)]
		public DateTime? Expiration { get; set; }

		[DataMemberCRUD("Type", MySqlDbType.VarChar, 250)]
		public string Type { get; set; }

		[DataMemberCRUD("Created", MySqlDbType.DateTime, 0)]
		public DateTime Created { get; set; }

		public ClientSecrets()
		{}

		public ClientSecrets(int id,int clients_Id,string description,string value,DateTime? expiration,string type,DateTime created)
		{
			Id = id;
			Clients_Id = clients_Id;
			Description = description;
			Value = value;
			Expiration = expiration;
			Type = type;
			Created = created;
		}
	}
}
