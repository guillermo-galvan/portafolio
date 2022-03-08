using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","usertokens")]
	public class UserTokens : Entity<UserTokens>
	{
		[DataMemberCRUD("Users_Id", MySqlDbType.VarChar, 450, true)]
		public string Users_Id { get; set; }

		[DataMemberCRUD("LoginProvider", MySqlDbType.VarChar, 128, true)]
		public string LoginProvider { get; set; }

		[DataMemberCRUD("Name", MySqlDbType.VarChar, 128, true)]
		public string Name { get; set; }

		[DataMemberCRUD("Value", MySqlDbType.VarChar, 21000)]
		public string Value { get; set; }

		public UserTokens()
		{}

		public UserTokens(string users_Id,string loginProvider,string name,string value)
		{
			Users_Id = users_Id;
			LoginProvider = loginProvider;
			Name = name;
			Value = value;
		}
	}
}
