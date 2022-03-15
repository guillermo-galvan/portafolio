using GG.Portafolio.Api.Controllers;
using GG.Portafolio.BusinessLogic.Blog;
using GG.Portafolio.DataBaseBusiness.Business;
using GG.Portafolio.DataBaseBusiness.Sentences;
using GG.Portafolio.Shared.Blog;
using GG.Portafolio.Shared.Test;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GG.Portafolio.Api.Test.Controllers
{
    public class BlogControllerTest
    {
        private readonly ILogger<BlogController> _logger;
        private readonly BlogController _controller;
        private readonly BlogManagement _blogManagement;
        private readonly Mock<IDataBaseConnection> _dataBaseConnectionMock;
        private readonly Mock<IWebHostEnvironment> _mockIWebHostEnvironment;

        public BlogControllerTest()
        {
            ServiceProvider serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            _logger = factory.CreateLogger<BlogController>();

            _dataBaseConnectionMock = MockIDataBaseConnection.GetMockIConnectionDataBase();

            MockIDataBaseConnection.SetUserEntity();
            MockIDataBaseConnection.SetBlogEntity();
            MockIDataBaseConnection.SetBlogDetailEntity();
            MockIDataBaseConnection.SetBlogCommentsEntity();

            _mockIWebHostEnvironment = new();

            _mockIWebHostEnvironment.Setup(m => m.ContentRootPath).Returns(AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
            _mockIWebHostEnvironment.Setup(m => m.WebRootPath).Returns(AppDomain.CurrentDomain.SetupInformation.ApplicationBase);

            _blogManagement = new(factory.CreateLogger<BlogManagement>(),
                new BlogBusiness(factory.CreateLogger<BlogBusiness>(), new BlogSentences(new CriteriaBuilder())),
                new BlogDetailBusiness(new BlogDetailSentences(new CriteriaBuilder()), factory.CreateLogger<BlogDetailBusiness>()),
                new BlogCommentsBusiness(new BlogCommentsSentences(new CriteriaBuilder()), factory.CreateLogger<BlogCommentsBusiness>()),
                new UserBusiness(new UserSentences(new CriteriaBuilder()), factory.CreateLogger<UserBusiness>()));

            _controller = new (_logger, _mockIWebHostEnvironment.Object, _blogManagement);
        }

        [Fact]
        public void BlogController_NotNull()
        {
            Assert.NotNull(new BlogController(_logger, _mockIWebHostEnvironment.Object, _blogManagement));
        }

        [Fact]
        public void BlogController_Get_IsType_BlogResponse()
        {
            var result = _controller.Get();
            var actionResult = Assert.IsType<ActionResult<IEnumerable<BlogResponse>>>(result);
            Assert.NotEmpty(actionResult.Value);
        }

        [Fact]
        public void BlogController_GetById_IsType_BlogContentReponse()
        {
            var result = _controller.GetById(Guid.NewGuid().ToString());
            var actionResult = Assert.IsType<ActionResult<BlogContentReponse>>(result);
            Assert.IsType<BlogContentReponse>(actionResult.Value);
        }

        [Fact]
        public void BlogController_GetById_IsType_NotFound()
        {
            var result = _controller.GetById(string.Empty);
            var actionResult = Assert.IsType<ActionResult<BlogContentReponse>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public void BlogController_Get_IsType_BlogContentWithCommentsReponse()
        {
            var result = _controller.Get(Guid.NewGuid().ToString());
            var actionResult = Assert.IsType<ActionResult<BlogContentWithCommentsReponse>>(result);
            Assert.IsType<BlogContentWithCommentsReponse>(actionResult.Value);
        }

        [Fact]
        public void BlogController_Get_IsType_NotFound()
        {
            var result = _controller.Get(string.Empty);
            var actionResult = Assert.IsType<ActionResult<BlogContentWithCommentsReponse>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public void BlogController_CreateBlog_IsType_BlogOperationResponse()
        {
            string userId = Guid.NewGuid().ToString();

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
            }, "mock"));

            _controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            var result = _controller.CreateBlog(new BlogNewRequest 
            {
                Content = "Detail",
                ContentFiles = new List<ContentFile>(),
                Dsc = "Dsc",
                Title = string.Empty,
                UserId = userId,
            });

            _controller.ControllerContext.HttpContext = null;

            var actionResult = Assert.IsType<ActionResult<BlogOperationResponse>>(result);
            Assert.IsType<BlogOperationResponse>(actionResult.Value);
        }

        [Fact]
        public void BlogController_CreateBlog_IsType_ConflictResult()
        {
            string userId = Guid.NewGuid().ToString();

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
            }, "mock"));

            _controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            var result = _controller.CreateBlog(new BlogNewRequest
            {
                Content = "Detail",
                ContentFiles = new List<ContentFile>(),
                Dsc = "Dsc",
                Title = "Title",
                UserId = userId,
            });

            _controller.ControllerContext.HttpContext = null;
            var actionResult = Assert.IsType<ActionResult<BlogOperationResponse>>(result);
            Assert.IsType<ConflictResult>(actionResult.Result);
        }

        [Fact]
        public void BlogController_CreateBlog_IsType_BadRequestResult()
        {
            string userId = Guid.NewGuid().ToString();

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
            }, "mock"));

            _controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            
            var result = _controller.CreateBlog(new BlogNewRequest
            {
                Content = "Detail",
                ContentFiles = new List<ContentFile>(),
                Dsc = "Dsc",
                Title = string.Empty,
                UserId = string.Empty,
            });
            
            _controller.ControllerContext.HttpContext = null;
            var actionResult = Assert.IsType<ActionResult<BlogOperationResponse>>(result);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void BlogController_EditBlog_IsType_BlogOperationResponse()
        {
            string userId = Guid.NewGuid().ToString();

            _controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var result = _controller.EditBlog(new BlogEditRequest
            {
                Content = "Detail",
                ContentFiles = new List<ContentFile>(),
                Dsc = "Dsc",
                Title = string.Empty,
                UserId = userId,
                Id = Guid.NewGuid().ToString(),
            });
            _controller.ControllerContext.HttpContext = null;
            var actionResult = Assert.IsType<ActionResult<BlogOperationResponse>>(result);
            Assert.IsType<BlogOperationResponse>(actionResult.Value);
        }

        [Fact]
        public void BlogController_EditBlog_IsType_ConflictResult()
        {
            string userId = Guid.NewGuid().ToString();

            _controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var result = _controller.EditBlog(new BlogEditRequest
            {
                Content = "Detail",
                ContentFiles = new List<ContentFile>(),
                Dsc = "Dsc",
                Title = "Title",
                UserId = userId,
                Id = Guid.NewGuid().ToString(),
            });
            _controller.ControllerContext.HttpContext = null;
            var actionResult = Assert.IsType<ActionResult<BlogOperationResponse>>(result);
            Assert.IsType<ConflictResult>(actionResult.Result);
        }

        [Fact]
        public void BlogController_EditBlog_IsType_BadRequest()
        {
            string userId = Guid.NewGuid().ToString();

            var result = _controller.EditBlog(new BlogEditRequest
            {
                Content = string.Empty,
                ContentFiles = new List<ContentFile>(),
                Dsc = string.Empty,
                Title = string.Empty,
                UserId = userId,
                Id = string.Empty,
            });
            var actionResult = Assert.IsType<ActionResult<BlogOperationResponse>>(result);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void BlogController_BlogDelete_IsType_OkResult()
        {
            var result = _controller.BlogDelete(Guid.NewGuid().ToString());
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void BlogController_BlogDelete_IsType_NotFound()
        {
            var result = _controller.BlogDelete(string.Empty);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void BlogController_CommentSave_IsType_OkResult()
        {
            var result = _controller.CommentSave(new BlogComments 
            {
                BlogId = Guid.NewGuid().ToString(),
                Content= "Content",
                Date = DateTime.Now,
                Name = "Name",
                User_Id = Guid.NewGuid().ToString(),
            });
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void BlogController_CommentSave_IsType_BadRequest()
        {
            _controller.ModelState.AddModelError(nameof(BlogComments.BlogId), "Required");
            var result = _controller.CommentSave(new BlogComments
            {
                BlogId = Guid.NewGuid().ToString(),
                Content = "Content",
                Date = DateTime.Now,
                Name = "Name",
                User_Id = Guid.NewGuid().ToString(),
            });
            _controller.ModelState.Clear();
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
