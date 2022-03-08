using GG.Portafolio.Api.Controllers;
using GG.Portafolio.BusinessLogic.Dealer;
using GG.Portafolio.Shared.Dealer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GG.Portafolio.Api.Test.Controllers
{
    public class DealerControllerTest
    {
        private readonly ILogger<DealerController> _logger;
        private readonly DealerController _controller;

        public DealerControllerTest()
        {
            ServiceProvider serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            _logger = factory.CreateLogger<DealerController>();

            _controller = new(_logger, new DealerManagement(factory.CreateLogger<DealerManagement>()));
        }

        [Fact]
        public void DealerController_NotNull()
        {
            ServiceProvider serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();

            Assert.NotNull(new DealerController(_logger, new DealerManagement(factory.CreateLogger<DealerManagement>())));
        }

        [Fact]
        public void DealerController_GetTimeList_IsType_TimeList()
        {
            var result = _controller.GetTimeList(DateTime.Now);
            var actionResult = Assert.IsType<ActionResult<TimeList>>(result);
            Assert.IsType<TimeList>(actionResult.Value);
        }

        [Fact]
        public void DealerController_GetTimeList_IsType_BadRequest()
        {
            var result = _controller.GetTimeList(DateTime.MaxValue);
            var actionResult = Assert.IsType<ActionResult<TimeList>>(result);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void DealerController_GetDeliveryMan_IsType_IEnumerable_DeliveryMan()
        {
            var result = _controller.GetDeliveryMan();
            var actionResult = Assert.IsType<ActionResult<IEnumerable<DeliveryMan>>>(result);
            Assert.IsType<DeliveryMan[]>(actionResult.Value);
        }

    }
}
