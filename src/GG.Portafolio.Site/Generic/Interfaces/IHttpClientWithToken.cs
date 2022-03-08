using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace GG.Portafolio.Site.Generic.Interfaces
{
    public interface IHttpClientWithToken : ISerialize
    {
        Task<(TResultOk, TResultError, HttpStatusCode)> GetAsync<TResultOk, TResultError>(string urlRelative, HttpContext httpContext, ILogger logger);

        Task<(TResultOk, TResultError, HttpStatusCode)> PostAsync<TResultOk, TResultError>(string urlRelative, object paramPeticion, HttpContext httpContext, ILogger logger);

        Task<(TResultOk, TResultError, HttpStatusCode)> DeleteAsync<TResultOk, TResultError>(string urlRelative, HttpContext httpContext, ILogger logger);

        Task<(TResultOk, TResultError, HttpStatusCode)> PutAsync<TResultOk, TResultError>(string urlRelative, object paramPeticion, HttpContext httpContext, ILogger logger);
    }
}
