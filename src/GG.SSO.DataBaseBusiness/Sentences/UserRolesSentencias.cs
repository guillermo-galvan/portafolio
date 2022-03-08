using GGPuntoYComa.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGPuntoYComa.SSO.DataBaseBusiness.Sentences
{
    public class UserRolesSentencias : SentencesBase
    {
        public UserRolesSentencias(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public UserRolesSentencias AddCliteriByUsers_Id(string userId, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(UserRoles.Users_Id), userId, logicalOperator);

            return this;
        }

        public UserRolesSentencias AddCliteriByRoles_Id(string rolesId, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(UserRoles.Roles_Id), rolesId, logicalOperator);

            return this;
        }

        public UserRolesSentencias AddInCliteriByRoles_Id(IEnumerable<string> rolesIds, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddInCriteria(nameof(UserRoles.Roles_Id), rolesIds, logicalOperator);

            return this;
        }

    }
}
