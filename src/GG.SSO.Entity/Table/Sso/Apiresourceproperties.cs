using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","apiresourceproperties")]
	public class ApiResourceProperties : Entity<ApiResourceProperties>
	{
		[DataMemberCRUD("Id", MySqlDbType.Int32, 10, true, true)]
		public int Id { get; set; }

		[DataMemberCRUD("ApiResources_Id", MySqlDbType.Int32, 10, true)]
		public int ApiResources_Id { get; set; }

		[DataMemberCRUD("Key", MySqlDbType.VarChar, 250)]
		public string Key { get; set; }

		[DataMemberCRUD("Value", MySqlDbType.VarChar, 2000)]
		public string Value { get; set; }

		public ApiResourceProperties()
		{}

		public ApiResourceProperties(int id,int apiResources_Id,string key,string value)
		{
			Id = id;
			ApiResources_Id = apiResources_Id;
			Key = key;
			Value = value;
		}
	}
}
