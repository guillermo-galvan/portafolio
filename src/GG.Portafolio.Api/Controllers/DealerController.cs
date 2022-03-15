using GG.Portafolio.BusinessLogic.Dealer;
using GG.Portafolio.Shared.Dealer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using GG.Portafolio.Shared;

namespace GG.Portafolio.Api.Controllers
{
    [Authorize(Policy.PolicyBasic)]
    [Route("api/[controller]")]
    [ApiController]
    public class DealerController : ControllerBase
    {
        private readonly ILogger<DealerController> _logger;
        private readonly DealerManagement _dealerManagement;

        public DealerController(ILogger<DealerController> logger, DealerManagement dealerManagement)
        {
            _logger = logger;
            _dealerManagement = dealerManagement; 
        }

        [HttpGet("gettimelist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TimeList> GetTimeList(DateTime date)
        {
            try
            {
                return _dealerManagement.Get(date);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {date}", MethodBase.GetCurrentMethod().Name,date);
                return BadRequest();
            }
        }

        [HttpGet("deliveryman")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<DeliveryMan>> GetDeliveryMan()
        {
            try
            {
                return _dealerManagement.GetDeliveryMan().ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
                return BadRequest();
            }
        }
    }
}
