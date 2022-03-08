using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","companyaddresses")]
	public class CompanyAddresses : Entity<CompanyAddresses>
	{
		[DataMemberCRUD("Id", MySqlDbType.Int32, 10, true, true)]
		public int Id { get; set; }

		[DataMemberCRUD("Company_Id", MySqlDbType.Int32, 10, true)]
		public int Company_Id { get; set; }

		[DataMemberCRUD("Country", MySqlDbType.VarChar, 450)]
		public string Country { get; set; }

		[DataMemberCRUD("State", MySqlDbType.VarChar, 450)]
		public string State { get; set; }

		[DataMemberCRUD("City", MySqlDbType.VarChar, 450)]
		public string City { get; set; }

		[DataMemberCRUD("Municipality", MySqlDbType.VarChar, 450)]
		public string Municipality { get; set; }

		[DataMemberCRUD("Colony", MySqlDbType.VarChar, 450)]
		public string Colony { get; set; }

		[DataMemberCRUD("Address", MySqlDbType.VarChar, 1000)]
		public string Address { get; set; }

		[DataMemberCRUD("Postcode", MySqlDbType.VarChar, 20)]
		public string Postcode { get; set; }

		public CompanyAddresses()
		{}

		public CompanyAddresses(int id,int company_Id,string country,string state,string city,string municipality,string colony,string address,string postcode)
		{
			Id = id;
			Company_Id = company_Id;
			Country = country;
			State = state;
			City = city;
			Municipality = municipality;
			Colony = colony;
			Address = address;
			Postcode = postcode;
		}
	}
}
