using GGPuntoYComa.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGPuntoYComa.SSO.DataBaseBusiness.Sentences
{
    public class ClientCorsOriginsSentences : SentencesBase
    {
        public ClientCorsOriginsSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public ClientCorsOriginsSentences AddCliteriByOrigin(string origin, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(ClientCorsOrigins.Origin), origin, logicalOperator);

            return this;
        }

        public ClientCorsOriginsSentences AddCliteriByClient(int clientId, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(ClientCorsOrigins.Clients_Id), clientId, logicalOperator);

            return this;
        }
    }
}
