using GG.Portafolio.Shared;
using GG.Portafolio.Shared.Blog;
using GG.Portafolio.Site.Generic.Interfaces;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GG.Portafolio.Site.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;
        private readonly IHttpClient _httpClient;

        public BlogController(ILogger<BlogController> logger, IHttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        [Route("blog/{title}")]
        public async Task<IActionResult> Index(string title)
        {
            BlogContentWithCommentsReponse model = null;

            try
            {
                (BlogContentWithCommentsReponse Ok, _, HttpStatusCode Status) =
                    await _httpClient.GetAsync<BlogContentWithCommentsReponse, ErrorApi>($"Blog/getbytitle/{title}", _logger);

                if (Status == HttpStatusCode.OK)
                {
                    model = Ok;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
            }

            return View(model ?? new BlogContentWithCommentsReponse());
        }


        [Route("savecomment")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<JsonResult> SaveComment(BlogComments comments)
        {
            bool result = false;

            try
            {
                if (ModelState.IsValid)
                {
                    string userId = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject || x.Type == ClaimTypes.NameIdentifier)?.Value;
                    comments.Date = DateTime.Now;
                    comments.User_Id = userId;
                    (_, _, HttpStatusCode StatusCode) = await _httpClient.PostAsync<string, ErrorApi>("Blog/commentsave", comments, _logger);
                    result = StatusCode == HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "{Name}  BlogId:{BlogId} Date:{Date} Name:{Name} Content:{Content}",
                   MethodBase.GetCurrentMethod().Name, comments.BlogId, comments.Date, comments.Name, comments.Content);
            }

            return Json(new
            {
                success = result,
                comments.Name,
                Comment = comments.Content,
                Date = comments.Date.ToString("dd/MM/yyyy HH:mm:ss")
            });
        }
    }
}
