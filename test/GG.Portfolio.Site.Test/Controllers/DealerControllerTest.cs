using GG.Portafolio.Site.Test;
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
    public class DealerControllerTest
    {
        private readonly ILogger<DealerController> _logger;

        private readonly Mock<IHttpClient> _httpClient;

        private readonly DealerController _controller;
        public DealerControllerTest()
        {
            ServiceProvider serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            _logger = factory.CreateLogger<DealerController>();

            _httpClient = MockIHttpClientFactory.GetMockHttpClient();
            _controller = new DealerController(_logger, _httpClient.Object);

            MockIHttpClientFactory.SetDeliveryMan();
            MockIHttpClientFactory.SetTimeList();
        }

        [Fact]
        public void DealerController_NotNull()
        {
            Assert.NotNull(new DealerController(_logger, _httpClient.Object));
        }

        [Fact]
        public void DealerController_Index_IsType_ViewResult()
        {
            var result = _controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void DealerController_GetData_IsType_JsonResult()
        {
            var result = await _controller.GetData(DateTime.Now);
            var jsonResult = Assert.IsType<JsonResult>(result);

            Assert.NotNull(jsonResult);
            var properties = jsonResult.Value.GetType().GetProperties().Select(x => new { x.Name, Value = x.GetValue(jsonResult.Value) });
            Assert.Contains(properties, x => x.Name == "Success");
            Assert.True((bool)properties.Where(x => x.Name == "Success").First().Value);
        }
    }
}
