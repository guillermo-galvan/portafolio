using GG.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Sentences
{
    public class ApiResourceSecretsSentences : SentencesBase
    {
        public ApiResourceSecretsSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public ApiResourceSecretsSentences AddInCliteriByApiResources_Id(IEnumerable<int> elements,
            LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddInCriteria(nameof(ApiResourceSecrets.ApiResources_Id), elements, logicalOperator);

            return this;
        }
    }
}
