using GG.SSO.DataBaseBusiness.Sentences;
using GG.SSO.Entity.Table.Sso;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.DataBaseBusiness.Business
{
    public class UserClaimsBusiness
    {
        private readonly UserClaimsSentences _userClaimsSentences;
        private readonly ILogger<UserClaimsBusiness> _logger;

        public UserClaimsBusiness(ILogger<UserClaimsBusiness> logger, UserClaimsSentences userClaimsSentences)
        {
            _userClaimsSentences = userClaimsSentences;
            _logger = logger;
        }

        public void Insert(UserClaims userClaims)
        {
            try
            {
                userClaims.Insert();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Insert {userClaims}", userClaims);
                throw;
            }
        }

        public void Update(UserClaims userClaims)
        {
            try
            {
                userClaims.Update();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update {userClaims}", userClaims);
                throw;
            }
        }

        public void Delete(UserClaims userClaims)
        {
            try
            {
                userClaims.Delete();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete {userClaims}", userClaims);
                throw;
            }
        }

        public IEnumerable<UserClaims> Get(string users_Id)
        {
            try
            {
                return UserClaims.Read(_userClaimsSentences.AddCliteriByUsers_Id(users_Id).GetCriteriaCollection());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get {users_Id}", users_Id);
                throw;
            }
        }

        public UserClaims Get(string users_id, Claim claim)
        {
            try
            {
                return UserClaims.Read(_userClaimsSentences.AddCliteriByUsers_Id(users_id)
                                                           .AddCliteriByType(claim.Type)
                                                           .AddCliteriByValue(claim.Type)
                                                           .GetCriteriaCollection()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get {users_Id} {claim}", users_id, claim);
                throw;
            }
        }

        public IEnumerable<UserClaims> Get(Claim claim)
        {
            try
            {
                return UserClaims.Read(_userClaimsSentences.AddCliteriByType(claim.Type)
                                                           .AddCliteriByValue(claim.Type)
                                                           .GetCriteriaCollection());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get {claim}", claim);
                throw;
            }
        }
    }
}
