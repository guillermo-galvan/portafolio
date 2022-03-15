using GG.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Sentences
{
    public class ClientsSentences: SentencesBase
    {
        public ClientsSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public ClientsSentences AddCliteriByClient(string clientId, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(Clients.ClientId), clientId, logicalOperator);

            return this;
        }

        public ClientsSentences AddCliteriByIsDeleted(bool isDeleted, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(Clients.IsDeleted), isDeleted, logicalOperator);

            return this;
        }
    }
}
