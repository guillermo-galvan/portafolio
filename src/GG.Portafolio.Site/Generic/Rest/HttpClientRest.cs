using GG.Portafolio.Site.Generic.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using System.Reflection;
using System;
using System.Net;

namespace GG.Portafolio.Site.Generic.Rest
{
    public class HttpClientRest : HttpClientBase, IHttpClient
    {
        private readonly IAccessToken _accessToken;

        public HttpClientRest(IHttpClientFactory httpClientFactory, IAccessToken accessToken) : base(httpClientFactory)
        {
            _accessToken = accessToken;
        }

        public async Task<(TResultOk, TResultError, HttpStatusCode)> GetAsync<TResultOk, TResultError>(string urlRelative, ILogger logger)
        {
            try
            {
                _accessToken.VerifyAutentificacion(logger);                
                return await GetAsync<TResultOk, TResultError>(urlRelative, _accessToken.TokenInfo.TokenResponse.AccessToken,logger);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Method {name} URL: {urlRelative} Type ReturnOk {result} ReturnError {error} ",
                    MethodBase.GetCurrentMethod().Name,urlRelative, typeof(TResultOk), typeof(TResultError));
                throw;
            }
        }

        public async Task<(TResultOk, TResultError, HttpStatusCode)> PostAsync<TResultOk, TResultError>(string urlRelative, object paramPeticion, ILogger logger)
        {
            try
            {
                _accessToken.VerifyAutentificacion(logger);
                return await PostAsync<TResultOk, TResultError>(urlRelative, paramPeticion, _accessToken.TokenInfo.TokenResponse.AccessToken, logger);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Method {name} URL: {urlRelative} Type ReturnOk {result} ReturnError {error} ",
                    MethodBase.GetCurrentMethod().Name, urlRelative, typeof(TResultOk), typeof(TResultError));
                throw;
            }
        }

        public async Task<(TResultOk, TResultError, HttpStatusCode)> DeleteAsync<TResultOk, TResultError>(string urlRelative, ILogger logger)
        {
            try
            {
                _accessToken.VerifyAutentificacion(logger);
                return await DeleteAsync<TResultOk, TResultError>(urlRelative, _accessToken.TokenInfo.TokenResponse.AccessToken, logger);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Method {name} URL: {urlRelative} Type ReturnOk {result} ReturnError {error} ",
                    MethodBase.GetCurrentMethod().Name, urlRelative, typeof(TResultOk), typeof(TResultError));
                throw;
            }
        }
    }
}
