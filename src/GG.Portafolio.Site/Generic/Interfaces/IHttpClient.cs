using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace GG.Portafolio.Site.Generic.Interfaces
{
    public interface IHttpClient : ISerialize
    {
        Task<(TResultOk, TResultError, HttpStatusCode)> GetAsync<TResultOk, TResultError>(string urlRelative, ILogger logger);

        Task<(TResultOk, TResultError, HttpStatusCode)> PostAsync<TResultOk, TResultError>(string urlRelative, object paramPeticion, ILogger logger);

        Task<(TResultOk, TResultError, HttpStatusCode)> DeleteAsync<TResultOk, TResultError>(string urlRelative, ILogger logger);
    }
}
