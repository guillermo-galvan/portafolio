using GG.Portafolio.Site.Test;
using GG.Portafolio.Site.Test.Data;
using GG.Portafolio.Shared.Blog;
using GG.Portafolio.Site.Controllers;
using GG.Portafolio.Site.Generic.Interfaces;
using GG.Portafolio.Site.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GG.Portafolio.Site.Test.Controllers
{
    public class BlogManagementControllerTest
    {
        private readonly ILogger<BlogManagementController> _logger;
        private readonly Mock<IHttpClientWithToken> _httpClient;
        private readonly IOptions<ConfigurationValues> _configurationValues;
        private readonly Mock<IWebHostEnvironment> _mockIWebHostEnvironment;
        private readonly BlogManagementController _controller;

        public BlogManagementControllerTest()
        {
            ServiceProvider serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            _logger = factory.CreateLogger<BlogManagementController>();

            _mockIWebHostEnvironment = Utilities.GetMockIWebHostEnvironment();
            _httpClient = MockIHttpClientFactory.GetMockIHttpClientWithToken();

            MockIHttpClientFactory.SetBlogResponse();
            MockIHttpClientFactory.SetBlogManagementModel();
            MockIHttpClientFactory.SetBlogOperationResponse();

            _configurationValues = Options.Create(new ConfigurationValues()
            {
                TinymceKey = "TinymceKey"
            });

            _controller = new(_logger, _httpClient.Object, _configurationValues, _mockIWebHostEnvironment.Object)
            {
                TempData = Utilities.GetTempDataDictionary()
            };
        }

        [Fact]
        public void BlogManagementController_NotNull()
        {
            Assert.NotNull(new BlogManagementController(_logger, _httpClient.Object, _configurationValues, _mockIWebHostEnvironment.Object));
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public async void BlogManagementController_Index_IsType_ViewResult(bool isHttpContextUse, bool isEmptyModel)
        {
            if (isHttpContextUse)
            {
                _controller.ControllerContext.HttpContext = new DefaultHttpContext
                {
                    User = Utilities.GetClaimsPrincipal(),
                };
            }

            var result = await _controller.Index();
            _controller.ControllerContext.HttpContext = null;
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<BlogResponse>>(viewResult.ViewData.Model);

            if (isEmptyModel)
            {
                Assert.Empty(model);
            }
            else
            {
                Assert.NotEmpty(model);
            }
        }

        [Theory]
        [InlineData("Test 1", false)]
        [InlineData("Test 2", false)]
        [InlineData("", true)]
        [InlineData(null, true)]
        public async void BlogManagementController_NewEdit_IsType_ViewResult(string title, bool isEmptyModel)
        {
            var result = await _controller.NewEdit(title);
            _controller.TempData.Clear();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<BlogManagementModel>(viewResult.ViewData.Model);
            if (!isEmptyModel)
            {
                Assert.Equal(title, model.Title);
            }
            else
            {
                Assert.True(string.IsNullOrWhiteSpace(model.Title));
            }
        }

        [Theory]
        [ClassData(typeof(ImageLoadTestData))]
        public void BlogManagementController_SaveImage_IsType_JsonResult(ImgageLoad imageLoad, bool success)
        {
            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = Utilities.GetClaimsPrincipal(),
            };

            if (!imageLoad.FileContent.Any())
            {
                _controller.ModelState.AddModelError(nameof(ImgageLoad.FileContent), "Required");
            }

            if (string.IsNullOrWhiteSpace(imageLoad.FileName))
            {
                _controller.ModelState.AddModelError(nameof(ImgageLoad.FileName), "Required");
            }

            var result = _controller.SaveImage(imageLoad);
            _controller.ControllerContext.HttpContext = null;
            _controller.ModelState.Clear();
            var jsonResult = Assert.IsType<JsonResult>(result);
            var model = (ImageLoadResponse)jsonResult.Value;
            Assert.Equal(success, model.Success);
        }

        [Fact]
        public async void BlogManagementController_Save_IsType_RedirectToActionResult()
        {
            MockIHttpClientFactory.SetBlogContentReponse();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = Utilities.GetClaimsPrincipal(),
            };

            var result = await _controller.Save(new BlogManagementModel
            {
                Content = "Content",
                CreateDate = DateTime.Now.Ticks,
                Dsc = "Dsc",
                EditDate = DateTime.Now.Ticks,
                Id = Guid.NewGuid().ToString(),
                Images = string.Empty,
                Title = "Title"
            });
            _controller.ControllerContext.HttpContext = null;
            _controller.ModelState.Clear();
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async void BlogManagementController_Save_IsType_ViewResult()
        {
            MockIHttpClientFactory.SetBlogContentReponse();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = Utilities.GetClaimsPrincipal(),
            };

            _controller.ModelState.AddModelError(nameof(BlogManagementModel.Content), "Required");

            var result = await _controller.Save(new BlogManagementModel
            {
                Content = "Content",
                CreateDate = DateTime.Now.Ticks,
                Dsc = "Dsc",
                EditDate = DateTime.Now.Ticks,
                Id = Guid.NewGuid().ToString(),
                Images = string.Empty,
                Title = "Title"
            });
            _controller.ControllerContext.HttpContext = null;
            _controller.ModelState.Clear();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<BlogManagementModel>(viewResult.ViewData.Model);

            Assert.Equal("Title", model.Title);
        }

        [Fact]
        public async void BlogManagementController_Delete_IsType_ViewResult()
        {
            var result = await _controller.Delete(Guid.NewGuid().ToString());
            _controller.ModelState.Clear();
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
