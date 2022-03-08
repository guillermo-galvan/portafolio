using System;
using System.Data.CRUD;
using System.Security.Claims;
using MySql.Data.MySqlClient;

namespace GGPuntoYComa.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","roleclaims")]
	public class RoleClaims : Entity<RoleClaims>
	{
		[DataMemberCRUD("Id", MySqlDbType.Int32, 10, true, true)]
		public int Id { get; set; }

		[DataMemberCRUD("Roles_Id", MySqlDbType.VarChar, 450, true)]
		public string Roles_Id { get; set; }

		[DataMemberCRUD("Type", MySqlDbType.VarChar, 7000)]
		public string Type { get; set; }

		[DataMemberCRUD("Value", MySqlDbType.VarChar, 7000)]
		public string Value { get; set; }

		[DataMemberCRUD("ValueType", MySqlDbType.VarChar, 7000)]
		public string ValueType { get; set; }

		public RoleClaims()
		{}

		public RoleClaims(int id,string roles_Id,string type,string value,string valueType)
		{
			Id = id;
			Roles_Id = roles_Id;
			Type = type;
			Value = value;
			ValueType = valueType;
		}
	}
}
