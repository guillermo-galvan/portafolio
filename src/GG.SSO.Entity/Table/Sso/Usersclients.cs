using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GGPuntoYComa.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","usersclients")]
	public class UsersClients : Entity<UsersClients>
	{
		[DataMemberCRUD("Users_Id", MySqlDbType.VarChar, 450, true)]
		public string Users_Id { get; set; }

		[DataMemberCRUD("Clients_Id", MySqlDbType.Int32, 10, true)]
		public int Clients_Id { get; set; }

		public UsersClients()
		{}

		public UsersClients(string users_Id,int clients_Id)
		{
			Users_Id = users_Id;
			Clients_Id = clients_Id;
		}
	}
}
