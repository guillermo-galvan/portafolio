using GG.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Sentences
{
    public  class CompanyAddressesSentences  : SentencesBase
    {
        public CompanyAddressesSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public CompanyAddressesSentences AddCliteriById(int id, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(CompanyAddresses.Id), id, logicalOperator);

            return this;
        }

        public CompanyAddressesSentences AddCliteriByCompany_Id(int company_Id, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(CompanyAddresses.Company_Id), company_Id, logicalOperator);

            return this;
        }
    }
}
