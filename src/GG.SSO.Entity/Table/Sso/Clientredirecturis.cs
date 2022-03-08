using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GGPuntoYComa.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","clientredirecturis")]
	public class ClientRedirectUris : Entity<ClientRedirectUris>
	{
		[DataMemberCRUD("Id", MySqlDbType.Int32, 10, true, true)]
		public int Id { get; set; }

		[DataMemberCRUD("Clients_Id", MySqlDbType.Int32, 10, true)]
		public int Clients_Id { get; set; }

		[DataMemberCRUD("RedirectUri", MySqlDbType.VarChar, 2000)]
		public string RedirectUri { get; set; }

		public ClientRedirectUris()
		{}

		public ClientRedirectUris(int id,int clients_Id,string redirectUri)
		{
			Id = id;
			Clients_Id = clients_Id;
			RedirectUri = redirectUri;
		}
	}
}
