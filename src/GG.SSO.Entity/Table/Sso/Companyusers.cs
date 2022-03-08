using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GGPuntoYComa.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","companyusers")]
	public class CompanyUsers : Entity<CompanyUsers>
	{
		[DataMemberCRUD("Company_Id", MySqlDbType.Int32, 10, true)]
		public int Company_Id { get; set; }

		[DataMemberCRUD("Users_Id", MySqlDbType.VarChar, 450, true)]
		public string Users_Id { get; set; }

		public CompanyUsers()
		{}

		public CompanyUsers(int company_Id,string users_Id)
		{
			Company_Id = company_Id;
			Users_Id = users_Id;
		}
	}
}
