using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GGPuntoYComa.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","clientpostlogoutredirecturis")]
	public class ClientPostLogoutRedirectUris : Entity<ClientPostLogoutRedirectUris>
	{
		[DataMemberCRUD("Id", MySqlDbType.Int32, 10, true, true)]
		public int Id { get; set; }

		[DataMemberCRUD("Clients_Id", MySqlDbType.Int32, 10, true)]
		public int Clients_Id { get; set; }

		[DataMemberCRUD("PostLogoutRedirectUri", MySqlDbType.VarChar, 2000)]
		public string PostLogoutRedirectUri { get; set; }

		public ClientPostLogoutRedirectUris()
		{}

		public ClientPostLogoutRedirectUris(int id,int clients_Id,string postLogoutRedirectUri)
		{
			Id = id;
			Clients_Id = clients_Id;
			PostLogoutRedirectUri = postLogoutRedirectUri;
		}
	}
}
