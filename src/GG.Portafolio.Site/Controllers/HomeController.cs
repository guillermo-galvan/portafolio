using GG.Portafolio.Shared.Blog;
using GG.Portafolio.Site.Generic.Interfaces;
using GG.Portafolio.Site.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using GG.Portafolio.Shared;

namespace GG.Portafolio.Site.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, IHttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        private async Task<List<List<BlogResponse>>> GetBlogs()
        {
            List<List<BlogResponse>> blogs = new();

            try
            {
                (IEnumerable<BlogResponse> Ok, ErrorApi ErrorApi, HttpStatusCode Status) =
                 await _httpClient.GetAsync<IEnumerable<BlogResponse>, ErrorApi>("Blog/get", _logger);

                if (Status == HttpStatusCode.OK)
                {
                    blogs.AddRange(Ok.OrderByDescending(x => x.CreateDate).Select((x, i) => new { Index = i, Value = x })
                      .GroupBy(x => x.Index / 2)
                      .Select(x => x.Select(v => v.Value).ToList()).ToList());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
            }

            return blogs;
        }

        public async Task<IActionResult> Index()
        {
            DefaultViews defaultViews = TempData.ContainsKey("DefaultViews") ? (DefaultViews)TempData["DefaultViews"] : DefaultViews.Home;
            if (defaultViews == DefaultViews.Blog)
            { 
                ViewBag.Blogs = await GetBlogs();
            }
            ViewBag.Home = true;

            return View(defaultViews);
        }

        public IActionResult RedirectMenu(string url)
        {
            DefaultViews defaultViews = url switch
            {
                "blog" => DefaultViews.Blog,
                "aboutme" => DefaultViews.AboutMe,
                "portfolio" => DefaultViews.Portfolio,
                _ => DefaultViews.Home,
            };

            TempData["DefaultViews"] = defaultViews;

            return RedirectToAction("Index");
        }

        public PartialViewResult Home()
        {
            return PartialView();
        }

        public PartialViewResult AboutMe() 
        {
            return PartialView();
        }

        public PartialViewResult Portfolio()
        {
            return PartialView();
        }

        public async Task<PartialViewResult> Blogs()
        {
            return PartialView(await GetBlogs());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
