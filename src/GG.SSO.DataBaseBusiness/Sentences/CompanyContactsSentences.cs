using GG.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Sentences
{
    public class CompanyContactsSentences : SentencesBase
    {
        public CompanyContactsSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public CompanyContactsSentences AddCliteriById(int id, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(CompanyContacts.Id), id, logicalOperator);

            return this;
        }

        public CompanyContactsSentences AddCliteriByCompany_Id(int company_Id, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(CompanyContacts.Company_Id), company_Id, logicalOperator);

            return this;
        }
    }
}
