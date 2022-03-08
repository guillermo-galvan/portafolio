using GGPuntoYComa.SSO.BusinesLogic.Helpers;
using GGPuntoYComa.SSO.BusinesLogic.Model.Identity;
using GGPuntoYComa.SSO.DataBaseBusiness.Business;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace GGPuntoYComa.SSO.BusinesLogic.Identity
{
    public class UserStore : IUserStore<ApplicationUser>, IUserEmailStore<ApplicationUser>,
        IUserPhoneNumberStore<ApplicationUser>, IUserTwoFactorStore<ApplicationUser>,
        IUserPasswordStore<ApplicationUser>, IUserRoleStore<ApplicationUser>,
        IUserClaimStore<ApplicationUser>, IUserLoginStore<ApplicationUser>,
        IUserSecurityStampStore<ApplicationUser>, IUserLockoutStore<ApplicationUser>,
        IUserAuthenticationTokenStore<ApplicationUser>, IUserAuthenticatorKeyStore<ApplicationUser>,
        IUserTwoFactorRecoveryCodeStore<ApplicationUser>
    {
        private const string InternalLoginProvider = "[AspNetUserStore]";
        private const string AuthenticatorKeyTokenName = "AuthenticatorKey";
        private const string RecoveryCodeTokenName = "RecoveryCodes";
        private const char CharacterSeparator = ';';

        private readonly ILogger<UserStore> _logger;
        private readonly UsersBusiness _usersBusiness;
        private readonly UsersClientsBusiness _usersClientsBusiness;
        private readonly ClientsBasicBusiness _clientsBasicBusiness;
        private readonly RolesBusiness _rolesBusiness;
        private readonly UserRolesBusiness _userRolesBusiness;
        private readonly UserClaimsBusiness _userClaimsBusiness;
        private readonly UserExternalLoginsBussines _userExternalLoginsBussines;
        private readonly UserTokensBusiness _userTokensBusiness;

        public UserStore(ILogger<UserStore> logger, UsersBusiness usersBusiness,
            UsersClientsBusiness usersClientsBusiness, ClientsBasicBusiness clientsBasicBusiness,
            RolesBusiness rolesBusiness, UserRolesBusiness userRolesBusiness, UserClaimsBusiness userClaimsBusiness,
            UserExternalLoginsBussines userExternalLoginsBussines, UserTokensBusiness userTokensBusiness)
        {
            _logger = logger;
            _usersBusiness = usersBusiness;
            _usersClientsBusiness = usersClientsBusiness;
            _clientsBasicBusiness = clientsBasicBusiness;
            _rolesBusiness = rolesBusiness;
            _userRolesBusiness = userRolesBusiness;
            _userClaimsBusiness = userClaimsBusiness;
            _userExternalLoginsBussines = userExternalLoginsBussines;
            _userTokensBusiness = userTokensBusiness;
        }

        private void Valid(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (user.Clients == null)
            {
                throw new ArgumentNullException(nameof(user.Clients));
            }

            if (user.User == null)
            {
                throw new ArgumentNullException(nameof(user.User));
            }
        }

        private void SaveLog(Exception ex, ApplicationUser user, string methodName)
        {
            _logger.LogError(ex, "{methodName} {User} {Clients}", methodName, user.User, user.Clients);
        }

        private IdentityResult GetIdentityError(Exception ex)
        {
            return IdentityResult.Failed(new IdentityError[]
                {
                    new IdentityError { Code = $"{ex.GetHashCode()}", Description = ex.Message }
                });
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                Valid(user);
                user.User.Id = Guid.NewGuid().ToString();
                using TransactionScope scope = new();
                _usersBusiness.Insert(user.User);

                foreach (var item in user.Clients)
                {
                    _usersClientsBusiness.Insert(new Entity.Table.Sso.UsersClients(user.User.Id, item.Id));
                }

                scope.Complete();
            }
            catch (Exception ex)
            {
                SaveLog(ex, user, MethodBase.GetCurrentMethod().Name);
                return Task.FromResult(GetIdentityError(ex));
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                Valid(user);
                using TransactionScope scope = new();
                foreach (var item in user.Clients)
                {
                    _usersClientsBusiness.Delete(new Entity.Table.Sso.UsersClients(user.User.Id, item.Id));
                }
                _usersBusiness.Delete(user.User);
                scope.Complete();
            }
            catch (Exception ex)
            {
                SaveLog(ex, user, MethodBase.GetCurrentMethod().Name);
                return Task.FromResult(GetIdentityError(ex));
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ApplicationUser result = null;

            try
            {
                var user = _usersBusiness.Get(userId);
                if (user != null)
                {
                    var userClients = _usersClientsBusiness.Get(userId);
                    var clientsBasics = _clientsBasicBusiness.Get(userClients.Select(x => x.Clients_Id)).ToList();
                    result = new ApplicationUser(user, clientsBasics);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{methodName} {userId}", MethodBase.GetCurrentMethod().Name, userId);
            }

            return Task.FromResult(result);
        }

        public Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ApplicationUser result = null;

            try
            {
                var user = _usersBusiness.GetByNormalizedUserName(normalizedUserName);
                if (user != null)
                {
                    var userClients = _usersClientsBusiness.Get(user.Id);
                    var clientsBasics = _clientsBasicBusiness.Get(userClients.Select(x => x.Clients_Id)).ToList();
                    result = new ApplicationUser(user, clientsBasics);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{methodName} {normalizedUserName}",
                    MethodBase.GetCurrentMethod().Name, normalizedUserName);
            }

            return Task.FromResult(result);
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Valid(user);

            return Task.FromResult(user.User.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Valid(user);

            return Task.FromResult(user.User.Id);
        }

        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Valid(user);

            return Task.FromResult(user.User.UserName);
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Valid(user);
            user.User.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Valid(user);
            user.User.UserName = userName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Valid(user);

            user.User.ConcurrencyStamp = Guid.NewGuid().ToString();

            try
            {
                var userClients = _usersClientsBusiness.Get(user.User.Id);

                var newClientes = (from cli in userClients
                                   join ne in user.Clients on cli.Clients_Id equals ne.Id into left
                                   from le in left.DefaultIfEmpty()
                                   where cli == default
                                   select le).ToList();

                using TransactionScope scope = new();

                _usersBusiness.Update(user.User);

                newClientes.ForEach(x =>
                    _usersClientsBusiness.Insert(new Entity.Table.Sso.UsersClients(user.User.Id, x.Id)));

                scope.Complete();

            }
            catch (Exception ex)
            {
                SaveLog(ex, user, MethodBase.GetCurrentMethod().Name);
                return Task.FromResult(GetIdentityError(ex));
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {

        }

        public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);
            user.User.Email = email;
            return Task.CompletedTask;
        }

        public Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Valid(user);

            return Task.FromResult(user.User.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);
            return Task.FromResult(user.User.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);
            user.User.EmailConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ApplicationUser result = null;

            try
            {
                var user = _usersBusiness.GetByNormalizedEmail(normalizedEmail);
                if (user != null)
                {
                    var userClients = _usersClientsBusiness.Get(user.Id);
                    var clientsBasics = _clientsBasicBusiness.Get(userClients.Select(x => x.Clients_Id)).ToList();
                    result = new ApplicationUser(user, clientsBasics);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{methodName} {normalizedEmail}",
                    MethodBase.GetCurrentMethod().Name, normalizedEmail);
            }

            return Task.FromResult(result);
        }

        public Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);
            return Task.FromResult(user.User.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);
            user.User.NormalizedEmail = normalizedEmail;
            return Task.CompletedTask;
        }

        public Task SetPhoneNumberAsync(ApplicationUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);
            user.User.PhoneNumber = phoneNumber;
            return Task.CompletedTask;
        }

        public Task<string> GetPhoneNumberAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);
            return Task.FromResult(user.User.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);
            return Task.FromResult(user.User.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);
            user.User.PhoneNumberConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);
            user.User.TwoFactorEnabled = enabled;
            return Task.CompletedTask;
        }

        public Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);
            return Task.FromResult(user.User.TwoFactorEnabled);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);
            user.User.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);
            return Task.FromResult(user.User.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);
            return Task.FromResult(user.User.PasswordHash != null);
        }

        public Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(roleName));
            }

            string normalizedRoleName = roleName.ToUpper();
            var roleEntity = _rolesBusiness.GetByNormalizedRoleName(normalizedRoleName);

            if (roleEntity == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Role {0} does not exist.", normalizedRoleName));
            }

            try
            {
                using TransactionScope scope = new();
                _userRolesBusiness.Insert(new Entity.Table.Sso.UserRoles(user.User.Id, roleEntity.Id));
                scope.Complete();
            }
            catch (Exception ex)
            {
                SaveLog(ex, user, MethodBase.GetCurrentMethod().Name);
                throw;
            }

            return Task.CompletedTask;
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Valid(user);
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(roleName));
            }
            var normalizedRoleName = roleName.ToUpper();
            try
            {
                var roleEntity = _rolesBusiness.GetByNormalizedRoleName(normalizedRoleName);

                if (roleEntity != null)
                {
                    var userRole = _userRolesBusiness.Get(user.User.Id, roleEntity.Id);

                    if (userRole != null)
                    {
                        using TransactionScope scope = new();
                        _userRolesBusiness.Delete(userRole);
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                SaveLog(ex, user, MethodBase.GetCurrentMethod().Name);
                throw;
            }
            return Task.CompletedTask;
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            IList<string> result = null;

            Valid(user);
            try
            {
                var userRoles = _userRolesBusiness.Get(user.User.Id);
                result = _rolesBusiness.Get(userRoles.Select(x => x.Roles_Id)).Select(x => x.Name).ToList();
            }
            catch (Exception ex)
            {
                SaveLog(ex, user, MethodBase.GetCurrentMethod().Name);
            }

            return Task.FromResult(result);
        }

        public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(roleName));
            }
            var normalizedRoleName = roleName.ToUpper();

            try
            {
                var role = _rolesBusiness.GetByNormalizedRoleName(normalizedRoleName);

                if (role != null)
                {
                    var userRole = _userRolesBusiness.Get(user.User.Id, role.Id);
                    return Task.FromResult(userRole != null);
                }
            }
            catch (Exception ex)
            {
                SaveLog(ex, user, MethodBase.GetCurrentMethod().Name);
            }

            return Task.FromResult(false);
        }

        public Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(roleName));
            }
            List<ApplicationUser> result = new();
            var normalizedRoleName = roleName.ToUpper();

            try
            {
                var role = _rolesBusiness.GetByNormalizedRoleName(normalizedRoleName);

                if (role != null)
                {
                    var userRoles = _userRolesBusiness.GetByRolesId(role.Id);

                    if (userRoles.Any())
                    {
                        var users = _usersBusiness.Get(userRoles.Select(x => x.Users_Id));
                        var userClients = _usersClientsBusiness.Get(users.Select(x => x.Id));
                        var clientsBasics = _clientsBasicBusiness.Get(userClients.Select(x => x.Clients_Id));

                        foreach (var item in users)
                        {
                            var useClie = userClients.Where(x => x.Users_Id == item.Id).Select(x => x.Clients_Id);
                            var clie = clientsBasics.Where(x => useClie.Contains(x.Id)).ToList();
                            result.Add(new ApplicationUser(item, clie));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{methodName} {roleNameroleName}", MethodBase.GetCurrentMethod().Name, roleName);
            }

            return Task.FromResult((IList<ApplicationUser>)result);
        }

        public Task<IList<Claim>> GetClaimsAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);

            List<Claim> result = new();

            try
            {
                var claims = _userClaimsBusiness.Get(user.User.Id);
                result.AddRange(claims.Select(x => Converts.ToClaim(x)));
            }
            catch (Exception ex)
            {
                SaveLog(ex, user, MethodBase.GetCurrentMethod().Name);
            }

            return Task.FromResult((IList<Claim>)result);
        }

        public Task AddClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Valid(user);
            try
            {
                using TransactionScope scope = new();

                foreach (var item in claims)
                {
                    _userClaimsBusiness.Insert(
                        new Entity.Table.Sso.UserClaims(0, user.User.Id, item.Type, item.Value, item.ValueType));
                }

                scope.Complete();
            }
            catch (Exception ex)
            {
                SaveLog(ex, user, MethodBase.GetCurrentMethod().Name);
                throw;
            }

            return Task.CompletedTask;
        }

        public Task ReplaceClaimAsync(ApplicationUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Valid(user);

            try
            {
                var claimDb = _userClaimsBusiness.Get(user.User.Id, claim);

                if (claimDb != null)
                {
                    claimDb.Type = newClaim.Type;
                    claimDb.Value = newClaim.Value;
                    claimDb.ValueType = newClaim.ValueType;

                    using TransactionScope scope = new();
                    _userClaimsBusiness.Update(claimDb);
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                SaveLog(ex, user, MethodBase.GetCurrentMethod().Name);
                throw;
            }

            return Task.CompletedTask;
        }

        public Task RemoveClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var claimsDB = _userClaimsBusiness.Get(user.User.Id);

                var claimsDelete = (from db in claimsDB
                                    join cla in claims on
                                    new { db.Type, db.Value }
                                    equals
                                    new { cla.Type, cla.Value }
                                    select db).ToList();
                claimsDelete.ForEach(x => _userClaimsBusiness.Delete(x));
            }
            catch (Exception ex)
            {
                SaveLog(ex, user, MethodBase.GetCurrentMethod().Name);
                throw;
            }

            return Task.CompletedTask;
        }

        public Task<IList<ApplicationUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            List<ApplicationUser> result = new();

            try
            {
                var userClaim = _userClaimsBusiness.Get(claim);

                if (userClaim.Any())
                {
                    var users = _usersBusiness.Get(userClaim.Select(x => x.Users_Id));
                    var userClients = _usersClientsBusiness.Get(users.Select(x => x.Id));
                    var clientsBasics = _clientsBasicBusiness.Get(userClients.Select(x => x.Clients_Id));

                    foreach (var item in users)
                    {
                        var useClie = userClients.Where(x => x.Users_Id == item.Id).Select(x => x.Clients_Id);
                        result.Add(new ApplicationUser(item, clientsBasics.Where(x => useClie.Contains(x.Id)).ToList()));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{methodName} {claim}", MethodBase.GetCurrentMethod().Name, claim);
            }

            return Task.FromResult((IList<ApplicationUser>)result);
        }

        public Task AddLoginAsync(ApplicationUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Valid(user);
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }

            try
            {
                using TransactionScope scope = new();
                _userExternalLoginsBussines.Insert(new Entity.Table.Sso.UserExternalLogins(user.User.Id, login.LoginProvider, login.ProviderKey, login.ProviderDisplayName));
                scope.Complete();
            }
            catch (Exception ex)
            {
                SaveLog(ex, user, MethodBase.GetCurrentMethod().Name);
                throw;
            }

            return Task.FromResult(false);
        }

        public Task RemoveLoginAsync(ApplicationUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Valid(user);
            try
            {

                var entity = _userExternalLoginsBussines.Get(user.User.Id, loginProvider, providerKey);

                if (entity != null)
                {
                    _userExternalLoginsBussines.Delete(entity);
                }
            }
            catch (Exception ex)
            {
                SaveLog(ex, user, MethodBase.GetCurrentMethod().Name);
                throw;
            }

            return Task.CompletedTask;
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            List<UserLoginInfo> result = new();
            Valid(user);

            try
            {
                var logins = _userExternalLoginsBussines.Get(user.User.Id);
                result.AddRange(logins.Select(x => Converts.ToUserLoginInfo(x)));
            }
            catch (Exception ex)
            {
                SaveLog(ex, user, MethodBase.GetCurrentMethod().Name);
            }

            return Task.FromResult((IList<UserLoginInfo>)result);
        }

        public Task<ApplicationUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ApplicationUser result = null;

            try
            {
                var userLogin = _userExternalLoginsBussines.Get(loginProvider, providerKey);

                if (userLogin != null)
                {
                    var users = _usersBusiness.Get(userLogin.Users_Id);
                    var userClients = _usersClientsBusiness.Get(users.Id);
                    var clientsBasics = _clientsBasicBusiness.Get(userClients.Select(x => x.Clients_Id));
                    result = new ApplicationUser(users, clientsBasics.ToList());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{methodName} {loginProvider} {providerKey}",
                    MethodBase.GetCurrentMethod().Name, loginProvider, providerKey);
            }

            return Task.FromResult(result);
        }

        public Task SetSecurityStampAsync(ApplicationUser user, string stamp, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Valid(user);
            user.User.SecurityStamp = stamp ?? throw new ArgumentNullException(nameof(stamp));
            return Task.CompletedTask;
        }

        public Task<string> GetSecurityStampAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);
            return Task.FromResult(user.User.SecurityStamp);
        }

        public Task<DateTimeOffset?> GetLockoutEndDateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);

            DateTimeOffset? result = user.User.LockoutEnd.HasValue ? DateTime.SpecifyKind(user.User.LockoutEnd.Value, DateTimeKind.Utc) : null;
            return Task.FromResult(result);
        }

        public Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);
            user.User.LockoutEnd = lockoutEnd.HasValue ? lockoutEnd.Value.UtcDateTime : null;
            return Task.CompletedTask;
        }

        public Task<int> IncrementAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);
            user.User.AccessFailedCount++;
            return Task.FromResult(user.User.AccessFailedCount);
        }

        public Task ResetAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);
            user.User.AccessFailedCount = 0;
            return Task.CompletedTask;
        }

        public Task<int> GetAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);
            return Task.FromResult(user.User.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);
            return Task.FromResult(user.User.LockoutEnabled);
        }

        public Task SetLockoutEnabledAsync(ApplicationUser user, bool enabled, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);
            user.User.LockoutEnabled = enabled;
            return Task.CompletedTask;
        }

        public Task SetTokenAsync(ApplicationUser user, string loginProvider, string name, string value, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Valid(user);

            try
            {
                var token = _userTokensBusiness.Get(user.User.Id, loginProvider, name);

                using TransactionScope scope = new();
                if (token == null)
                {
                    _userTokensBusiness.Insert(new Entity.Table.Sso.UserTokens(user.User.Id, loginProvider, name, value));
                }
                else if (token.Value != value)
                {
                    token.Value = value;
                    _userTokensBusiness.Update(token);
                }
                scope.Complete();
            }
            catch (Exception ex)
            {
                SaveLog(ex, user, MethodBase.GetCurrentMethod().Name);
                throw;
            }

            return Task.CompletedTask;
        }

        public Task RemoveTokenAsync(ApplicationUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Valid(user);

            try
            {
                var entry = _userTokensBusiness.Get(user.User.Id, loginProvider, name);

                using TransactionScope scope = new();
                if (entry != null)
                {
                    _userTokensBusiness.Delete(entry);
                }
                scope.Complete();
            }
            catch (Exception ex)
            {
                SaveLog(ex, user, MethodBase.GetCurrentMethod().Name);
                throw;
            }

            return Task.CompletedTask;
        }

        public Task<string> GetTokenAsync(ApplicationUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Valid(user);
            Entity.Table.Sso.UserTokens entry = null;
            try
            {
                entry = _userTokensBusiness.Get(user.User.Id, loginProvider, name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{methodName} {user} {loginProvider} {name}",
                    MethodBase.GetCurrentMethod().Name, user, loginProvider, name);
            }

            return Task.FromResult(entry?.Value);
        }

        public Task SetAuthenticatorKeyAsync(ApplicationUser user, string key, CancellationToken cancellationToken)
            => SetTokenAsync(user, InternalLoginProvider, AuthenticatorKeyTokenName, key, cancellationToken);

        public Task<string> GetAuthenticatorKeyAsync(ApplicationUser user, CancellationToken cancellationToken)
         => GetTokenAsync(user, InternalLoginProvider, AuthenticatorKeyTokenName, cancellationToken);

        public Task ReplaceCodesAsync(ApplicationUser user, IEnumerable<string> recoveryCodes, CancellationToken cancellationToken)
        {
            var mergedCodes = string.Join(CharacterSeparator, recoveryCodes);
            return SetTokenAsync(user, InternalLoginProvider, RecoveryCodeTokenName, mergedCodes, cancellationToken);
        }

        public async Task<bool> RedeemCodeAsync(ApplicationUser user, string code, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Valid(user);
            if (code == null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            try
            {
                var mergedCodes = await GetTokenAsync(user, InternalLoginProvider, RecoveryCodeTokenName, cancellationToken) ?? "";
                var splitCodes = mergedCodes.Split(CharacterSeparator);
                if (splitCodes.Contains(code))
                {
                    var updatedCodes = new List<string>(splitCodes.Where(s => s != code));
                    await ReplaceCodesAsync(user, updatedCodes, cancellationToken);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{methodName} {user} {code}",
                    MethodBase.GetCurrentMethod().Name, user, code);
            }

            return false;
        }

        public async Task<int> CountCodesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Valid(user);
            string mergedCodes = string.Empty;
            try
            {
                mergedCodes = await GetTokenAsync(user, InternalLoginProvider, RecoveryCodeTokenName, cancellationToken) ?? string.Empty;
            }
            catch (Exception ex)
            {
                SaveLog(ex, user, MethodBase.GetCurrentMethod().Name);
            }

            return mergedCodes.Length == 0 ? 0 : mergedCodes.Split(CharacterSeparator).Length;
        }
    }
}
