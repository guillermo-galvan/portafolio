using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","identityresourceclaims")]
	public class IdentityResourceClaims : Entity<IdentityResourceClaims>
	{
		[DataMemberCRUD("Id", MySqlDbType.Int32, 10, true, true)]
		public int Id { get; set; }

		[DataMemberCRUD("IdentityResources_Id", MySqlDbType.Int32, 10, true)]
		public int IdentityResources_Id { get; set; }

		[DataMemberCRUD("Type", MySqlDbType.VarChar, 200)]
		public string Type { get; set; }

		public IdentityResourceClaims()
		{}

		public IdentityResourceClaims(int id,int identityResources_Id,string type)
		{
			Id = id;
			IdentityResources_Id = identityResources_Id;
			Type = type;
		}
	}
}
