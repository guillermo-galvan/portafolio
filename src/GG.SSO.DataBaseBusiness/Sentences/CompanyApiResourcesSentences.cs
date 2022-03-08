using GG.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Sentences
{
    public class CompanyApiResourcesSentences : SentencesBase
    {
        public CompanyApiResourcesSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public CompanyApiResourcesSentences AddCliteriByApiResources_Id(int apiResources_Id,
            LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(CompanyApiResources.ApiResources_Id), apiResources_Id, logicalOperator);

            return this;
        }

        public CompanyApiResourcesSentences AddCliteriByCompany_Id(int company_Id,
            LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(CompanyApiResources.Company_Id), company_Id, logicalOperator);

            return this;
        }
    }
}
