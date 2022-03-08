using GG.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Sentences
{
    public class IdentityResourceClaimsSentences : SentencesBase
    {
        public IdentityResourceClaimsSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public IdentityResourceClaimsSentences AddCliteriById(int id, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(IdentityResourceClaims.Id), id, logicalOperator);

            return this;
        }
    }
}
