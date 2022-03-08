using GG.Portafolio.Entity.Table.Blog;
using System.Data.CRUD;

namespace GG.Portafolio.DataBaseBusiness.Sentences
{
    public class UserSentences : SentencesBase
    {
        public UserSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public UserSentences AddCliteriById(string id, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(User.Id), id, logicalOperator);

            return this;
        }

        public UserSentences AddCliteriByEmail(string email, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(User.Email), email, logicalOperator);

            return this;
        }

        public UserSentences AddCliteriByName(string name, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(User.Name), name, logicalOperator);

            return this;
        }
    }
}
