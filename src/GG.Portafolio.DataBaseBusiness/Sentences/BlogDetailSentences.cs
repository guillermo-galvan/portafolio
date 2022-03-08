using GG.Portafolio.Entity.Table.Blog;
using System.Data.CRUD;

namespace GG.Portafolio.DataBaseBusiness.Sentences
{
    public class BlogDetailSentences : SentencesBase
    {
        public BlogDetailSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public BlogDetailSentences AddCliteriByBlog_Id(string blog_Id, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(BlogDetail.Blog_Id), blog_Id, logicalOperator);

            return this;
        }
    }
}
