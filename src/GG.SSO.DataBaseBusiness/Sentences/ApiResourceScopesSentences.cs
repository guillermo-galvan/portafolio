using GGPuntoYComa.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGPuntoYComa.SSO.DataBaseBusiness.Sentences
{
    public class ApiResourceScopesSentences : SentencesBase
    {
        public ApiResourceScopesSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public ApiResourceScopesSentences AddInCliteriByApiResources_Id(IEnumerable<int> elements,
            LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddInCriteria(nameof(ApiResourceScopes.ApiResources_Id), elements, logicalOperator);

            return this;
        }
    }
}
