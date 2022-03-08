using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GGPuntoYComa.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","companyclients")]
	public class CompanyClients : Entity<CompanyClients>
	{
		[DataMemberCRUD("Company_Id", MySqlDbType.Int32, 10, true)]
		public int Company_Id { get; set; }

		[DataMemberCRUD("Clients_Id", MySqlDbType.Int32, 10, true)]
		public int Clients_Id { get; set; }

		public CompanyClients()
		{}

		public CompanyClients(int company_Id,int clients_Id)
		{
			Company_Id = company_Id;
			Clients_Id = clients_Id;
		}
	}
}
