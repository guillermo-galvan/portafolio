using GG.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Sentences
{
    public class ClientPostLogoutRedirectUrisSentences : SentencesBase
    {
        public ClientPostLogoutRedirectUrisSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public ClientPostLogoutRedirectUrisSentences AddCliteriByClient(int clientId, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(ClientPostLogoutRedirectUris.Clients_Id), clientId, logicalOperator);

            return this;
        }
    }
}
