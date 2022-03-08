using GG.Portafolio.BusinessLogic.Blog;
using GG.Portafolio.Shared;
using GG.Portafolio.Shared.Blog;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;

namespace GG.Portafolio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy.PolicyBasic)]
    public class BlogController : ControllerBase
    {
        private readonly ILogger<BlogController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly BlogManagement _blogManagement;

        public BlogController(ILogger<BlogController> logger, IWebHostEnvironment environment,
            BlogManagement blogManagement)
        {
            _logger = logger;
            _environment = environment;
            _blogManagement = blogManagement;
        }

        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<BlogResponse>> Get()
        {
            try
            {
                return _blogManagement.GetBlogs().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name}", MethodBase.GetCurrentMethod().Name);
                return NotFound();
            }
        }

        [Authorize(Policy.PolicyAdmin)]
        [HttpGet("get/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<BlogContentReponse> GetById(string id)
        {
            try
            {
                var result = _blogManagement.GetBlogById(id);
                return result == null ? NotFound() : result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {id}", MethodBase.GetCurrentMethod().Name, id);
                return NotFound();
            }
        }

        [HttpGet("getbytitle/{title}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<BlogContentWithCommentsReponse> Get(string title)
        {
            try
            {
                var result = _blogManagement.GetBlogs(title);
                return result == null ? NotFound() : result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {title}", MethodBase.GetCurrentMethod().Name, title);
                return NotFound();
            }
        }

        [Authorize(Policy.PolicyAdmin)]
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<BlogOperationResponse> CreateBlog(BlogNewRequest model)
        {
            try
            {
                string userId = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject || x.Type == ClaimTypes.NameIdentifier)?.Value;

                if (ModelState.IsValid && model.UserId == userId)
                {
                    BlogOperationResponse response =
                    _blogManagement.CreateNewBlog(model, _environment.WebRootPath, $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}");
                    return response.Errores.Any() ? Conflict() : response;
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {model}.", MethodBase.GetCurrentMethod().Name, model);
                return BadRequest();
            }
        }

        [Authorize(Policy.PolicyAdmin)]
        [HttpPut("modify")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<BlogOperationResponse> EditBlog(BlogEditRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BlogOperationResponse response =
                    _blogManagement.EditBlog(model, _environment.WebRootPath, $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}");

                    return response.Errores.Any() ? Conflict() : response;
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {model}.", MethodBase.GetCurrentMethod().Name, model);
                return BadRequest();
            }
        }

        [Authorize(Policy.PolicyAdmin)]
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult BlogDelete(string id)
        {
            try
            {
                return _blogManagement.BlogDelete(id) ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Name} {id}", MethodBase.GetCurrentMethod().Name, id);
                return BadRequest();
            }
        }

        [HttpPost("commentsave")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CommentSave(BlogComments comment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _blogManagement.CommentSave(comment);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar comentario BlogId:{BlogId} Name: {Name} Date: {Date}  Content:{Content}.", comment.BlogId, comment.Name, comment.Date, comment.Content);
                return Problem("Ocurrio un error.", statusCode: (int)HttpStatusCode.BadRequest);
            }
        }
    }
}
