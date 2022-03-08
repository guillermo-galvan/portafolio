using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","clientidprestrictions")]
	public class ClientIdPRestrictions : Entity<ClientIdPRestrictions>
	{
		[DataMemberCRUD("Id", MySqlDbType.Int32, 10, true, true)]
		public int Id { get; set; }

		[DataMemberCRUD("Clients_Id", MySqlDbType.Int32, 10, true)]
		public int Clients_Id { get; set; }

		[DataMemberCRUD("Provider", MySqlDbType.VarChar, 200)]
		public string Provider { get; set; }

		public ClientIdPRestrictions()
		{}

		public ClientIdPRestrictions(int id,int clients_Id,string provider)
		{
			Id = id;
			Clients_Id = clients_Id;
			Provider = provider;
		}
	}
}
