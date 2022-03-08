using GG.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Sentences
{
    public class ApiScopesSentences : SentencesBase
    {
        public ApiScopesSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public ApiScopesSentences AddCliteriByIsDeleted(bool isDeleted, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(ApiScopes.IsDeleted), isDeleted, logicalOperator);

            return this;
        }
    }
}
