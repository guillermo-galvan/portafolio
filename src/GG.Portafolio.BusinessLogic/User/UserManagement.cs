using GG.Portafolio.DataBaseBusiness;
using GG.Portafolio.DataBaseBusiness.Business;
using GG.Portafolio.Shared.User;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Reflection;

namespace GG.Portafolio.BusinessLogic.User
{
    public class UserManagement
    {
        private readonly UserBusiness _userBusiness;
        private readonly ILogger<UserManagement> _logger;
        private readonly DatabaseOperation _databaseOperation;

        public UserManagement(ILogger<UserManagement> logger, UserBusiness userBusiness, DatabaseOperation databaseOperation)
        {
            _userBusiness = userBusiness;
            _logger = logger;
            _databaseOperation = databaseOperation;
        }

        public bool Validate(UserRequest user)
        {
            try
            {
                var userDb = _userBusiness.GetUserById(user.Subject);

                if (userDb == null)
                {
                    Create(user);
                }
                else
                {
                    if (userDb.Email != user.Email || userDb.Name != user.Name)
                    {
                        userDb.Email = user.Email;
                        userDb.Name = user.Name;
                        Update(userDb);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {user}", MethodBase.GetCurrentMethod().Name, user);
                return false;
            }
        }

        public void Create(UserRequest user)
        {
            try
            {
                Create(user.Subject, user.Email, user.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {user}", MethodBase.GetCurrentMethod().Name, user);
                throw;
            }
        }

        public void Create(string id, string email, string name)
        {
            try
            {
                _databaseOperation.WithTransaction(ConnectionDataBaseCollection.Connections["MainConnection"], new List<DatabaseOperationModel> {
                    new DatabaseOperationModel
                    {
                        Entity = new Entity.Table.Blog.User(id, name, email),
                        Operation = OperationType.Insert
                    }
                }, _logger);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} Id:{id} Email:{email} Name:{name}", MethodBase.GetCurrentMethod().Name, id, email, name);
                throw;
            }
        }

        public void Update(Entity.Table.Blog.User user)
        {
            _databaseOperation.WithTransaction(ConnectionDataBaseCollection.Connections["MainConnection"], new List<DatabaseOperationModel> {
                    new DatabaseOperationModel
                    {
                        Entity = user,
                        Operation = OperationType.Update
                    }
                }, _logger);
        }

    }
}
