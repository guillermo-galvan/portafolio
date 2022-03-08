using GG.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Sentences
{
    public class UserClaimsSentences : SentencesBase
    {
        public UserClaimsSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public UserClaimsSentences AddCliteriById(int id, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(UserClaims.Id), id, logicalOperator);

            return this;
        }

        public UserClaimsSentences AddCliteriByUsers_Id(string users_id, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(UserClaims.Users_Id), users_id, logicalOperator);

            return this;
        }

        public UserClaimsSentences AddCliteriByType(string type, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(UserClaims.Type), type, logicalOperator);

            return this;
        }

        public UserClaimsSentences AddCliteriByValue(string value, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(UserClaims.Value), value, logicalOperator);

            return this;
        }

        public UserClaimsSentences AddCliteriByValueType(string valueType, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(UserClaims.ValueType), valueType, logicalOperator);

            return this;
        }

    }
}
