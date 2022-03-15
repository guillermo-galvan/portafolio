using GG.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Sentences
{
    public class CompanyApiScopesSentences : SentencesBase
    {
        public CompanyApiScopesSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public CompanyApiScopesSentences AddCliteriByApiScopes_Id(int apiScopes_Id,
            LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(CompanyApiScopes.ApiScopes_Id), apiScopes_Id, logicalOperator);

            return this;
        }

        public CompanyApiScopesSentences AddCliteriByCompany_Id(int company_Id,
            LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(CompanyApiScopes.Company_Id), company_Id, logicalOperator);

            return this;
        }
    }
}
