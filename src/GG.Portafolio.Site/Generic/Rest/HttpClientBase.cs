using GG.Portafolio.Shared;
using GG.Portafolio.Site.Generic.Interfaces;
using GG.Portafolio.Site.Models;
using GG.Portafolio.Site.Generic.Convert;
using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GG.Portafolio.Site.Generic.Rest
{
    public abstract class HttpClientBase : ISerialize
    {
        protected readonly IHttpClientFactory _httpClientFactory;

        public HttpClientBase(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private JsonSerializerOptions GetJsonSerializerOptions()
        {
            JsonSerializerOptions options = new()
            {
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true,
                IgnoreNullValues = true
            };
            options.Converters.Add(new ConvertDateTime());
            options.Converters.Add(new ConvertBytes());

            return options;
        }

        public string Serialize<T>(T value)
        {
            return JsonSerializer.Serialize(value);
        }

        public T Deserialize<T>(string value)
        {
            return JsonSerializer.Deserialize<T>(value, GetJsonSerializerOptions());
        }

        private async Task<(TResultOk, TResultError, HttpStatusCode)> GetTypeResponse<TResultOk, TResultError>(HttpResponseMessage httpResponseMessage)
        {
            TResultError error = default;
            TResultOk ok = default;

            string content = await httpResponseMessage.Content.ReadAsStringAsync();

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                ok = typeof(string) == typeof(TResultOk) && string.IsNullOrEmpty(content) ? default : Deserialize<TResultOk>(content);
            }
            else if (string.IsNullOrWhiteSpace(content) && typeof(TResultError) == typeof(ErrorApi))
            {
                var constructorInfos = typeof(TResultError).GetConstructors().FirstOrDefault(x => x.GetParameters().Length == 0);

                if (constructorInfos != null)
                {
                    error = (TResultError)constructorInfos?.Invoke(null);

                    error.GetType().GetProperty(nameof(ErrorApi.Status))?.SetValue(error, (int)httpResponseMessage.StatusCode, null);
                }
            }
            else
            {
                error = Deserialize<TResultError>(content);
            }

            return (ok, error, httpResponseMessage.IsSuccessStatusCode ? HttpStatusCode.OK : httpResponseMessage.StatusCode);
        }

        public async Task<(TResultOk, TResultError, HttpStatusCode)> GetAsync<TResultOk, TResultError>(string urlRelative, string accessToken, ILogger logger)
        {
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient(nameof(ConfigurationValues.BackEndURL));
                httpClient.SetBearerToken(accessToken);
                HttpResponseMessage response = await httpClient.GetAsync(urlRelative);
                return await GetTypeResponse<TResultOk, TResultError>(response);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Method {name} URL: {urlRelative} Type ReturnOk {result} ReturnError {error} ",
                    MethodBase.GetCurrentMethod().Name, urlRelative, typeof(TResultOk), typeof(TResultError));
                throw;
            }
        }

        public async Task<(TResultOk, TResultError, HttpStatusCode)> PostAsync<TResultOk, TResultError>(string urlRelative, object paramPeticion, string accessToken, ILogger logger)
        {
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient(nameof(ConfigurationValues.BackEndURL));
                httpClient.SetBearerToken(accessToken);
                string json = Serialize(paramPeticion);
                HttpResponseMessage response = await httpClient.PostAsync(urlRelative, new StringContent(json, Encoding.UTF8, "application/json"));
                return await GetTypeResponse<TResultOk, TResultError>(response);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Method {name} URL: {urlRelative} Type ReturnOk {result} ReturnError {error} ",
                    MethodBase.GetCurrentMethod().Name, urlRelative, typeof(TResultOk), typeof(TResultError));
                throw;
            }
        }

        public async Task<(TResultOk, TResultError, HttpStatusCode)> DeleteAsync<TResultOk, TResultError>(string urlRelative, string accessToken, ILogger logger)
        {
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient(nameof(ConfigurationValues.BackEndURL));
                httpClient.SetBearerToken(accessToken);
                HttpResponseMessage response = await httpClient.DeleteAsync(urlRelative);
                return await GetTypeResponse<TResultOk, TResultError>(response);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Method {name} URL: {urlRelative} Type ReturnOk {result} ReturnError {error} ",
                    MethodBase.GetCurrentMethod().Name, urlRelative, typeof(TResultOk), typeof(TResultError));
                throw;
            }
        }

        public async Task<(TResultOk, TResultError, HttpStatusCode)> PutAsync<TResultOk, TResultError>(string urlRelative, object paramPeticion, string accessToken, ILogger logger)
        {
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient(nameof(ConfigurationValues.BackEndURL));
                httpClient.SetBearerToken(accessToken);
                string json = Serialize(paramPeticion);
                HttpResponseMessage response = await httpClient.PutAsync(urlRelative, new StringContent(json, Encoding.UTF8, "application/json"));
                return await GetTypeResponse<TResultOk, TResultError>(response);
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
