using GG.Portafolio.Shared;
using GG.Portafolio.Shared.Blog;
using GG.Portafolio.Site.Generic.Interfaces;
using GG.Portafolio.Site.Models;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GG.Portafolio.Site.Controllers
{
    [Authorize(Policy.PolicyBasic)]
    [Authorize(Policy.PolicyAdmin)]
    public class BlogManagementController : Controller
    {
        private const string _nameFileImage = "imgtmp";

        private readonly ILogger<BlogManagementController> _logger;
        private readonly IHttpClientWithToken _httpClient;
        private readonly ConfigurationValues _configurationValues;
        private readonly IWebHostEnvironment _environment;

        public BlogManagementController(ILogger<BlogManagementController> logger, IHttpClientWithToken httpClient, IOptions<ConfigurationValues> options,
            IWebHostEnvironment environment)
        {
            _logger = logger;
            _httpClient = httpClient;
            _configurationValues = options.Value;
            _environment = environment;
        }

        private async Task<T> GetBlogManagementById<T>(string blog, bool raiseException = true)
        {
            (T Ok, _, HttpStatusCode CodeStatus) =
                await _httpClient.GetAsync<T, ErrorApi>($"Blog/get/{blog}", HttpContext, _logger);

            if (CodeStatus == HttpStatusCode.OK)
            {
                return Ok;
            }
            else if (raiseException)
            {
                throw new Exception($"No se pudo recuperar el blog {blog}");
            }
            else
            {
                return default;
            }
        }

        public async Task<IActionResult> Index()
        {
            List<BlogResponse> blogs = new();

            try
            {
                (IEnumerable<BlogResponse> Ok, ErrorApi ErrorApi, HttpStatusCode Status) =
                    await _httpClient.GetAsync<IEnumerable<BlogResponse>, ErrorApi>("Blog/get", HttpContext, _logger);

                if (Status == HttpStatusCode.OK)
                {
                    blogs.AddRange(Ok);
                }

                if (TempData["DeleteBlogBackup"] != null)
                {
                    ViewBag.DeleteBlogBackup = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
            }

            return View(blogs);
        }

        public async Task<IActionResult> NewEdit(string blog)
        {
            BlogManagementModel model = null;
            try
            {
                ViewData["Operation"] = string.IsNullOrWhiteSpace(blog) ? "Nuevo" : "Editar";
                ViewData["TinymceKey"] = _configurationValues.TinymceKey;

                model = string.IsNullOrWhiteSpace(blog) ? new BlogManagementModel { CreateDate = DateTime.Now.Ticks } :
                    await GetBlogManagementById<BlogManagementModel>(blog);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {blog}", MethodBase.GetCurrentMethod().Name, blog);
                ViewBag.ListError = new List<string> { "No se pudo recuperar el blog." };
            }

            return View("~/Views/BlogManagement/NewEdit.cshtml", model ?? new BlogManagementModel());
        }

        [HttpPost]
        public JsonResult SaveImage(ImgageLoad model)
        {
            ImageLoadResponse response = new() { Success = false };
            try
            {
                if (ModelState.IsValid)
                {
                    string subject = HttpContext.User.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject || x.Type == ClaimTypes.NameIdentifier)?.Value;
                    string pathDirectory = Path.Combine(_environment.WebRootPath, _nameFileImage, subject);
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(model.FileName)}";
                    string pathFile = Path.Combine(pathDirectory, fileName);

                    if (!Directory.Exists(pathDirectory))
                    {
                        Directory.CreateDirectory(pathDirectory);
                    }

                    if (!System.IO.File.Exists(pathFile))
                    {
                        System.IO.File.WriteAllBytes(pathFile, model.FileContent);
                    }

                    response.Success = true;
                    response.FileName = fileName;
                    response.Url = $"/{_nameFileImage}/{subject}/{fileName}";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
            }

            return Json(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(BlogManagementModel model)
        {
            try
            {
                ViewData["Operation"] = string.IsNullOrWhiteSpace(model.Id) ? "Nuevo" : "Editar";
                ViewData["TinymceKey"] = _configurationValues.TinymceKey;

                if (ModelState.IsValid)
                {
                    string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject || x.Type == ClaimTypes.NameIdentifier)?.Value;
                    string pathfiles = Path.Combine(_environment.WebRootPath, _nameFileImage, userId);
                    BlogContentReponse Ok = await GetBlogManagementById<BlogContentReponse>(string.IsNullOrWhiteSpace(model.Id) ? "0" : model.Id, false);

                    BlogNewRequest newRequest = Ok == null ?
                        new BlogNewRequest()
                        {
                            Title = model.Title,
                            Content = model.Content,
                            Dsc = model.Dsc,
                            UserId = userId,
                        } :
                        new BlogEditRequest()
                        {
                            Title = model.Title,
                            Content = model.Content,
                            Dsc = model.Dsc,
                            UserId = userId,
                            Id = model.Id,
                        };

                    List<string> files = model.Images?.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                    files?.ForEach(x =>
                    {
                        string[] fileStruct = x.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                        string filePath = Path.Combine(pathfiles, fileStruct[^1]);

                        newRequest.ContentFiles.Add(new ContentFile
                        {
                            File = System.IO.File.ReadAllBytes(filePath),
                            Name = fileStruct[^1],
                            Url = x
                        });
                    });

                    (BlogOperationResponse Response, ErrorApi Error1, HttpStatusCode Status1) = Ok == null ?
                        await _httpClient.PostAsync<BlogOperationResponse, ErrorApi>("Blog/create", newRequest, HttpContext, _logger) :
                        await _httpClient.PutAsync<BlogOperationResponse, ErrorApi>("Blog/modify", newRequest, HttpContext, _logger);

                    if (Status1 == HttpStatusCode.OK)
                    {
                        if (Directory.Exists(pathfiles))
                        {
                            Directory.Delete(pathfiles, true);
                        }
                        TempData["DeleteBlogBackup"] = true;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.ListError = Response.Errores;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
                ViewBag.ListError = new List<string> { "Error al guardar el blog." };
            }

            return View("~/Views/BlogManagement/NewEdit.cshtml", model);
        }

        public async Task<IActionResult> Delete(string blog)
        {
            try
            {
                _ = await _httpClient.DeleteAsync<string, ErrorApi>($"Blog/delete/{blog}", HttpContext, _logger);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
            }

            return RedirectToAction("Index");
        }
    }
}
