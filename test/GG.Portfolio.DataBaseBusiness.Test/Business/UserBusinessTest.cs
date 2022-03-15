using GG.Portafolio.DataBaseBusiness.Business;
using GG.Portafolio.DataBaseBusiness.Sentences;
using GG.Portafolio.Entity.Table.Blog;
using GG.Portafolio.Shared.Test;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GG.Portafolio.DataBaseBusiness.Test.Business
{
    public class UserBusinessTest
    {
        private readonly ILogger<UserBusiness> _logger;
        private readonly Mock<IDataBaseConnection> _dataBaseConnectionMock;
        private readonly UserBusiness _business;

        public UserBusinessTest()
        {
            ServiceProvider serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            _logger = factory.CreateLogger<UserBusiness>();

            _dataBaseConnectionMock = MockIDataBaseConnection.GetMockIConnectionDataBase();

            _business = new(new UserSentences(new CriteriaBuilder()), _logger);

            MockIDataBaseConnection.SetUserEntity();
        }

        [Fact]
        public void UserBusiness_NotNull()
        {
            Assert.NotNull(new UserBusiness(new UserSentences(new CriteriaBuilder()), _logger));
        }

        [Fact]
        public void UserBusiness_GetUser_NotNull()
        {
            Assert.NotNull(_business.GetUser(Guid.NewGuid().ToString(), "tests@gmail.com","Test"));
        }

        [Fact]
        public void UserBusiness_GetUser_Null_Id()
        {
            Assert.Null(_business.GetUser(string.Empty, "tests@gmail.com", "Test"));
        }

        [Fact]
        public void UserBusiness_GetUser_Null_Email()
        {
            Assert.Null(_business.GetUser(Guid.NewGuid().ToString(), string.Empty, "Test"));
        }

        [Fact]
        public void UserBusiness_GetUser_Null_Name()
        {
            Assert.Null(_business.GetUser(Guid.NewGuid().ToString(), "tests@gmail.com", string.Empty));
        }

        [Fact]
        public void UserBusiness_GetUserById_NotNull()
        {
            Assert.NotNull(_business.GetUserById(Guid.NewGuid().ToString()));
        }

        [Fact]
        public void UserBusiness_GetUserById_Null()
        {
            Assert.Null(_business.GetUserById(string.Empty));
        }
    }
}
