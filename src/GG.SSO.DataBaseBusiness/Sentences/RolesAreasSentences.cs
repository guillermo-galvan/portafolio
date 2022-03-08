using GGPuntoYComa.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGPuntoYComa.SSO.DataBaseBusiness.Sentences
{
    public class RolesAreasSentences : SentencesBase
    {
        public RolesAreasSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public RolesAreasSentences AddCliteriByAreas_Id(int areas_Id, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(RolesAreas.Areas_Id), areas_Id, logicalOperator);

            return this;
        }

        public RolesAreasSentences AddCliteriByRoles_Id(string roles_Id, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(RolesAreas.Roles_Id), roles_Id, logicalOperator);

            return this;
        }
    }
}
