using GGPuntoYComa.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGPuntoYComa.SSO.DataBaseBusiness.Sentences
{
    public class ClientIdPRestrictionsSentences : SentencesBase
    {
        public ClientIdPRestrictionsSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public ClientIdPRestrictionsSentences AddCliteriByClient(int clientId, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(ClientIdPRestrictions.Clients_Id), clientId, logicalOperator);

            return this;
        }
    }
}
