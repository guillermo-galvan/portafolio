using GG.Portafolio.Api.Controllers;
using GG.Portafolio.BusinessLogic.User;
using GG.Portafolio.DataBaseBusiness;
using GG.Portafolio.DataBaseBusiness.Business;
using GG.Portafolio.DataBaseBusiness.Sentences;
using GG.Portafolio.Shared.Test;
using GG.Portafolio.Shared.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.CRUD;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GG.Portafolio.Api.Test.Controllers
{
    public class UserControllerTest
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserController _controller;
        private readonly UserManagement _userManagement;

        public UserControllerTest()
        {
            ServiceProvider serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            _logger = factory.CreateLogger<UserController>();

            _userManagement = new(factory.CreateLogger<UserManagement>(),
                new UserBusiness(new UserSentences(new CriteriaBuilder()), factory.CreateLogger<UserBusiness>()),
                new DatabaseOperation()
            );

            MockIDataBaseConnection.GetMockIConnectionDataBase();

            MockIDataBaseConnection.SetUserEntity();

            _controller = new UserController(_logger, _userManagement);
        }

        [Fact]
        public void UserController_NotNull()
        {
            Assert.NotNull(new UserController(_logger, _userManagement));
        }

        [Fact]
        public void DealerController_Validate_IsType_UserResponse()
        {
            var result = _controller.Validate(new UserRequest
            {
                Email = "test@gmail.com",
                Name = "Test",
                Subject = Guid.NewGuid().ToString(),
            });

            var actionResult = Assert.IsType<ActionResult<UserResponse>>(result);
            Assert.IsType<UserResponse>(actionResult.Value);
        }

        [Fact]
        public void DealerController_Validate_IsType_BadRequest()
        {
            _controller.ModelState.AddModelError(nameof(UserRequest.Name), "El campo es requerido");
            var result = _controller.Validate(new UserRequest
            {
                Email = "test@gmail.com",
                Name = "Test",
                Subject = Guid.NewGuid().ToString(),
            });
            _controller.ModelState.Clear();
            var actionResult = Assert.IsType<ActionResult<UserResponse>>(result);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }
    }
}
