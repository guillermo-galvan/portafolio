using GG.Portafolio.Shared;
using GG.Portafolio.Shared.TemplateWord;
using GG.Portafolio.Site.Generic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace GG.Portafolio.Site.Controllers
{
    public class TemplateWordController : Controller
    {
        private readonly ILogger<TemplateWordController> _logger;

        private readonly IHttpClient _httpClient;

        public TemplateWordController(ILogger<TemplateWordController> logger, IHttpClient httpClient)
        { 
            _logger = logger;
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new TemplateRequest());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(TemplateRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    (TemplateResponse Ok, ErrorApi Error, HttpStatusCode StatusCode) response =
                        await _httpClient.PostAsync< TemplateResponse, ErrorApi> ("TemplateWord/generatetemplate", model, _logger);

                    if (response.StatusCode == HttpStatusCode.OK) 
                    {
                        return File(response.Ok.File, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", $"{model.Name}.{response.Ok.Extension}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {model}", MethodBase.GetCurrentMethod().Name, model);
            }

            return View(model);
        }

        [HttpGet]        
        public async Task<IActionResult> GetTemplate()
        {
            try
            {
                (TemplateResponse Ok, ErrorApi Error, HttpStatusCode StatusCode) model = await _httpClient.GetAsync<TemplateResponse, ErrorApi>("TemplateWord/downloadtemplate", _logger);

                if (model.StatusCode == HttpStatusCode.OK)
                {
                    return File(model.Ok.File, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", $"template.{model.Ok.Extension}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
            }

            return Problem("Error",statusCode: (int)HttpStatusCode.NotFound);
        }
    }
}
