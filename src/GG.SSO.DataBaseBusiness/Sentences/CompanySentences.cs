using GGPuntoYComa.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGPuntoYComa.SSO.DataBaseBusiness.Sentences
{
    public class CompanySentences : SentencesBase
    {
        public CompanySentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public CompanySentences AddCliteriById(int id, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(Company.Id), id, logicalOperator);

            return this;
        }

        public CompanySentences AddCliteriByBusinessName(int businessName, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(Company.BusinessName), businessName, logicalOperator);

            return this;
        }

        public CompanySentences AddCliteriByIsDeleted(bool isDeleted, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(Company.IsDeleted), isDeleted, logicalOperator);

            return this;
        }


    }
}
