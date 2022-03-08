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
    public class BlogBusinessTest
    {
        private readonly ILogger<BlogBusiness> _logger;
        private readonly Mock<IDataBaseConnection> _dataBaseConnectionMock;
        private readonly BlogBusiness _blogBusiness;

        public BlogBusinessTest()
        {
            ServiceProvider serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            _logger = factory.CreateLogger<BlogBusiness>();

            _dataBaseConnectionMock = MockIDataBaseConnection.GetMockIConnectionDataBase();

            MockIDataBaseConnection.SetBlogEntity();

            _blogBusiness = new(_logger, new BlogSentences(new CriteriaBuilder()));
        }

        [Fact]
        public void BlogBusiness_NotNull()
        {
            Assert.NotNull(new BlogBusiness(_logger, new BlogSentences(new CriteriaBuilder())));
        }

        [Fact]
        public void BlogBusiness_GetBlogs_NotNull()
        {
            Assert.NotNull(_blogBusiness.GetBlogs());
        }

        [Fact]
        public void BlogBusiness_GetBlogs_NotEmpty()
        {
            Assert.NotEmpty(_blogBusiness.GetBlogs());
        }

        [Fact]
        public void BlogBusiness_GetBlogByTitle_NotNull()
        {
            Assert.NotNull(_blogBusiness.GetBlogByTitle("Test"));
        }

        [Fact]
        public void BlogBusiness_GetBlogByTitle_Null()
        {
            Assert.Null(_blogBusiness.GetBlogByTitle(string.Empty));
        }

        [Fact]
        public void BlogBusiness_GetBlogById_NotNull()
        {
            Assert.NotNull(_blogBusiness.GetBlogById("Test"));
        }

        [Fact]
        public void BlogBusiness_GetBlogById_Null()
        {
            Assert.Null(_blogBusiness.GetBlogById(string.Empty));
        }

    }
}
