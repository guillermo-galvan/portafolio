using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GG.Portafolio.DataBaseBusiness.Test
{
    public class DataBaseBusinessServiceExtensionsTest
    {
        [Fact]
        public void DataBaseBusinessServiceExtensions_NotTrow()
        {
            try
            {
                ServiceCollection serviceProvider = new();
                serviceProvider.AddDataBaseBusiness();
                Assert.True(true);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }
    }
}
