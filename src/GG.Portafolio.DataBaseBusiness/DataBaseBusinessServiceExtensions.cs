using GG.Portafolio.DataBaseBusiness.Business;
using GG.Portafolio.DataBaseBusiness.Sentences;
using System.Data.CRUD;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DataBaseBusinessServiceExtensions
    {
        public static IServiceCollection AddDataBaseBusiness(this IServiceCollection services)
        {
            //Criteria Builder
            services.AddTransient<ICriteriaBuilder, CriteriaBuilder>();

            //Sentences
            services.AddScoped<UserSentences>();
            services.AddScoped<BlogSentences>();
            services.AddScoped<BlogDetailSentences>();
            services.AddScoped<BlogCommentsSentences>();

            //Business
            services.AddScoped<BlogBusiness>();            
            services.AddScoped<UserBusiness>();
            services.AddScoped<BlogDetailBusiness>();
            services.AddScoped<BlogCommentsBusiness>();

            return services;
        }
    }
}
