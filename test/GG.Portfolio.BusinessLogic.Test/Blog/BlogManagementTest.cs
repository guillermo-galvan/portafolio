using GG.Portafolio.BusinessLogic.Blog;
using GG.Portafolio.DataBaseBusiness.Business;
using GG.Portafolio.DataBaseBusiness.Sentences;
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

namespace GG.Portafolio.BusinessLogic.Test.Blog
{
    public class BlogManagementTest
    {
        private readonly ILogger<BlogManagement> _logger;
        private readonly Mock<IDataBaseConnection> _dataBaseConnectionMock;
        private readonly BlogManagement _blogManagement;

        public BlogManagementTest()
        {
            ServiceProvider serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            _logger = factory.CreateLogger<BlogManagement>();

            _dataBaseConnectionMock = MockIDataBaseConnection.GetMockIConnectionDataBase();

            MockIDataBaseConnection.SetUserEntity();
            MockIDataBaseConnection.SetBlogEntity();
            MockIDataBaseConnection.SetBlogDetailEntity();
            MockIDataBaseConnection.SetBlogCommentsEntity();

            _blogManagement = new(_logger,
                new BlogBusiness(factory.CreateLogger<BlogBusiness>(), new BlogSentences(new CriteriaBuilder())),
                new BlogDetailBusiness(new BlogDetailSentences(new CriteriaBuilder()), factory.CreateLogger<BlogDetailBusiness>()),
                new BlogCommentsBusiness(new BlogCommentsSentences(new CriteriaBuilder()), factory.CreateLogger<BlogCommentsBusiness>()),
                new UserBusiness(new UserSentences(new CriteriaBuilder()), factory.CreateLogger<UserBusiness>())
                );
        }


        [Fact]
        public void BlogBusiness_NotNull()
        {
            ServiceProvider serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();

            Assert.NotNull(new BlogManagement(_logger,
                new BlogBusiness(factory.CreateLogger<BlogBusiness>(), new BlogSentences(new CriteriaBuilder())),
                new BlogDetailBusiness(new BlogDetailSentences(new CriteriaBuilder()), factory.CreateLogger<BlogDetailBusiness>()),
                new BlogCommentsBusiness(new BlogCommentsSentences(new CriteriaBuilder()), factory.CreateLogger<BlogCommentsBusiness>()),
                new UserBusiness(new UserSentences(new CriteriaBuilder()), factory.CreateLogger<UserBusiness>())
                ));
        }

        [Fact]
        public void BlogManagement_GetBlogs_NotNull()
        {
            Assert.NotNull(_blogManagement.GetBlogs());
        }

        [Fact]
        public void BlogManagement_GetBlogs_NotEmpty()
        {
            Assert.NotEmpty(_blogManagement.GetBlogs());
        }

        [Fact]
        public void BlogManagement_GetBlogById_NotNull()
        {
            Assert.NotNull(_blogManagement.GetBlogById(Guid.NewGuid().ToString()));
        }

        [Fact]
        public void BlogManagement_GetBlogById_Null()
        {
            Assert.Null(_blogManagement.GetBlogById(string.Empty));
        }

        [Fact]
        public void BlogManagement_GetBlogsbyTitle_NotNull()
        {
            Assert.NotNull(_blogManagement.GetBlogs(Guid.NewGuid().ToString()));
        }

        [Fact]
        public void BlogManagement_GetBlogsbyTitle_Null()
        {
            Assert.Null(_blogManagement.GetBlogs(string.Empty));
        }

        [Fact]
        public void BlogManagement_CreateNewBlog_NotNull()
        {
            Assert.NotNull(_blogManagement.CreateNewBlog(new Shared.Blog.BlogNewRequest 
            {
                Content = "Detail",
                ContentFiles = new List<Shared.Blog.ContentFile>(),
                Dsc = "Dsc",
                Title = "Title",
                UserId  = Guid.NewGuid().ToString(),
            }, "Test", "Test"));
        }

        [Fact]
        public void BlogManagement_CreateNewBlog_Errores_NotEmpty()
        {
            var result = _blogManagement.CreateNewBlog(new Shared.Blog.BlogNewRequest
            {
                Content = "Detail",
                ContentFiles = new List<Shared.Blog.ContentFile>(),
                Dsc = "Dsc",
                Title = "Title",
                UserId = Guid.NewGuid().ToString(),
            }, "Test", "Test");

            Assert.NotEmpty(result.Errores);
        }

        [Fact]
        public void BlogManagement_CreateNewBlog_Errores_Empty()
        {
            var result = _blogManagement.CreateNewBlog(new Shared.Blog.BlogNewRequest
            {
                Content = "Detail",
                ContentFiles = new List<Shared.Blog.ContentFile>(),
                Dsc = "Dsc",
                Title = string.Empty,
                UserId = Guid.NewGuid().ToString(),
            }, "Test", "Test");

            Assert.Empty(result.Errores);
        }

        [Fact]
        public void BlogManagement_EditBlog_NotNull()
        {
            Assert.NotNull(_blogManagement.EditBlog(new Shared.Blog.BlogEditRequest
            {
                Content = "Detail",
                ContentFiles = new List<Shared.Blog.ContentFile>(),
                Dsc = "Dsc",
                Title = "Title",
                UserId = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid().ToString(),
            }, "Test", "Test"));
        }

        [Fact]
        public void BlogManagement_EditBlog_Errores_NotEmpty()
        {
            var result = _blogManagement.EditBlog(new Shared.Blog.BlogEditRequest
            {
                Content = "Detail",
                ContentFiles = new List<Shared.Blog.ContentFile>(),
                Dsc = "Dsc",
                Title = "Title",
                UserId = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid().ToString(),
            }, "Test", "Test");

            Assert.NotEmpty(result.Errores);
        }

        [Fact]
        public void BlogManagement_EditBlog_Errores_Empty()
        {
            var result = _blogManagement.EditBlog(new Shared.Blog.BlogEditRequest
            {
                Content = "Detail",
                ContentFiles = new List<Shared.Blog.ContentFile>(),
                Dsc = "Dsc",
                Title = string.Empty,
                UserId = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid().ToString(),
            }, "Test", "Test");

            Assert.Empty(result.Errores);
        }

        [Fact]
        public void BlogManagement_BlogDelete_False()
        {
            Assert.False(_blogManagement.BlogDelete(string.Empty));
        }

        [Fact]
        public void BlogManagement_BlogDelete_True()
        {
            Assert.True(_blogManagement.BlogDelete(Guid.NewGuid().ToString()));
        }

        [Fact]
        public void BlogManagement_CommentSave()
        {
            _blogManagement.CommentSave(new Shared.Blog.BlogComments 
            {
                BlogId = Guid.NewGuid().ToString(),
                Content = "Test",
                Date = DateTime.Now,
                Name = "Test",
                User_Id = Guid.NewGuid().ToString(),
            });
            Assert.True(true);
        }
    }
}
