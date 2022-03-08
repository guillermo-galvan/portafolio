using GG.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Sentences
{
    public class UsersClientsSentences : SentencesBase
    {
        public UsersClientsSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public UsersClientsSentences AddCliteriByUsers_Id(string userId, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(UsersClients.Users_Id), userId, logicalOperator);

            return this;
        }

        public UsersClientsSentences AddCliteriByClients_Id(int clientId, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(UsersClients.Clients_Id), clientId, logicalOperator);

            return this;
        }

        public UsersClientsSentences AddInCliteriByUsers_Id(IEnumerable<string> userId, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddInCriteria(nameof(UsersClients.Users_Id), userId, logicalOperator);

            return this;
        }
    }
}
