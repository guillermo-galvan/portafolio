using GG.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Sentences
{
    public class ClientsBasicSentences : SentencesBase
    {
        public ClientsBasicSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public ClientsBasicSentences AddCliteriById(int id, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(ClientsBasic.Id), id, logicalOperator);

            return this;
        }

        public ClientsBasicSentences AddCliteriByClientId(string clientId, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(ClientsBasic.ClientId), clientId, logicalOperator);

            return this;
        }

        public ClientsBasicSentences AddCliteriByIsDeleted(bool isDeleted, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(ClientsBasic.IsDeleted), isDeleted, logicalOperator);

            return this;
        }

        public ClientsBasicSentences AddInCliteriById(IEnumerable<int> ids, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddInCriteria(nameof(ClientsBasic.Id), ids, logicalOperator);

            return this;
        }
    }
}
