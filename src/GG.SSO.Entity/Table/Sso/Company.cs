using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","company")]
	public class Company : Entity<Company>
	{
		[DataMemberCRUD("Id", MySqlDbType.Int32, 10, true, true)]
		public int Id { get; set; }

		[DataMemberCRUD("CompanyName", MySqlDbType.VarChar, 1000)]
		public string CompanyName { get; set; }

		[DataMemberCRUD("BusinessName", MySqlDbType.VarChar, 1000)]
		public string BusinessName { get; set; }

		[DataMemberCRUD("Logo", MySqlDbType.VarChar, 2000)]
		public string Logo { get; set; }

		[DataMemberCRUD("WebSite", MySqlDbType.VarChar, 1000)]
		public string WebSite { get; set; }

        [DataMemberCRUD("Created", MySqlDbType.DateTime, 0)]
		public DateTime Created { get; set; }

		[DataMemberCRUD("Updated", MySqlDbType.DateTime, 0)]
		public DateTime Updated { get; set; }

		[DataMemberCRUD("IsDeleted", MySqlDbType.Bit, 1)]
		public bool IsDeleted { get; set; }

		public Company()
		{}

		public Company(int id,string companyName,string businessName,string logo,string webSite, DateTime created,DateTime updated,bool isDeleted)
		{
			Id = id;
			CompanyName = companyName;
			BusinessName = businessName;
			Logo = logo;
			WebSite = webSite;
			Created = created;
			Updated = updated;
			IsDeleted = isDeleted;
		}
	}
}
