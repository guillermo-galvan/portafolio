using GG.Portafolio.Site.Test;
using GG.Portafolio.Shared.TemplateWord;
using GG.Portafolio.Site.Controllers;
using GG.Portafolio.Site.Generic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GG.Portafolio.Site.Test.Controllers
{
    public class TemplateWordControllerTest
    {
        private readonly ILogger<TemplateWordController> _logger;

        private readonly Mock<IHttpClient> _httpClient;

        private readonly TemplateWordController _controller;

        public TemplateWordControllerTest()
        {
            ServiceProvider serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            _logger = factory.CreateLogger<TemplateWordController>();

            _httpClient = MockIHttpClientFactory.GetMockHttpClient();
            MockIHttpClientFactory.SetTemplateResponse();
            _controller = new(_logger, _httpClient.Object);
        }

        [Fact]
        public void TemplateWordController_NotNull()
        {
            Assert.NotNull(new TemplateWordController(_logger, _httpClient.Object));
        }

        [Fact]
        public void TemplateWordController_Index_IsType_ViewResult()
        {
            var result = _controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<TemplateRequest>(viewResult.ViewData.Model);
            Assert.True(string.IsNullOrEmpty(model.Name));
        }

        [Fact]
        public async void TemplateWordController_Index_IsType_FileContentResult()
        {
            var result = await _controller.Index(new TemplateRequest
            {
                Name = "Name",
                ColumnName1 = "ColumnName1",
                ColumnName2 = "ColumnName2",
                ColumnName3 = "ColumnName3"
            });
            var viewResult = Assert.IsType<FileContentResult>(result);
            Assert.NotEmpty(viewResult.FileContents);
        }

        [Fact]
        public async void TemplateWordController_Index2_IsType_ViewResult()
        {
            _controller.ModelState.AddModelError("Name", "Required");
            var result = await _controller.Index(new TemplateRequest
            {
                Name = "Name",
                ColumnName1 = "ColumnName1",
                ColumnName2 = "ColumnName2",
                ColumnName3 = "ColumnName3"
            });
            _controller.ModelState.Clear();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<TemplateRequest>(viewResult.ViewData.Model);
            Assert.Equal("Name", model.Name);
        }

        [Fact]
        public async void TemplateWordController_GetTemplate_IsType_FileContentResult()
        {
            var result = await _controller.GetTemplate();
            var viewResult = Assert.IsType<FileContentResult>(result);
            Assert.NotEmpty(viewResult.FileContents);
        }
    }
}
