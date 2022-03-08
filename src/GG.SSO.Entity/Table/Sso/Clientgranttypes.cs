using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","clientgranttypes")]
	public class ClientGrantTypes : Entity<ClientGrantTypes>
	{
		[DataMemberCRUD("Id", MySqlDbType.Int32, 10, true, true)]
		public int Id { get; set; }

		[DataMemberCRUD("Clients_Id", MySqlDbType.Int32, 10, true)]
		public int Clients_Id { get; set; }

		[DataMemberCRUD("GrantType", MySqlDbType.VarChar, 250)]
		public string GrantType { get; set; }

		public ClientGrantTypes()
		{}

		public ClientGrantTypes(int id,int clients_Id,string grantType)
		{
			Id = id;
			Clients_Id = clients_Id;
			GrantType = grantType;
		}
	}
}
