using GG.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Sentences
{
    public class ApiResourcesSentences : SentencesBase
    {
        public ApiResourcesSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public ApiResourcesSentences AddCliteriByIsDeleted(bool isDeleted, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(ApiResources.IsDeleted), isDeleted, logicalOperator);

            return this;
        }
    }
}
