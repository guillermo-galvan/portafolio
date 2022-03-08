using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.Portafolio.Entity.Table.Blog
{
    [DataClassCRUD("blog", "blogcomments")]
    public class BlogComments : Entity<BlogComments>
    {
        [DataMemberCRUD("Id", MySqlDbType.Int64, 19, true, true)]
        public long Id { get; set; }

        [DataMemberCRUD("Blog_Id", MySqlDbType.VarChar, 450)]
        public string Blog_Id { get; set; }

        [DataMemberCRUD("Comment", MySqlDbType.Text, 65535)]
        public string Comment { get; set; }

        [DataMemberCRUD("Create", MySqlDbType.Int64, 19)]
        public long Create { get; set; }

        [DataMemberCRUD("User_Id", MySqlDbType.VarChar, 450)]
        public string User_Id { get; set; }

        public BlogComments()
        { }

        public BlogComments(long id, string blog_Id, string comment, long create, string user_Id)
        {
            Id = id;
            Blog_Id = blog_Id;
            Comment = comment;
            Create = create;
            User_Id = user_Id;
        }
    }
}
