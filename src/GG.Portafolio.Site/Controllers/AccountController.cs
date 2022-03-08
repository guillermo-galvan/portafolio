using GG.Portafolio.Shared;
using GG.Portafolio.Site.Generic.Interfaces;
using GG.Portafolio.Site.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace GG.Portafolio.Site.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccessToken _accessToken;
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IAccessToken accessToken, IHttpClientFactory httpClientFactory)
        {
            _accessToken = accessToken;
            _httpClientFactory = httpClientFactory;
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize(Policy.PolicyBasic)]
        public async Task<IActionResult> Index()
        {
            var a = User.Claims.Select(c => new { c.Type, c.Value }).ToArray();
            var b = new { User.Identity.Name, User.Identity.AuthenticationType, User.Identity.IsAuthenticated };
            var c = User.Identities.ToArray();

            ViewBag.Json = JsonSerializer.Serialize(new
            {
                Identity = b,
                Claims = a,
            }, new JsonSerializerOptions { WriteIndented = true });

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            HttpClient client = _httpClientFactory.CreateClient(nameof(ConfigurationValues.SsoUrl));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var jsonElement = await client.GetFromJsonAsync<JsonElement>(_accessToken.DiscoveryDocumentResponse.UserInfoEndpoint);
            ViewBag.Json2 = JsonSerializer.Serialize(jsonElement, new JsonSerializerOptions { WriteIndented = true });

            return View();
        }

        [Authorize(Policy.PolicyBasic)]
        public IActionResult Logout()
        {
            return SignOut(CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
