using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.Portafolio.Entity.Table.Blog
{
    [DataClassCRUD("blog","user")]
	public class User : Entity<User>
	{
		[DataMemberCRUD("Id", MySqlDbType.VarChar, 450, true)]
		public string Id { get; set; }

		[DataMemberCRUD("Name", MySqlDbType.VarChar, 256)]
		public string Name { get; set; }

		[DataMemberCRUD("Email", MySqlDbType.VarChar, 256)]
		public string Email { get; set; }

		public User()
		{}

		public User(string id,string name,string email)
		{
			Id = id;
			Name = name;
			Email = email;
		}
	}
}
