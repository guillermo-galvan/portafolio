using GG.Portafolio.Shared.Blog;
using GG.Portafolio.Site.Controllers;
using GG.Portafolio.Site.Generic.Interfaces;
using GG.Portafolio.Site.Test.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace GG.Portafolio.Site.Test.Controllers
{
    public class BlogControllerTest
    {
        private readonly ILogger<BlogController> _logger;
        private readonly Mock<IHttpClient> _httpClient;
        private readonly BlogController _controller;

        public BlogControllerTest()
        {
            ServiceProvider serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            _logger = factory.CreateLogger<BlogController>();
            _httpClient = MockIHttpClientFactory.GetMockHttpClient();
            MockIHttpClientFactory.SetBlogContentWithCommentsReponse();

            _controller = new (_logger,_httpClient.Object);
        }

        [Fact]
        public void BlogController_NotNull()
        {
            Assert.NotNull(new BlogController(_logger, _httpClient.Object));
        }

        [Theory]
        [InlineData("Test 1", false)]
        [InlineData("", true)]
        [InlineData("Test 2", false)]
        [InlineData(null, true)]
        public async void BlogController_Index_IsType_ViewResult(string title, bool IsTitleEmpty)
        {
            var result = await _controller.Index(title);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<BlogContentWithCommentsReponse>(viewResult.ViewData.Model);
            if (!IsTitleEmpty)
            {
                Assert.Equal(title, model.Title);
            }
            else
            { 
                Assert.True(string.IsNullOrWhiteSpace(model.Title));
            }
        }

        [Theory]
        [ClassData(typeof(BlogCommentsTestData))]
        public async void BlogController_SaveComment_IsType_JsonResult(BlogComments comment, bool isHttpContextUse, bool success)
        {
            if (isHttpContextUse)
            {
                _controller.ControllerContext.HttpContext = new DefaultHttpContext
                {
                    User = Utilities.GetClaimsPrincipal(),
                };
            }

            if (string.IsNullOrWhiteSpace(comment.BlogId))
            {
                _controller.ModelState.AddModelError(nameof(BlogComments.BlogId), "Required");
            }

            if (string.IsNullOrWhiteSpace(comment.Content))
            {
                _controller.ModelState.AddModelError(nameof(BlogComments.Content), "Required");
            }

            if (string.IsNullOrWhiteSpace(comment.Name))
            {
                _controller.ModelState.AddModelError(nameof(BlogComments.Name), "Required");
            }

            var result = await _controller.SaveComment(comment);
            _controller.ModelState.Clear();
            _controller.ControllerContext.HttpContext = null;
            var jsonResult = Assert.IsType<JsonResult>(result);
            Assert.NotNull(jsonResult.Value);
            var properties = jsonResult.Value.GetType().GetProperties().Select(x => new { x.Name, Value = x.GetValue(jsonResult.Value) });
            Assert.Contains(properties, x => x.Name == "success");
            Assert.Equal(success, properties.Where(x => x.Name == "success").First().Value);
        }


    }
}
