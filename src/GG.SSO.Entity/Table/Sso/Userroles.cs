using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GGPuntoYComa.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","userroles")]
	public class UserRoles : Entity<UserRoles>
	{
		[DataMemberCRUD("Users_Id", MySqlDbType.VarChar, 450, true)]
		public string Users_Id { get; set; }

		[DataMemberCRUD("Roles_Id", MySqlDbType.VarChar, 450, true)]
		public string Roles_Id { get; set; }

		public UserRoles()
		{}

		public UserRoles(string users_Id,string roles_Id)
		{
			Users_Id = users_Id;
			Roles_Id = roles_Id;
		}
	}
}
