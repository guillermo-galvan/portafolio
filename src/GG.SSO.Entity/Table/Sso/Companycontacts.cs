using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GGPuntoYComa.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","companycontacts")]
	public class CompanyContacts : Entity<CompanyContacts>
	{
		[DataMemberCRUD("Id", MySqlDbType.Int32, 10, true)]
		public int Id { get; set; }

		[DataMemberCRUD("Company_Id", MySqlDbType.Int32, 10, true)]
		public int Company_Id { get; set; }

		[DataMemberCRUD("Name", MySqlDbType.VarChar, 1000)]
		public string Name { get; set; }

		[DataMemberCRUD("PositionWork", MySqlDbType.VarChar, 100)]
		public string PositionWork { get; set; }

		[DataMemberCRUD("PhoneNumber", MySqlDbType.VarChar, 100)]
		public string PhoneNumber { get; set; }

		[DataMemberCRUD("Email", MySqlDbType.VarChar, 1000)]
		public string Email { get; set; }

		public CompanyContacts()
		{}

		public CompanyContacts(int id,int company_Id,string name,string positionWork,string phoneNumber,string email)
		{
			Id = id;
			Company_Id = company_Id;
			Name = name;
			PositionWork = positionWork;
			PhoneNumber = phoneNumber;
			Email = email;
		}
	}
}
