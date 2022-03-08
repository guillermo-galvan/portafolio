using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GGPuntoYComa.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","users")]
	public class Users : Entity<Users>
	{
		[DataMemberCRUD("Id", MySqlDbType.VarChar, 450, true)]
		public string Id { get; set; }

		[DataMemberCRUD("UserName", MySqlDbType.VarChar, 256)]
		public string UserName { get; set; }

		[DataMemberCRUD("NormalizedUserName", MySqlDbType.VarChar, 256)]
		public string NormalizedUserName { get; set; }

		[DataMemberCRUD("Email", MySqlDbType.VarChar, 256)]
		public string Email { get; set; }

		[DataMemberCRUD("NormalizedEmail", MySqlDbType.VarChar, 256)]
		public string NormalizedEmail { get; set; }

		[DataMemberCRUD("EmailConfirmed", MySqlDbType.Bit, 1)]
		public bool EmailConfirmed { get; set; }

		[DataMemberCRUD("PasswordHash", MySqlDbType.Text, 65535)]
		public string PasswordHash { get; set; }

		[DataMemberCRUD("SecurityStamp", MySqlDbType.Text, 65535)]
		public string SecurityStamp { get; set; }

		[DataMemberCRUD("ConcurrencyStamp", MySqlDbType.Text, 65535)]
		public string ConcurrencyStamp { get; set; }

		[DataMemberCRUD("PhoneNumber", MySqlDbType.Text, 65535)]
		public string PhoneNumber { get; set; }

		[DataMemberCRUD("PhoneNumberConfirmed", MySqlDbType.Bit, 1)]
		public bool PhoneNumberConfirmed { get; set; }

		[DataMemberCRUD("TwoFactorEnabled", MySqlDbType.Bit, 1)]
		public bool TwoFactorEnabled { get; set; }

		[DataMemberCRUD("LockoutEnd", MySqlDbType.DateTime, 0)]
		public DateTime? LockoutEnd { get; set; }

		[DataMemberCRUD("LockoutEnabled", MySqlDbType.Bit, 1)]
		public bool LockoutEnabled { get; set; }

		[DataMemberCRUD("AccessFailedCount", MySqlDbType.Int32, 10)]
		public int AccessFailedCount { get; set; }

		public Users()
		{}

		public Users(string id,string userName,string normalizedUserName,string email,string normalizedEmail,bool emailConfirmed,string passwordHash,string securityStamp,string concurrencyStamp,string phoneNumber,bool phoneNumberConfirmed,bool twoFactorEnabled,DateTime? lockoutEnd,bool lockoutEnabled,int accessFailedCount)
		{
			Id = id;
			UserName = userName;
			NormalizedUserName = normalizedUserName;
			Email = email;
			NormalizedEmail = normalizedEmail;
			EmailConfirmed = emailConfirmed;
			PasswordHash = passwordHash;
			SecurityStamp = securityStamp;
			ConcurrencyStamp = concurrencyStamp;
			PhoneNumber = phoneNumber;
			PhoneNumberConfirmed = phoneNumberConfirmed;
			TwoFactorEnabled = twoFactorEnabled;
			LockoutEnd = lockoutEnd;
			LockoutEnabled = lockoutEnabled;
			AccessFailedCount = accessFailedCount;
		}
	}
}
