using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GGPuntoYComa.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","apiresourcescopes")]
	public class ApiResourceScopes : Entity<ApiResourceScopes>
	{
		[DataMemberCRUD("Id", MySqlDbType.Int32, 10, true, true)]
		public int Id { get; set; }

		[DataMemberCRUD("ApiResources_Id", MySqlDbType.Int32, 10, true)]
		public int ApiResources_Id { get; set; }

		[DataMemberCRUD("Scope", MySqlDbType.VarChar, 200)]
		public string Scope { get; set; }

		public ApiResourceScopes()
		{}

		public ApiResourceScopes(int id,int apiResources_Id,string scope)
		{
			Id = id;
			ApiResources_Id = apiResources_Id;
			Scope = scope;
		}
	}
}
