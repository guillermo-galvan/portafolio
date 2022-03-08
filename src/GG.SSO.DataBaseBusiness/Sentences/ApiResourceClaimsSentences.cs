using GG.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Sentences
{
    public class ApiResourceClaimsSentences : SentencesBase
    {
        public ApiResourceClaimsSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public ApiResourceClaimsSentences AddInCliteriByApiResources_Id(IEnumerable<int> elements,
            LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddInCriteria(nameof(ApiResourceClaims.ApiResources_Id), elements, logicalOperator);

            return this;
        }
    }
}
