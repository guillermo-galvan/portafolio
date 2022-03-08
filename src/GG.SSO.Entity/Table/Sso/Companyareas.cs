using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GGPuntoYComa.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","companyareas")]
	public class CompanyAreas : Entity<CompanyAreas>
	{
		[DataMemberCRUD("Company_Id", MySqlDbType.Int32, 10, true)]
		public int Company_Id { get; set; }

		[DataMemberCRUD("Areas_Id", MySqlDbType.Int32, 10, true)]
		public int Areas_Id { get; set; }

		public CompanyAreas()
		{}

		public CompanyAreas(int company_Id,int areas_Id)
		{
			Company_Id = company_Id;
			Areas_Id = areas_Id;
		}
	}
}
