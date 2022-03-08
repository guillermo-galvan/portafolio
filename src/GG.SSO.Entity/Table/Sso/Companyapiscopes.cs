using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","companyapiscopes")]
	public class CompanyApiScopes : Entity<CompanyApiScopes>
	{
		[DataMemberCRUD("Company_Id", MySqlDbType.Int32, 10, true)]
		public int Company_Id { get; set; }

		[DataMemberCRUD("ApiScopes_Id", MySqlDbType.Int32, 10, true)]
		public int ApiScopes_Id { get; set; }

		public CompanyApiScopes()
		{}

		public CompanyApiScopes(int company_Id,int apiScopes_Id)
		{
			Company_Id = company_Id;
			ApiScopes_Id = apiScopes_Id;
		}
	}
}
