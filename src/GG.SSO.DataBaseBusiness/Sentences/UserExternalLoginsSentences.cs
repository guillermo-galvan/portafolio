using GG.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Sentences
{
    public class UserExternalLoginsSentences : SentencesBase
    {
        public UserExternalLoginsSentences(ICriteriaBuilder criteriaBuilder) : base(criteriaBuilder)
        { }

        public UserExternalLoginsSentences AddCliteriByUsers_Id(string users_Id, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(UserExternalLogins.Users_Id), users_Id, logicalOperator);

            return this;
        }

        public UserExternalLoginsSentences AddCliteriByProviderKey(string providerKey, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(UserExternalLogins.ProviderKey), providerKey, logicalOperator);

            return this;
        }

        public UserExternalLoginsSentences AddCliteriByProviderDisplayName(string providerDisplayName, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(UserExternalLogins.ProviderDisplayName), providerDisplayName, logicalOperator);

            return this;
        }

        public UserExternalLoginsSentences AddCliteriByLoginProvider(string loginProvider, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            _criteriaBuilder.AddEqualCriteria(nameof(UserExternalLogins.LoginProvider), loginProvider, logicalOperator);

            return this;
        }
    }
}
