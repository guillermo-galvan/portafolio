using GG.Portafolio.DataBaseBusiness;
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

namespace GG.Portafolio.DataBaseBusiness.Test
{
    public class DatabaseOperationTest
    {
        private readonly ILogger<DatabaseOperation> _logger;
        private readonly Mock<IDataBaseConnection> _dataBaseConnectionMock;
        private readonly DatabaseOperation _operation;

        public DatabaseOperationTest()
        {
            ServiceProvider serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            _logger = factory.CreateLogger<DatabaseOperation>();

            _dataBaseConnectionMock = MockIDataBaseConnection.GetMockIConnectionDataBase();
            _operation = new();
        }

        [Fact]
        public void DatabaseOperation_NotNull()
        {
            Assert.NotNull(new DatabaseOperation());
        }

        [Fact]
        public void DatabaseOperation_WithTransaction()
        {
            try
            {
                _operation.WithTransaction(_dataBaseConnectionMock.Object, new List<DatabaseOperationModel>
                {
                    new DatabaseOperationModel
                    {
                        Entity = new Blog
                        {
                            Create = DateTime.Now.Ticks,
                            Dsc = "Test",
                            Edit = DateTime.Now.Ticks,
                            Id = Guid.NewGuid().ToString(),
                            IsActive = true,
                            Title = "Test",
                            User_Id = Guid.NewGuid().ToString(),
                        },
                        Operation = OperationType.Insert,
                        AfterExecute = (entity) => { return true; },
                    },
                    new DatabaseOperationModel
                    {
                        Entity = new BlogDetail
                        {
                            Blog_Id = Guid.NewGuid().ToString(),
                            Detail  = "Test"
                        },
                        Operation = OperationType.Insert,
                        AfterExecute = (entity) => { return true; },
                    },
                }, _logger);

                Assert.True(true);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void DatabaseOperation_WithoutTransaction()
        {
            try
            {
                _operation.WithoutTransaction(_dataBaseConnectionMock.Object, new List<DatabaseOperationModel>
                {
                    new DatabaseOperationModel
                    {
                        Entity = new Blog
                        {
                            Create = DateTime.Now.Ticks,
                            Dsc = "Test",
                            Edit = DateTime.Now.Ticks,
                            Id = Guid.NewGuid().ToString(),
                            IsActive = true,
                            Title = "Test",
                            User_Id = Guid.NewGuid().ToString(),
                        },
                        Operation = OperationType.Insert,
                        AfterExecute = (entity) => { return true; },
                    },
                    new DatabaseOperationModel
                    {
                        Entity = new BlogDetail
                        {
                            Blog_Id = Guid.NewGuid().ToString(),
                            Detail  = "Test"
                        },
                        Operation = OperationType.Insert,
                        AfterExecute = (entity) => { return true; },
                    },
                }, _logger);

                Assert.True(true);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }
    }
}
