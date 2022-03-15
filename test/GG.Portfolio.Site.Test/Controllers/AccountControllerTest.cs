using GG.Portafolio.Site.Controllers;
using GG.Portafolio.Site.Generic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net.Http;
using Xunit;

namespace GG.Portafolio.Site.Test.Controllers
{
    public class AccountControllerTest
    {
        private readonly AccountController _controller;
        private readonly Mock<IAccessToken> _mockIAccessToken;
        private readonly Mock<IHttpClientFactory> _mockIHttpClientFactory;
        public AccountControllerTest()
        { 
            _mockIAccessToken = MockIAccessToken.GetMockIAccessToken();
            _mockIHttpClientFactory = MockIHttpClientFactory.GetHttpClientFactory(_mockIAccessToken.Object.DiscoveryDocumentResponse.UserInfoEndpoint);
            _controller = new(_mockIAccessToken.Object, _mockIHttpClientFactory.Object)
            {
                TempData = Utilities.GetTempDataDictionary()
            };
        }

        [Fact]
        public void AccountController_NotNull()
        {
            Assert.NotNull(new AccountController(_mockIAccessToken.Object, _mockIHttpClientFactory.Object) { TempData = Utilities.GetTempDataDictionary() });
        }

        [Fact]
        public void AccountController_AccessDenied_IsType_ViewResult()
        {
            var result = _controller.AccessDenied();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void AccountController_Index_IsType_ViewResult()
        {   
            _controller.ControllerContext.HttpContext = new DefaultHttpContext {
                User = Utilities.GetClaimsPrincipal(),
                RequestServices = Utilities.GetServiceProvider(),
            };

            var result = await _controller.Index();
            _controller.ControllerContext.HttpContext = null;            
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AccountController_Logout_IsType_SignInResult()
        {
            var result = _controller.Logout();            
            Assert.IsType<SignOutResult>(result);
        }
    }
}
