using GGPuntoYComa.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGPuntoYComa.SSO.DataBaseBusiness.Sentences
{
    public class ClientScopesSentences : SentencesBase
    {
        public ClientScopesSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public ClientScopesSentences AddCliteriByClient(int clientId, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(ClientScopes.Clients_Id), clientId, logicalOperator);

            return this;
        }
    }
}
