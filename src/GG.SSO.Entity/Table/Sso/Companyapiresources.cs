using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","companyapiresources")]
	public class CompanyApiResources : Entity<CompanyApiResources>
	{
		[DataMemberCRUD("Company_Id", MySqlDbType.Int32, 10, true)]
		public int Company_Id { get; set; }

		[DataMemberCRUD("ApiResources_Id", MySqlDbType.Int32, 10, true)]
		public int ApiResources_Id { get; set; }

		public CompanyApiResources()
		{}

		public CompanyApiResources(int company_Id,int apiResources_Id)
		{
			Company_Id = company_Id;
			ApiResources_Id = apiResources_Id;
		}
	}
}
