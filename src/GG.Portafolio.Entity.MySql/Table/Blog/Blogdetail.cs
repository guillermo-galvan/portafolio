using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.Portafolio.Entity.Table.Blog
{
    [DataClassCRUD("blog", "blogdetail")]
    public class BlogDetail : Entity<BlogDetail>
    {
        [DataMemberCRUD("Blog_Id", MySqlDbType.VarChar, 450, true)]
        public string Blog_Id { get; set; }

        [DataMemberCRUD("Detail", MySqlDbType.LongText, -1)]
        public string Detail { get; set; }

        public BlogDetail()
        { }

        public BlogDetail(string blog_Id, string detail)
        {
            Blog_Id = blog_Id;
            Detail = detail;
        }
    }
}
