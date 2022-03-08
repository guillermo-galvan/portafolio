using GG.Portafolio.Entity.Table.Blog;
using System.Data.CRUD;

namespace GG.Portafolio.DataBaseBusiness.Sentences
{
    public class BlogCommentsSentences : SentencesBase
    {
        public BlogCommentsSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        {
        }

        public BlogCommentsSentences AddCliteriById(long id, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(BlogComments.Id), id, logicalOperator);

            return this;
        }

        public BlogCommentsSentences AddCliteriByBlog_Id(string blog_Id, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(BlogComments.Blog_Id), blog_Id, logicalOperator);

            return this;
        }

        public BlogCommentsSentences AddCliteriByUser_Id(string user_Id, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(BlogComments.User_Id), user_Id, logicalOperator);

            return this;
        }
    }
}
