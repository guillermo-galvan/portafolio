using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","identityproviders")]
	public class Identityproviders : Entity<Identityproviders>
	{
		[DataMemberCRUD("Id", MySqlDbType.Int32, 10, true, true)]
		public int Id { get; set; }

		[DataMemberCRUD("Scheme", MySqlDbType.VarChar, 200)]
		public string Scheme { get; set; }

		[DataMemberCRUD("DisplayName", MySqlDbType.VarChar, 200)]
		public string DisplayName { get; set; }

		[DataMemberCRUD("Enabled", MySqlDbType.Bit, 1)]
		public bool Enabled { get; set; }

		[DataMemberCRUD("Type", MySqlDbType.VarChar, 20)]
		public string Type { get; set; }

		[DataMemberCRUD("Properties", MySqlDbType.VarChar, 20000)]
		public string Properties { get; set; }

		public Identityproviders()
		{}

		public Identityproviders(int id,string scheme,string displayName,bool enabled,string type,string properties)
		{
			Id = id;
			Scheme = scheme;
			DisplayName = displayName;
			Enabled = enabled;
			Type = type;
			Properties = properties;
		}
	}
}
