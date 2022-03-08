using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.SSO.Entity.Table.Sso
{
    [DataClassCRUD("sso", "clientclaims")]
	public class ClientClaims : Entity<ClientClaims>
	{
		[DataMemberCRUD("id", MySqlDbType.Int32, 10, true, true)]
		public int Id { get; set; }

		[DataMemberCRUD("Clients_Id", MySqlDbType.Int32, 10, true)]
		public int Clients_Id { get; set; }

		[DataMemberCRUD("Type", MySqlDbType.VarChar, 250)]
		public string Type { get; set; }

		[DataMemberCRUD("Value", MySqlDbType.VarChar, 250)]
		public string Value { get; set; }

		public ClientClaims()
		{ }

		public ClientClaims(int id, int clients_Id, string type, string value)
		{
			Id = id;
			Clients_Id = clients_Id;
			Type = type;
			Value = value;
		}
	}
}
