using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GGPuntoYComa.SSO.Entity.Table.Sso
{
    [DataClassCRUD("sso","userclaims")]
	public class UserClaims : Entity<UserClaims>
	{
		[DataMemberCRUD("Id", MySqlDbType.Int32, 10, true, true)]
		public int Id { get; set; }

		[DataMemberCRUD("Users_Id", MySqlDbType.VarChar, 450, true)]
		public string Users_Id { get; set; }

		[DataMemberCRUD("Type", MySqlDbType.VarChar, 7000)]
		public string Type { get; set; }

		[DataMemberCRUD("Value", MySqlDbType.VarChar, 7000)]
		public string Value { get; set; }

		[DataMemberCRUD("ValueType", MySqlDbType.VarChar, 7000)]
		public string ValueType { get; set; }

		public UserClaims()
		{}

		public UserClaims(int id,string users_Id,string type,string value,string valueType)
		{
			Id = id;
			Users_Id = users_Id;
			Type = type;
			Value = value;
			ValueType = valueType;
		}
	}
}
