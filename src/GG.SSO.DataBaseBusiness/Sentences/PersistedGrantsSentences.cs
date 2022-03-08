using GGPuntoYComa.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGPuntoYComa.SSO.DataBaseBusiness.Sentences
{
    public class PersistedGrantsSentences : SentencesBase
    {
        public PersistedGrantsSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        {}

        public PersistedGrantsSentences AddCliteriByKey(string key, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(PersistedGrants.Key), key, logicalOperator);

            return this;
        }

        public PersistedGrantsSentences AddCliteriByType(string type, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(PersistedGrants.Type), type, logicalOperator);

            return this;
        }

        public PersistedGrantsSentences AddCliteriBySubjectId(string subjectId, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(PersistedGrants.SubjectId), subjectId, logicalOperator);

            return this;
        }

        public PersistedGrantsSentences AddCliteriBySessionId(string sessionId, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(PersistedGrants.SessionId), sessionId, logicalOperator);

            return this;
        }

        public PersistedGrantsSentences AddCliteriByClienteId(string clientId, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
             _criteriaBuilder.AddEqualCriteria(nameof(PersistedGrants.ClientId), clientId, logicalOperator);

            return this;
        }

        public PersistedGrantsSentences AddCliteriByIsDeleted(bool isDeleted, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(PersistedGrants.IsDeleted), isDeleted, logicalOperator);

            return this;
        }
    }
}
