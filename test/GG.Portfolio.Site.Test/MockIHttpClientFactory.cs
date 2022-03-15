using GG.Portafolio.Shared;
using GG.Portafolio.Shared.Blog;
using GG.Portafolio.Site.Generic.Interfaces;
using GG.Portafolio.Site.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using GG.Portafolio.Shared.Dealer;
using GG.Portafolio.Shared.TemplateWord;

namespace GG.Portafolio.Site.Test
{
    internal static class MockIHttpClientFactory
    {
        private static readonly object _objectLock = new();
        private static Mock<IHttpClient> _mockIHttpClient;
        private static Mock<IHttpClientWithToken> _mockIHttpClientWithToken;

        private static string[] GetSplitUrl(string url)
        {
            return url.Split('/', StringSplitOptions.RemoveEmptyEntries);
        }

        private static (BlogManagementModel, ErrorApi, HttpStatusCode) GetBlogContent(string url, HttpContext httpContext, ILogger logger)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest;
            BlogManagementModel result = null;
            var splitUrl = GetSplitUrl(url);
            ErrorApi error = null;

            if (url.Contains("Blog/get/"))
            {
                if (splitUrl.Length < 3 || string.IsNullOrEmpty(splitUrl[2]))
                {
                    error = new ErrorApi
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                    };
                }
                else
                {
                    result = new BlogManagementModel
                    {
                        Content = "Content",
                        CreateDate = DateTime.Now.Ticks,
                        Dsc = "Dsc",
                        EditDate = DateTime.Now.Ticks,
                        Id = Guid.NewGuid().ToString(),
                        Title = splitUrl[2]
                    };
                    httpStatusCode = HttpStatusCode.OK;
                }
            }

