using GG.Portafolio.BusinessLogic.User;
using GG.Portafolio.Shared;
using GG.Portafolio.Shared.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace GG.Portafolio.Api.Controllers
{
    [Authorize(Policy.PolicyBasic)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManagement _userManagement;

        public UserController(ILogger<UserController> logger, UserManagement userManagement)
        {
            _logger = logger;
            _userManagement = userManagement;
        }

        [HttpGet("validate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserResponse> Validate([FromQuery] UserRequest user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return new UserResponse() { Success = _userManagement.Validate(user) };
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {user}", MethodBase.GetCurrentMethod().Name, user);
                return BadRequest();
            }
        }
    }
}
