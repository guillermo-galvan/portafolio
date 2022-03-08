using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using GG.Portafolio.Site.Generic.Interfaces;

namespace GG.Portafolio.Site.Generic.Rest
{
    public class HttpClientRestWithToken : HttpClientBase, IHttpClientWithToken
    {
        public HttpClientRestWithToken(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {

        }

        public async Task<(TResultOk, TResultError, HttpStatusCode)> GetAsync<TResultOk, TResultError>(string urlRelative, HttpContext httpContext, ILogger logger)
        {
            try
            {
                var accessToken = await httpContext.GetTokenAsync("access_token");
                return await GetAsync<TResultOk, TResultError>(urlRelative, accessToken, logger);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Method {name} URL: {urlRelative} Type ReturnOk {result} ReturnError {error} ",
                    MethodBase.GetCurrentMethod().Name, urlRelative, typeof(TResultOk), typeof(TResultError));
                throw;
            }
        }

        public async Task<(TResultOk, TResultError, HttpStatusCode)> PostAsync<TResultOk, TResultError>(string urlRelative, object paramPeticion, HttpContext httpContext, ILogger logger)
        {
            try
            {
                var accessToken = await httpContext.GetTokenAsync("access_token");
                return await PostAsync<TResultOk, TResultError>(urlRelative, paramPeticion, accessToken, logger);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Method {name} URL: {urlRelative} Type ReturnOk {result} ReturnError {error} ",
                    MethodBase.GetCurrentMethod().Name, urlRelative, typeof(TResultOk), typeof(TResultError));
                throw;
            }
        }

        public async Task<(TResultOk, TResultError, HttpStatusCode)> DeleteAsync<TResultOk, TResultError>(string urlRelative, HttpContext httpContext, ILogger logger)
        {
            try
            {
                var accessToken = await httpContext.GetTokenAsync("access_token");
                return await DeleteAsync<TResultOk, TResultError>(urlRelative, accessToken, logger);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Method {name} URL: {urlRelative} Type ReturnOk {result} ReturnError {error} ",
                    MethodBase.GetCurrentMethod().Name, urlRelative, typeof(TResultOk), typeof(TResultError));
                throw;
            }
        }

        public async Task<(TResultOk, TResultError, HttpStatusCode)> PutAsync<TResultOk, TResultError>(string urlRelative, object paramPeticion, HttpContext httpContext, ILogger logger)
        {
            try
            {
                var accessToken = await httpContext.GetTokenAsync("access_token");
                return await PutAsync<TResultOk, TResultError>(urlRelative, paramPeticion, accessToken, logger);
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
