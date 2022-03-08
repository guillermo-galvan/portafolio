using GGPuntoYComa.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGPuntoYComa.SSO.DataBaseBusiness.Sentences
{
    public class CompanyRolesSentences : SentencesBase
    {
        public CompanyRolesSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public CompanyRolesSentences AddCliteriByRoles_Id(string roles_Id,
            LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(CompanyRoles.Roles_Id), roles_Id, logicalOperator);

            return this;
        }

        public CompanyRolesSentences AddCliteriByCompany_Id(int company_Id,
            LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(CompanyRoles.Company_Id), company_Id, logicalOperator);

            return this;
        }
    }
}
