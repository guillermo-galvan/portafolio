using GG.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Sentences
{
    public class UsersSentences : SentencesBase
    {
        public UsersSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public UsersSentences AddCliteriById(string id,LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(Users.Id), id, logicalOperator);

            return this;
        }

        public UsersSentences AddCliteriByNormalizedUserName(string normalizedUserName, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(Users.NormalizedUserName), normalizedUserName, logicalOperator);

            return this;
        }

        public UsersSentences AddCliteriByNormalizedEmail(string normalizedEmail, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(Users.NormalizedEmail), normalizedEmail, logicalOperator);

            return this;
        }

        public UsersSentences AddInCliteriById(IEnumerable<string> ids, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddInCriteria(nameof(Users.Id), ids, logicalOperator);

            return this;
        }
    }
}
