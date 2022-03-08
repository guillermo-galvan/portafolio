using GG.Portafolio.Entity.Table.Blog;
using System.Data.CRUD;

namespace GG.Portafolio.DataBaseBusiness.Sentences
{
    public class BlogSentences : SentencesBase
    {
        public BlogSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public BlogSentences AddCliteriById(string id, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(Blog.Id), id, logicalOperator);

            return this;
        }

        public BlogSentences AddCliteriByTitle(string title, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(Blog.Title), title, logicalOperator);

            return this;
        }

        public BlogSentences AddCliteriByUser_Id(string user_Id, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(Blog.User_Id), user_Id, logicalOperator);

            return this;
        }

        public BlogSentences AddCliteriByIsActive(bool isActive, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(Blog.IsActive), isActive, logicalOperator);

            return this;
        }
    }
}
