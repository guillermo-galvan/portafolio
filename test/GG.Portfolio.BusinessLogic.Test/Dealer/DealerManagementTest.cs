using GG.Portafolio.BusinessLogic.Dealer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GG.Portafolio.BusinessLogic.Test.Dealer
{
    public class DealerManagementTest
    {
        private readonly ILogger<DealerManagement> _logger;
        private readonly DealerManagement _dealerManagement;

        public DealerManagementTest()
        {
            ServiceProvider serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            _logger = factory.CreateLogger<DealerManagement>();
            _dealerManagement = new(_logger);
        }

        [Fact]
        public void DealerManagement_NotNull()
        {
            Assert.NotNull(new DealerManagement(_logger));
        }

        [Fact]
        public void DealerManagement_Get_NotNull()
        {
            Assert.NotNull(_dealerManagement.Get(DateTime.Now));
        }

        [Fact]
        public void DealerManagement_Get_True()
        {
            var result = _dealerManagement.Get(DateTime.Now);
            Assert.True(result.TimeCalculateds.Count == 23);
            Assert.True(result.MaxId == 23);
            Assert.True(result.BaseDay == result.TimeCalculateds.Last().Time);
        }

        [Fact]
        public void DealerManagement_GetDeliveryMan_NotNull()
        {
            Assert.NotNull(_dealerManagement.GetDeliveryMan());
        }

        [Fact]
        public void DealerManagement_GetDeliveryMan_NotEmpty()
        {
            Assert.NotEmpty(_dealerManagement.GetDeliveryMan());
        }

        [Fact]
        public void DealerManagement_GetDeliveryMan_True()
        {
            var resutl = _dealerManagement.GetDeliveryMan();
            Assert.True(resutl.Count() == 9);
        }
    }
}
