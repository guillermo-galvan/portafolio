using GG.Portafolio.Shared;
using GG.Portafolio.Shared.Dealer;
using GG.Portafolio.Site.Generic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace GG.Portafolio.Site.Controllers
{
    public class DealerController : Controller
    {
        private readonly ILogger<DealerController> _logger;

        private readonly IHttpClient _httpClient;

        public DealerController(ILogger<DealerController> logger, IHttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetData([FromQuery]DateTime date)
        {
            try
            {
                var dateparse = HttpUtility.ParseQueryString(date.ToString("MM/dd/yyyy HH:mm:ss"));                
                var timeHttp = _httpClient.GetAsync<TimeList, ErrorApi>($"Dealer/gettimelist?date={dateparse}", _logger);
                var dealerHttp = _httpClient.GetAsync<List<DeliveryMan>, ErrorApi>("Dealer/deliveryman", _logger);

                (TimeList Ok, ErrorApi Error, HttpStatusCode StatusCode) timeList =  await timeHttp;
                (List<DeliveryMan> Ok, ErrorApi Error, HttpStatusCode StatusCode) deliveryMans = await dealerHttp;

                if (timeList.StatusCode == HttpStatusCode.OK && deliveryMans.StatusCode == HttpStatusCode.OK)
                {
                    return Json(new { Success = true, deliveries = deliveryMans.Ok, dates = timeList.Ok });
                }
                else
                {
                    return Json(new { Success = false });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {date}", MethodBase.GetCurrentMethod().Name, date);
                return Json(new { Success = false });
            }
        }
    }
}
