using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.Portafolio.Entity.Table.Blog
{
    [DataClassCRUD("blog","blog")]
	public class Blog : Entity<Blog>
	{
		[DataMemberCRUD("Id", MySqlDbType.VarChar, 450, true)]
		public string Id { get; set; }

		[DataMemberCRUD("User_Id", MySqlDbType.VarChar, 450)]
		public string User_Id { get; set; }

		[DataMemberCRUD("Title", MySqlDbType.VarChar, 100)]
		public string Title { get; set; }

		[DataMemberCRUD("Dsc", MySqlDbType.VarChar, 250)]
		public string Dsc { get; set; }

		[DataMemberCRUD("Create", MySqlDbType.Int64, 19)]
		public long Create { get; set; }

		[DataMemberCRUD("Edit", MySqlDbType.Int64, 19)]
		public long Edit { get; set; }

		[DataMemberCRUD("IsActive", MySqlDbType.Bit, 1)]
		public bool IsActive { get; set; }

		public Blog()
		{}

		public Blog(string id,string user_Id,string title,string dsc,long create,long edit,bool isActive)
		{
			Id = id;
			User_Id = user_Id;
			Title = title;
			Dsc = dsc;
			Create = create;
			Edit = edit;
			IsActive = isActive;
		}
	}
}
