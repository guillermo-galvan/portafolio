using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GG.Portafolio.Site.Test
{
    internal static class Utilities
    {
        internal static ITempDataDictionary GetTempDataDictionary()
        {
            ITempDataProvider tempDataProvider = Mock.Of<ITempDataProvider>();
            TempDataDictionaryFactory tempDataDictionaryFactory = new(tempDataProvider);
            return tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());
        }

        internal static IServiceProvider GetServiceProvider()
        {
            var authResult = AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(), null));
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            var serviceProvider = new Mock<IServiceProvider>();

            authResult.Properties.StoreTokens(new[]
            {
                new AuthenticationToken { Name = "access_token", Value = "accessTokenValue" }
            });

            authenticationServiceMock
                .Setup(x => x.AuthenticateAsync(It.IsAny<HttpContext>(), null))
                .ReturnsAsync(authResult);

            serviceProvider.Setup(_ => _.GetService(typeof(IAuthenticationService))).Returns(authenticationServiceMock.Object);

            return serviceProvider.Object;
        }

        internal static ClaimsPrincipal GetClaimsPrincipal()
        { 
            return new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString()),
            }, "mock"));
        }

        internal static Mock<IWebHostEnvironment> GetMockIWebHostEnvironment()
        {
            Mock<IWebHostEnvironment> mockIWebHostEnvironment = new();
            mockIWebHostEnvironment.Setup(m => m.ContentRootPath).Returns(AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
            mockIWebHostEnvironment.Setup(m => m.WebRootPath).Returns(Path.GetTempPath());
            return mockIWebHostEnvironment;
        }
    }
}
