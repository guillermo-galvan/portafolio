using GG.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Sentences
{
    public class IdentityResourcesSentences : SentencesBase
    {
        public IdentityResourcesSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        {}

        public IdentityResourcesSentences AddCliteriById(int id, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(IdentityResources.Id), id, logicalOperator);

            return this;
        }

        public IdentityResourcesSentences AddCliteriByName(string name, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(IdentityResources.Name), name, logicalOperator);

            return this;
        }

        public IdentityResourcesSentences AddInCliteriaByName(IEnumerable<string> names, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddInCriteria(nameof(IdentityResources.Name), names, logicalOperator);

            return this;
        }
    }
}