            return (result, error, httpStatusCode);
        }

        public static Mock<IHttpClientFactory> GetHttpClientFactory(string Url)
        {
            Mock<IHttpClientFactory> mock = new();
            NetworkHandler handler = null;
            string path = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Documents");            

            mock.Setup(m => m.CreateClient(It.IsAny<string>())).Returns<string>((m) => 
            {   
                if (m == nameof(ConfigurationValues.BackEndURL))
                {
                    
                }
                else if (m == nameof(ConfigurationValues.SsoUrl))
                {
                    switch (Url)
                    {
                        case "https://demo.identityserver.io/connect/userinfo":
                            handler = new(File.ReadAllText(Path.Combine(path, "success_userinfo_response.json")), HttpStatusCode.OK);
                            break;

                        default:
                            break;
                    }
                }

                return new HttpClient(handler);
            });

            return mock;
        }

        public static Mock<IHttpClient> GetMockHttpClient()
        {
            lock(_objectLock)
            {
                if (_mockIHttpClient == null)
                {
                    _mockIHttpClient = new();
                }
            }
            
            return _mockIHttpClient;
        }

        public static void SetBlogContentWithCommentsReponse()
        {
            _mockIHttpClient.Setup(m => m.GetAsync<BlogContentWithCommentsReponse, ErrorApi>(It.IsAny<string>(),It.IsAny<ILogger>()))
                            .Returns<string, ILogger>((url,logger) => {
                                           BlogContentWithCommentsReponse reponse = null;
                                           ErrorApi error = null;
                                           HttpStatusCode httpStatusCode = HttpStatusCode.OK;

                                           var splitUrl = GetSplitUrl(url);

                                           if (url.Contains("Blog/getbytitle/"))
                                           {
                                               if ( splitUrl.Length < 3 || string.IsNullOrEmpty(splitUrl[2]))
                                               {
                                                   error = new ErrorApi 
                                                   {
                                                       Status = (int)HttpStatusCode.BadRequest,
                                                   };

                                                   httpStatusCode = HttpStatusCode.BadRequest;
                                               }
                                               else
                                               {
                                                   reponse = new BlogContentWithCommentsReponse 
                                                   {
                                                       Comments = new List<BlogComments>(),
                                                       Content = "Content",
                                                       Title = splitUrl[2],
                                                       CreateDate = DateTime.Now.Ticks,
                                                       Dsc = "Dsc",
                                                       EditDate = DateTime.Now.Ticks,
                                                       Id = Guid.NewGuid().ToString(),
                                                   };
                                               }
                                           }
                                           

                                           return Task.FromResult((reponse, error, httpStatusCode));
                                       });

            _mockIHttpClient.Setup(m => m.PostAsync<string, ErrorApi>(It.IsAny<string>(), It.IsAny<BlogComments>(), It.IsAny<ILogger>()))
                            .Returns<string, object, ILogger > ((url, data, logger) => {
                                return Task.FromResult((string.Empty, (ErrorApi)null, HttpStatusCode.OK));
                            });
        }

        public static void SetTimeList()
        {
            _mockIHttpClient.Setup(m => m.GetAsync<TimeList, ErrorApi>(It.IsAny<string>(), It.IsAny<ILogger>()))
                            .Returns<string, ILogger>((url, logger) => {

                                List<TimeCalculated> list = new() 
                                {
                                    new TimeCalculated(1,DateTime.Now, 1,1),
                                    new TimeCalculated(2, DateTime.Now, 2, 2),
                                    new TimeCalculated(3, DateTime.Now, 3, 3),
                                };

                                return Task.FromResult((new TimeList(DateTime.Now, 3, list), (ErrorApi)null, HttpStatusCode.OK));
                            });
        }

        public static void SetDeliveryMan()
        {
            _mockIHttpClient.Setup(m => m.GetAsync<List<DeliveryMan>, ErrorApi>(It.IsAny<string>(), It.IsAny<ILogger>()))
                            .Returns<string, ILogger>((url, logger) => {

                                List<DeliveryMan> list = new()
                                {
                                    new DeliveryMan(1, "1", Array.Empty<DateTime>()),
                                    new DeliveryMan(2, "2", Array.Empty<DateTime>()),
                                    new DeliveryMan(3, "3", Array.Empty<DateTime>()),
                                };

                                return Task.FromResult((list, (ErrorApi)null, HttpStatusCode.OK));
                            });
        }

        public static void SetIEnumerableBlogResponse()
        {
            _mockIHttpClient.Setup(m => m.GetAsync<IEnumerable<BlogResponse>, ErrorApi>(It.IsAny<string>(), It.IsAny<ILogger>()))
                            .Returns<string, ILogger>((url, logger) => {

                                List<BlogResponse> list = new()
                                {
                                    new BlogResponse { CreateDate = DateTime.Now.Ticks, Dsc = "Dsc", Id = Guid.NewGuid().ToString(), Title = "Title 1" },
                                    new BlogResponse { CreateDate = DateTime.Now.Ticks, Dsc = "Dsc", Id = Guid.NewGuid().ToString(), Title = "Title 2" },
                                    new BlogResponse { CreateDate = DateTime.Now.Ticks, Dsc = "Dsc", Id = Guid.NewGuid().ToString(), Title = "Title 3" },
                                    new BlogResponse { CreateDate = DateTime.Now.Ticks, Dsc = "Dsc", Id = Guid.NewGuid().ToString(), Title = "Title 4" },
                                    new BlogResponse { CreateDate = DateTime.Now.Ticks, Dsc = "Dsc", Id = Guid.NewGuid().ToString(), Title = "Title 5" },
                                };

                                return Task.FromResult((list.AsEnumerable(), (ErrorApi)null, HttpStatusCode.OK));
                            });
        }

        public static void SetTemplateResponse()
        {
            _mockIHttpClient.Setup(m => m.PostAsync<TemplateResponse, ErrorApi>(It.IsAny<string>(),It.IsAny<TemplateRequest>(), It.IsAny<ILogger>()))
                            .Returns<string, TemplateRequest, ILogger>((url, model,logger) => {

                                string path = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Documents");

                                return Task.FromResult((new TemplateResponse() 
                                { 
                                    Extension = ".txt", 
                                    File = File.ReadAllBytes(Path.Combine(path, "success_userinfo_response.json")) 
                                }, (ErrorApi)null, HttpStatusCode.OK));
                            });

            _mockIHttpClient.Setup(m => m.GetAsync<TemplateResponse, ErrorApi>(It.IsAny<string>(), It.IsAny<ILogger>()))
                            .Returns<string, ILogger>((url, logger) => {

                                string path = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Documents");

                                return Task.FromResult((new TemplateResponse()
                                {
                                    Extension = ".txt",
                                    File = File.ReadAllBytes(Path.Combine(path, "success_userinfo_response.json"))
                                }, (ErrorApi)null, HttpStatusCode.OK));
                            });
        }

        public static Mock<IHttpClientWithToken> GetMockIHttpClientWithToken()
        {
            lock (_objectLock)
            {
                if (_mockIHttpClientWithToken == null)
                { 
                    _mockIHttpClientWithToken = new Mock<IHttpClientWithToken>();
                }
            }

            return _mockIHttpClientWithToken;
        }

        public static void SetBlogResponse()
        {
            _mockIHttpClientWithToken.Setup(m => m.GetAsync<IEnumerable<BlogResponse>, ErrorApi>(It.IsAny<string>(), It.IsAny<HttpContext>(), It.IsAny<ILogger>())).
                Returns<string, HttpContext, ILogger>((url, httpContext, logger) => {
                    List<BlogResponse> result = new();
                    HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest;
                    if (httpContext != null)
                    {
                        result.Add(new BlogResponse {
                            CreateDate = DateTime.Now.Ticks,
                            Dsc = "Dsc",
                            Id = Guid.NewGuid().ToString(),
                            Title = "Title"
                        });
                        result.Add(new BlogResponse
                        {
                            CreateDate = DateTime.Now.Ticks,
                            Dsc = "Dsc",
                            Id = Guid.NewGuid().ToString(),
                            Title = "Title"
                        });
                        httpStatusCode = HttpStatusCode.OK;
                    }

                    return Task.FromResult((result.AsEnumerable(), (ErrorApi)null, httpStatusCode));
                });
        }

        public static void SetBlogManagementModel()
        {
            _mockIHttpClientWithToken.Setup(m => m.GetAsync<BlogManagementModel, ErrorApi>(It.IsAny<string>(), It.IsAny<HttpContext>(), It.IsAny<ILogger>()))
                                    .Returns<string, HttpContext, ILogger>((url, httpContext, logger) => 
                                    {
                                        (BlogManagementModel result, ErrorApi error, HttpStatusCode httpStatusCode) = GetBlogContent(url, httpContext, logger);

                                        return Task.FromResult((result, error, httpStatusCode));
                                    });
        }

        public static void SetBlogContentReponse()
        {
            _mockIHttpClientWithToken.Setup(m => m.GetAsync<BlogContentReponse, ErrorApi>(It.IsAny<string>(), It.IsAny<HttpContext>(), It.IsAny<ILogger>()))
                                    .Returns<string, HttpContext, ILogger>((url, httpContext, logger) =>
                                    {
                                        (BlogContentReponse result, ErrorApi error, HttpStatusCode httpStatusCode) = GetBlogContent(url, httpContext, logger);

                                        return Task.FromResult((result, error, httpStatusCode));
                                    });
        }

        public static void SetBlogOperationResponse()
        {
            _mockIHttpClientWithToken.Setup(m => m.PostAsync<BlogOperationResponse, ErrorApi>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<HttpContext>(), It.IsAny<ILogger>()))
                .Returns<string, object, HttpContext, ILogger>((url, peticion, httpContext, logger) => 
                {
                    return Task.FromResult((new BlogOperationResponse 
                    {
                        Errores = new List<string>()
                    }, (ErrorApi)null, HttpStatusCode.OK));
                });

            _mockIHttpClientWithToken.Setup(m => m.PutAsync<BlogOperationResponse, ErrorApi>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<HttpContext>(), It.IsAny<ILogger>()))
               .Returns<string, object, HttpContext, ILogger>((url, peticion, httpContext, logger) =>
               {
                   return Task.FromResult((new BlogOperationResponse
                   {
                       Errores = new List<string>()
                   }, (ErrorApi)null, HttpStatusCode.OK));
               });
        }
    }
}
