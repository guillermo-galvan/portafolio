using GG.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Sentences
{
    public class UserTokensSentences : SentencesBase
    {
        public UserTokensSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public UserTokensSentences AddCliteriByUsers_Id(string users_Id, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(UserTokens.Users_Id), users_Id, logicalOperator);

            return this;
        }

        public UserTokensSentences AddCliteriByLoginProvider(string loginProvider, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(UserTokens.LoginProvider), loginProvider, logicalOperator);

            return this;
        }

        public UserTokensSentences AddCliteriByName(string name, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(UserTokens.Name), name, logicalOperator);

            return this;
        }

        public UserTokensSentences AddCliteriByValue(string value, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(UserTokens.Value), value, logicalOperator);

            return this;
        }
    }
}
