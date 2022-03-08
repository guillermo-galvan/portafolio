using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GGPuntoYComa.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","identityresourceproperties")]
	public class IdentityResourceProperties : Entity<IdentityResourceProperties>
	{
		[DataMemberCRUD("Id", MySqlDbType.Int32, 10, true, true)]
		public int Id { get; set; }

		[DataMemberCRUD("IdentityResources_Id", MySqlDbType.Int32, 10, true)]
		public int IdentityResources_Id { get; set; }

		[DataMemberCRUD("Key", MySqlDbType.VarChar, 250)]
		public string Key { get; set; }

		[DataMemberCRUD("Value", MySqlDbType.VarChar, 2000)]
		public string Value { get; set; }

		public IdentityResourceProperties()
		{}

		public IdentityResourceProperties(int id,int identityResources_Id,string key,string value)
		{
			Id = id;
			IdentityResources_Id = identityResources_Id;
			Key = key;
			Value = value;
		}
	}
}
