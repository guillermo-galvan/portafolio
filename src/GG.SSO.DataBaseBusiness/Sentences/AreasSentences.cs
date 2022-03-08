using GGPuntoYComa.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGPuntoYComa.SSO.DataBaseBusiness.Sentences
{
    public class AreasSentences : SentencesBase
    {
        public AreasSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public AreasSentences AddCliteriById(int id, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(Areas.Id), id, logicalOperator);

            return this;
        }

        public AreasSentences AddCliteriByName(string name, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(Areas.Name), name, logicalOperator);

            return this;
        }

        public AreasSentences AddCliteriByNormalizedName(string normalizedName, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(Areas.NormalizedName), normalizedName, logicalOperator);

            return this;
        }

        public AreasSentences AddCliteriByConcurrencyStamp(string concurrencyStamp, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(Areas.ConcurrencyStamp), concurrencyStamp, logicalOperator);

            return this;
        }

        public AreasSentences AddInCliteriById(IEnumerable<int> ids, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddInCriteria(nameof(Areas.Id), ids, logicalOperator);

            return this;
        }
    }
}
