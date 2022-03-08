using GGPuntoYComa.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGPuntoYComa.SSO.DataBaseBusiness.Sentences
{
    public class CompanyAreasSentences :SentencesBase
    {
        public CompanyAreasSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public CompanyAreasSentences AddCliteriByAreas_Id(int areas_Id,
            LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(CompanyAreas.Areas_Id), areas_Id, logicalOperator);

            return this;
        }

        public CompanyAreasSentences AddCliteriByCompany_Id(int company_Id,
            LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(CompanyAreas.Company_Id), company_Id, logicalOperator);

            return this;
        }
    }
}
