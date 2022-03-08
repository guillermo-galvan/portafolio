using GGPuntoYComa.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGPuntoYComa.SSO.DataBaseBusiness.Sentences
{
    public class IdentityResourcesPropertiesSentences : SentencesBase
    {
        public IdentityResourcesPropertiesSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public IdentityResourcesPropertiesSentences AddCliteriById(int id, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(IdentityResources.Id), id, logicalOperator);

            return this;
        }
    }
}
