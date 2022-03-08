using GG.Portafolio.BusinessLogic.Blog;
using GG.Portafolio.BusinessLogic.Dealer;
using GG.Portafolio.BusinessLogic.User;
using GG.Portafolio.DataBaseBusiness;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BussinessLogicCollectionExtensions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            //Management
            services.AddScoped<DealerManagement>();
            services.AddScoped<BlogManagement>();
            services.AddScoped<UserManagement>();

            services.AddTransient<DatabaseOperation>();

            services.AddDataBaseBusiness();

            return services;
        }
    }
}
