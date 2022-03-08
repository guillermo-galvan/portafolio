using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GGPuntoYComa.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","clientcorsorigins")]
	public class ClientCorsOrigins : Entity<ClientCorsOrigins>
	{
		[DataMemberCRUD("Id", MySqlDbType.Int32, 10, true, true)]
		public int Id { get; set; }

		[DataMemberCRUD("Clients_Id", MySqlDbType.Int32, 10, true)]
		public int Clients_Id { get; set; }

		[DataMemberCRUD("Origin", MySqlDbType.VarChar, 150)]
		public string Origin { get; set; }

		public ClientCorsOrigins()
		{}

		public ClientCorsOrigins(int id,int clients_Id,string origin)
		{
			Id = id;
			Clients_Id = clients_Id;
			Origin = origin;
		}
	}
}
