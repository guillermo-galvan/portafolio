using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","apiscopeproperties")]
	public class ApiScopeProperties : Entity<ApiScopeProperties>
	{
		[DataMemberCRUD("Id", MySqlDbType.Int32, 10, true, true)]
		public int Id { get; set; }

		[DataMemberCRUD("ApiScopes_Id", MySqlDbType.Int32, 10, true)]
		public int ApiScopes_Id { get; set; }

		[DataMemberCRUD("Key", MySqlDbType.VarChar, 250)]
		public string Key { get; set; }

		[DataMemberCRUD("Value", MySqlDbType.VarChar, 2000)]
		public string Value { get; set; }

		public ApiScopeProperties()
		{}

		public ApiScopeProperties(int id,int apiScopes_Id,string key,string value)
		{
			Id = id;
			ApiScopes_Id = apiScopes_Id;
			Key = key;
			Value = value;
		}
	}
}
