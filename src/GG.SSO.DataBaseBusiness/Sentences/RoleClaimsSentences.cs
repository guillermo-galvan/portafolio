using GGPuntoYComa.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGPuntoYComa.SSO.DataBaseBusiness.Sentences
{
    public class RoleClaimsSentences : SentencesBase
    {
        public RoleClaimsSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public RoleClaimsSentences AddCliteriByRoles_Id(string rolesId, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(RoleClaims.Roles_Id), rolesId, logicalOperator);

            return this;
        }

        public RoleClaimsSentences AddCliteriByType(string type, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(RoleClaims.Type), type, logicalOperator);

            return this;
        }

        public RoleClaimsSentences AddCliteriByValue(string value, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(RoleClaims.Value), value, logicalOperator);

            return this;
        }

    }
}
