using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GGPuntoYComa.SSO.Entity.Table.Sso
{
    [DataClassCRUD("sso","userexternallogins")]
	public class UserExternalLogins : Entity<UserExternalLogins>
	{
		[DataMemberCRUD("Users_Id", MySqlDbType.VarChar, 450, true)]
		public string Users_Id { get; set; }

		[DataMemberCRUD("LoginProvider", MySqlDbType.VarChar, 128, true)]
		public string LoginProvider { get; set; }

		[DataMemberCRUD("ProviderKey", MySqlDbType.VarChar, 128, true)]
		public string ProviderKey { get; set; }

		[DataMemberCRUD("ProviderDisplayName", MySqlDbType.VarChar, 21000)]
		public string ProviderDisplayName { get; set; }

		public UserExternalLogins()
		{}

		public UserExternalLogins(string users_Id,string loginProvider,string providerKey,string providerDisplayName)
		{
			Users_Id = users_Id;
			LoginProvider = loginProvider;
			ProviderKey = providerKey;
			ProviderDisplayName = providerDisplayName;
		}
	}
}
