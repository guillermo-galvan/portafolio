using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GGPuntoYComa.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","apiscopeclaims")]
	public class ApiScopeClaims : Entity<ApiScopeClaims>
	{
		[DataMemberCRUD("Id", MySqlDbType.Int32, 10, true, true)]
		public int Id { get; set; }

		[DataMemberCRUD("ApiScopes_Id", MySqlDbType.Int32, 10, true)]
		public int ApiScopes_Id { get; set; }

		[DataMemberCRUD("Type", MySqlDbType.VarChar, 200)]
		public string Type { get; set; }

		public ApiScopeClaims()
		{}

		public ApiScopeClaims(int id,int apiScopes_Id,string type)
		{
			Id = id;
			ApiScopes_Id = apiScopes_Id;
			Type = type;
		}
	}
}
