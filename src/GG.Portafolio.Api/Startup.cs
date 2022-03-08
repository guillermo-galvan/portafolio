using GG.Portafolio.Api.Handler;
using GG.Portafolio.Api.Model;
using GG.Portafolio.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Data.CRUD;
using System.Data.CRUD.MySql;
using System.Globalization;

namespace GG.Portafolio.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("es-MX");

            ConnectionDataBaseCollection.Connections.Add("MainConnection", new MySqlDataBaseConnection(Configuration.GetConnectionString("MainConnection")));

            IConfigurationSection configurationMyKeys = Configuration.GetSection(nameof(OAuthConfiguration));
            OAuthConfiguration oAuthConfiguration = configurationMyKeys.Get<OAuthConfiguration>();
            services.Configure<OAuthConfiguration>(configurationMyKeys);

            services.AddBusinessLogic();
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GGPuntoYComa.Portfolio.Api", Version = "v1" });
            });

            services.AddSingleton<IAuthorizationHandler, AudienceHandler>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = oAuthConfiguration.Authority;
                options.TokenValidationParameters.ValidateAudience = false;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policy.PolicyBasic, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(nameof(OAuthConfiguration.Scope).ToLower(), oAuthConfiguration.Scope);
                    policy.Requirements.Add(new AudienceRequirement(oAuthConfiguration.Audience));
                });

                options.AddPolicy(Policy.PolicyAdmin, policy =>
                {
                    policy.RequireRole(Policy.PolicyAdmin);
                    policy.RequireClaim("area", "Owner");
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy(Policy.PolicyDefault, policy =>
                {
                    // Podríamos especificar únicamente los orígenes permitidos.
                    policy.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GGPuntoYComa.Portfolio.Api v1"));
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(Policy.PolicyDefault);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
