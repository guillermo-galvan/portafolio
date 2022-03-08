using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGPuntoYComa.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso", "clients")]
	public class ClientsBasic : Entity<ClientsBasic>
	{
		[DataMemberCRUD("Id", MySqlDbType.Int32, 10, true, true)]
		public int Id { get; set; }

		[DataMemberCRUD("ClientId", MySqlDbType.VarChar, 200)]
		public string ClientId { get; set; }

		[DataMemberCRUD("IsDeleted", MySqlDbType.Bit, 1)]
		public bool IsDeleted { get; set; }

		public ClientsBasic()
		{ }

		public ClientsBasic(int id, string clientId, bool isDeleted)
		{
			Id = id;
			ClientId = clientId;
			IsDeleted = isDeleted;
		}
	}
}
