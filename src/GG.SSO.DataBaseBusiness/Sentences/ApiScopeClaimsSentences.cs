using GG.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Sentences
{
    public class ApiScopeClaimsSentences : SentencesBase
    {
        public ApiScopeClaimsSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public ApiScopeClaimsSentences AddInCliteriByApiScopes_Id(IEnumerable<int> elements,
            LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddInCriteria(nameof(ApiScopeClaims.ApiScopes_Id), elements, logicalOperator);

            return this;
        }
    }
}
