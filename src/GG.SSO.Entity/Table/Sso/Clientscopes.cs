using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","clientscopes")]
	public class ClientScopes : Entity<ClientScopes>
	{
		[DataMemberCRUD("Id", MySqlDbType.Int32, 10, true, true)]
		public int Id { get; set; }

		[DataMemberCRUD("Clients_Id", MySqlDbType.Int32, 10, true)]
		public int Clients_Id { get; set; }

		[DataMemberCRUD("Scope", MySqlDbType.VarChar, 250)]
		public string Scope { get; set; }

		public ClientScopes()
		{}

		public ClientScopes(int id,int clients_Id,string scope)
		{
			Id = id;
			Clients_Id = clients_Id;
			Scope = scope;
		}
	}
}
