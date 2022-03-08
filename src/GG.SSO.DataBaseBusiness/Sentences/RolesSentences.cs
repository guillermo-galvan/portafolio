using GG.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Sentences
{
    public class RolesSentences : SentencesBase
    {
        public RolesSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public RolesSentences AddCliteriById(string id, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(Roles.Id), id, logicalOperator);

            return this;
        }

        public RolesSentences AddCliteriByName(string name, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(Roles.Name), name, logicalOperator);

            return this;
        }

        public RolesSentences AddCliteriByNormalizedName(string normalizedName, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(Roles.NormalizedName), normalizedName, logicalOperator);

            return this;
        }

        public RolesSentences AddCliteriByConcurrencyStamp(string concurrencyStamp, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(Roles.ConcurrencyStamp), concurrencyStamp, logicalOperator);

            return this;
        }

        public RolesSentences AddInCliteriaByRoles_Id(IEnumerable<string> rolesIds, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddInCriteria(nameof(Roles.Id), rolesIds, logicalOperator);

            return this;
        }

    }
}
