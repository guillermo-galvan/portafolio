using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","areas")]
	public class Areas : Entity<Areas>
	{
		[DataMemberCRUD("Id", MySqlDbType.Int32, 10, true, true)]
		public int Id { get; set; }

		[DataMemberCRUD("Name", MySqlDbType.VarChar, 256)]
		public string Name { get; set; }

		[DataMemberCRUD("NormalizedName", MySqlDbType.VarChar, 256)]
		public string NormalizedName { get; set; }

		[DataMemberCRUD("ConcurrencyStamp", MySqlDbType.VarChar, 2000)]
		public string ConcurrencyStamp { get; set; }

		[DataMemberCRUD("IsDefault", MySqlDbType.Bit, 1)]
		public bool IsDefault { get; set; }

		public Areas()
		{}

		public Areas(int id,string name,string normalizedName,string concurrencyStamp, bool isDefault)
		{
			Id = id;
			Name = name;
			NormalizedName = normalizedName;
			ConcurrencyStamp = concurrencyStamp;
			IsDefault = isDefault;
		}
	}
}
