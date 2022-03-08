using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GG.Portafolio.BusinessLogic.Test
{
    public class BussinessLogicCollectionExtensionsTest
    {
        [Fact]
        public void BussinessLogicCollectionExtensions_NotTrow()
        {
            try
            {
                ServiceCollection serviceProvider = new();
                serviceProvider.AddBusinessLogic();
                Assert.True(true);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }
    }
}
