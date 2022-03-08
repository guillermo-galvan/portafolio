using GG.SSO.Entity.Table.Sso;
using System.Data.CRUD;

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
