using GG.Portafolio.BusinessLogic.User;
using GG.Portafolio.DataBaseBusiness;
using GG.Portafolio.DataBaseBusiness.Business;
using GG.Portafolio.DataBaseBusiness.Sentences;
using GG.Portafolio.Shared.Test;
using GG.Portafolio.Shared.User;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GG.Portafolio.BusinessLogic.Test.User
{
    public class UserManagementTest
    {
        private readonly ILogger<UserManagement> _logger;
        private readonly Mock<IDataBaseConnection> _dataBaseConnectionMock;
        private readonly UserManagement _management;

        public UserManagementTest()
        {
            ServiceProvider serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            _logger = factory.CreateLogger<UserManagement>();

            _dataBaseConnectionMock = MockIDataBaseConnection.GetMockIConnectionDataBase();

            MockIDataBaseConnection.SetUserEntity();

            _management = new(factory.CreateLogger<UserManagement>(),
                new UserBusiness(new UserSentences(new CriteriaBuilder()), factory.CreateLogger<UserBusiness>()),
                new DatabaseOperation()
            );
        }

        [Fact]
        public void UserManagement_NotNull()
        {
            ServiceProvider serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();

            Assert.NotNull(new UserManagement(factory.CreateLogger<UserManagement>(),
                new UserBusiness(new UserSentences(new CriteriaBuilder()), factory.CreateLogger<UserBusiness>()),
                new DatabaseOperation()
            ));
        }

        [Fact]
        public void UserManagement_Validate_True()
        {
            Assert.True(_management.Validate(new UserRequest 
            {
                Email = "Test@gmail.como",
                Name = "Test",
                Subject = Guid.NewGuid().ToString(),
            }));
        }

        [Fact]
        public void UserManagement_Create_True()
        {
            _management.Create(new UserRequest
            {
                Email = "Test@gmail.como",
                Name = "Test",
                Subject = Guid.NewGuid().ToString(),
            });

            Assert.True(true);
        }

        [Fact]
        public void UserManagement_Create1_True()
        {
            _management.Create(Guid.NewGuid().ToString(), "Test@gmail.com", "Test");

            Assert.True(true);
        }

        [Fact]
        public void UserManagement_Update_True()
        {
            _management.Update(new Entity.Table.Blog.User { 
                Name = "Test",
                Email = "test@gmail.com",
                Id = Guid.NewGuid().ToString(),
            });

            Assert.True(true);
        }
    }
}
