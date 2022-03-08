using GG.Portafolio.Site.Controllers;
using GG.Portafolio.Site.Generic.Interfaces;
using GG.Portafolio.Site.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Abstractions;
using GG.Portafolio.Site.Test;

namespace GG.Portafolio.Site.Test.Controllers
{
    public class HomeControllerTest
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Mock<IHttpClient> _httpClient;
        private readonly HomeController _controller;

        public HomeControllerTest()
        {
            ServiceProvider serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            _logger = factory.CreateLogger<HomeController>();
            _httpClient = MockIHttpClientFactory.GetMockHttpClient();
            MockIHttpClientFactory.SetIEnumerableBlogResponse();

            _controller = new(_logger, _httpClient.Object) { TempData = Utilities.GetTempDataDictionary() };
        }

        [Fact]
        public void HomeController_NotNull()
        {
            Assert.NotNull(new HomeController(_logger, _httpClient.Object));
        }

        [Theory]
        [InlineData(DefaultViews.Home)]
        [InlineData(DefaultViews.AboutMe)]
        [InlineData(DefaultViews.Portfolio)]
        [InlineData(DefaultViews.Blog)]
        public async void HomeController_Index_ViewResult(DefaultViews defaultViews)
        {
            _controller.TempData["DefaultViews"] = defaultViews;
            var result = await _controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Theory]
        [InlineData("")]
        [InlineData("blog")]
        [InlineData("aboutme")]
        [InlineData("portfolio")]
        public void HomeController_RedirectMenu_RedirectToActionResult(string url)
        {
            var result = _controller.RedirectMenu(url);
            var localRedirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Contains("Index", localRedirectResult.ActionName);
        }

        [Fact]
        public void HomeController_Home_PartialViewResult()
        {
            var result = _controller.Home();
            Assert.IsType<PartialViewResult>(result);
        }

        [Fact]
        public void HomeController_AboutMe_PartialViewResult()
        {
            var result = _controller.AboutMe();
            Assert.IsType<PartialViewResult>(result);
        }

        [Fact]
        public void HomeController_Portfolio_PartialViewResult()
        {
            var result = _controller.Portfolio();
            Assert.IsType<PartialViewResult>(result);
        }

        [Fact]
        public async void HomeController_Blogs_PartialViewResult()
        {
            var result = await _controller.Blogs();
            Assert.IsType<PartialViewResult>(result);
        }

        [Fact]
        public void HomeController_Error_ViewResult()
        {
            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = Utilities.GetClaimsPrincipal(),
            };

            var result = _controller.Error();
            Assert.IsType<ViewResult>(result);
        }
    }
}
