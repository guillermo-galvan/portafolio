using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","apiresourceclaims")]
	public class ApiResourceClaims : Entity<ApiResourceClaims>
	{
		[DataMemberCRUD("Id", MySqlDbType.Int32, 10, true, true)]
		public int Id { get; set; }

		[DataMemberCRUD("ApiResources_Id", MySqlDbType.Int32, 10, true)]
		public int ApiResources_Id { get; set; }

		[DataMemberCRUD("Type", MySqlDbType.VarChar, 200)]
		public string Type { get; set; }

		public ApiResourceClaims()
		{}

		public ApiResourceClaims(int id,int apiResources_Id,string type)
		{
			Id = id;
			ApiResources_Id = apiResources_Id;
			Type = type;
		}
	}
}
