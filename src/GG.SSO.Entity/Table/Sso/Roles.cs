using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","roles")]
	public class Roles : Entity<Roles>
	{
		[DataMemberCRUD("Id", MySqlDbType.VarChar, 450, true)]
		public string Id { get; set; }

		[DataMemberCRUD("Name", MySqlDbType.VarChar, 256)]
		public string Name { get; set; }

		[DataMemberCRUD("NormalizedName", MySqlDbType.VarChar, 256)]
		public string NormalizedName { get; set; }

		[DataMemberCRUD("ConcurrencyStamp", MySqlDbType.VarChar, 20000)]
		public string ConcurrencyStamp { get; set; }

		public Roles()
		{}

		public Roles(string id,string name,string normalizedName,string concurrencyStamp)
		{
			Id = id;
			Name = name;
			NormalizedName = normalizedName;
			ConcurrencyStamp = concurrencyStamp;
		}
	}
}
