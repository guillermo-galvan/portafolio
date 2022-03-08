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
    public class BlogCommentsBusinessTest
    {
        private readonly ILogger<BlogCommentsBusiness> _logger;
        private readonly Mock<IDataBaseConnection> _dataBaseConnectionMock;
        private readonly BlogCommentsBusiness _business;

        public BlogCommentsBusinessTest()
        {
            ServiceProvider serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            _logger = factory.CreateLogger<BlogCommentsBusiness>();

            _dataBaseConnectionMock = MockIDataBaseConnection.GetMockIConnectionDataBase();

            _business = new(new BlogCommentsSentences(new CriteriaBuilder()), _logger);

            MockIDataBaseConnection.SetBlogCommentsEntity();

        }

        [Fact]
        public void BlogCommentsBusiness_NotNull()
        {
            Assert.NotNull(new BlogCommentsBusiness(new BlogCommentsSentences(new CriteriaBuilder()), _logger));
        }

        [Fact]
        public void BlogCommentsBusiness_GetBlogComments_NotNull()
        {
            Assert.NotNull(_business.GetBlogComments(string.Empty));
        }

        [Fact]
        public void BlogCommentsBusiness_GetBlogComments_Empty()
        {
            Assert.Empty(_business.GetBlogComments(string.Empty));
        }

    }
}
