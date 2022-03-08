using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","rolesareas")]
	public class RolesAreas : Entity<RolesAreas>
	{
		[DataMemberCRUD("Roles_Id", MySqlDbType.VarChar, 450, true)]
		public string Roles_Id { get; set; }

		[DataMemberCRUD("Areas_Id", MySqlDbType.Int32, 10, true)]
		public int Areas_Id { get; set; }

		public RolesAreas()
		{}

		public RolesAreas(string roles_Id,int areas_Id)
		{
			Roles_Id = roles_Id;
			Areas_Id = areas_Id;
		}
	}
}
