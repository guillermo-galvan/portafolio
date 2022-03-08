using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GGPuntoYComa.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","companyroles")]
	public class CompanyRoles : Entity<CompanyRoles>
	{
		[DataMemberCRUD("Company_Id", MySqlDbType.Int32, 10, true)]
		public int Company_Id { get; set; }

		[DataMemberCRUD("Roles_Id", MySqlDbType.VarChar, 450, true)]
		public string Roles_Id { get; set; }

		public CompanyRoles()
		{}

		public CompanyRoles(int company_Id,string roles_Id)
		{
			Company_Id = company_Id;
			Roles_Id = roles_Id;
		}
	}
}
