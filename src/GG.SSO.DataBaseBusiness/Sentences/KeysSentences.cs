using GG.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Sentences
{
    public class KeysSentences : SentencesBase
    {
        public KeysSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        {
        }

        public KeysSentences AddCliteriById(string id, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(Keys.Id), id, logicalOperator);

            return this;
        }
    }
}
