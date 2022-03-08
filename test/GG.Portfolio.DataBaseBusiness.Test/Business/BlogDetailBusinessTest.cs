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
    public class BlogDetailBusinessTest
    {
        private readonly ILogger<BlogDetailBusiness> _logger;
        private readonly Mock<IDataBaseConnection> _dataBaseConnectionMock;
        private readonly BlogDetailBusiness _business;

        public BlogDetailBusinessTest()
        {
            ServiceProvider serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            _logger = factory.CreateLogger<BlogDetailBusiness>();

            _dataBaseConnectionMock = MockIDataBaseConnection.GetMockIConnectionDataBase();

            _business = new(new BlogDetailSentences(new CriteriaBuilder()), _logger);

            MockIDataBaseConnection.SetBlogDetailEntity();
        }

        [Fact]
        public void BlogDetailBusiness_NotNull()
        {
            Assert.NotNull(new BlogDetailBusiness(new BlogDetailSentences(new CriteriaBuilder()), _logger));
        }

        [Fact]
        public void BlogDetailBusiness_Get_NotNull()
        {
            Assert.NotNull(_business.Get("Test"));
        }

        [Fact]
        public void BlogDetailBusiness_Get_Null()
        {
            Assert.Null(_business.Get(string.Empty));
        }
    }
}
