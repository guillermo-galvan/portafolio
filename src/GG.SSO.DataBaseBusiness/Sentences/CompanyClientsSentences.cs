using GG.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Sentences
{
    public class CompanyClientsSentences : SentencesBase
    {
        public CompanyClientsSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public CompanyClientsSentences AddCliteriByClients_Id(int clients_Id,
            LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(CompanyClients.Clients_Id), clients_Id, logicalOperator);

            return this;
        }

        public CompanyClientsSentences AddCliteriByCompany_Id(int company_Id,
            LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(CompanyClients.Company_Id), company_Id, logicalOperator);

            return this;
        }
    }
}
