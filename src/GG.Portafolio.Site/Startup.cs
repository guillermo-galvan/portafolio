using GG.Portafolio.Shared;
using GG.Portafolio.Site.Generic;
using GG.Portafolio.Site.Generic.Events;
using GG.Portafolio.Site.Generic.Interfaces;
using GG.Portafolio.Site.Generic.Rest;
using GG.Portafolio.Site.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Net.Http;

namespace GG.Portafolio.Site
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

            IConfigurationSection configurationSection = Configuration.GetSection(nameof(ConfigurationValues));
            ConfigurationValues configurationValues = configurationSection.Get<ConfigurationValues>();
            services.Configure<ConfigurationValues>(configurationSection);
            services.Configure<OAuthConfiguration>(Configuration.GetSection(nameof(OAuthConfiguration)));

            configurationSection = Configuration.GetSection(nameof(OAuthConfigurationUser));
            OAuthConfigurationUser oAuthConfigurationUser = configurationSection.Get<OAuthConfigurationUser>();
            services.Configure<OAuthConfigurationUser>(configurationSection);

            services.AddHttpClient(nameof(ConfigurationValues.BackEndURL), client =>
            {
                client.BaseAddress = new Uri(configurationValues.BackEndURL);

            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true
                };
            });
            services.AddHttpClient(nameof(ConfigurationValues.SsoUrl), client =>
            {
                client.BaseAddress = new Uri(configurationValues.SsoUrl);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true
                };
            });

            services.AddSingleton<IHttpClient, HttpClientRest>();
            services.AddSingleton<IHttpClientWithToken, HttpClientRestWithToken>();
            services.AddSingleton<IAccessToken, AccessToken>();
            services.AddSingleton<CustomCookieAuthenticationEvents>();
            services.AddSingleton<CustomOpenIdConnectEvents>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.Name = ".ggpuntoycoma.session";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(oAuthConfigurationUser.CookieExpireTimeSpan);
                options.SlidingExpiration = true;
                options.EventsType = typeof(CustomCookieAuthenticationEvents);
            })
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
           {
               options.Authority = configurationValues.SsoUrl;
               options.ClientId = oAuthConfigurationUser.ClientId;
               options.ClientSecret = oAuthConfigurationUser.ClientSecret;
               options.ResponseType = "code";
               options.SaveTokens = true;
               options.GetClaimsFromUserInfoEndpoint = true;
               options.ClaimActions.MapUniqueJsonKey("address_UserInfo", "address");
               options.ClaimActions.MapUniqueJsonKey("email_verified_UserInfo", "email_verified");
               options.ClaimActions.MapUniqueJsonKey("website_UserInfo", "website");
               options.ClaimActions.MapUniqueJsonKey("phone_number_UserInfo", "phone_number");
               options.ClaimActions.MapUniqueJsonKey("user_area", "area");
               options.ClaimActions.MapJsonKey("user_role", "role");

               foreach (var item in oAuthConfigurationUser.Scope.Split(" "))
               {
                   options.Scope.Add(item);
               }

               options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
               {
                   RoleClaimType = "user_role",
               };
               options.EventsType = typeof(CustomOpenIdConnectEvents);
           });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policy.PolicyBasic, policy =>
                {
                    policy.RequireAuthenticatedUser();
                });

                options.AddPolicy(Policy.PolicyAdmin, policy =>
                {
                    policy.RequireRole(Policy.PolicyAdmin);
                    policy.RequireClaim("user_area", "Owner");
                });

                options.AddPolicy("All", policy =>
                {
                    policy.RequireRole("Admin", "Seller", "Accountant");
                });
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });

            services.AddControllersWithViews();

#if DEBUG
            services.AddHostedService(sp => new NpmWatchHostedService(
                enabled: sp.GetRequiredService<IWebHostEnvironment>().IsDevelopment(),
                logger: sp.GetRequiredService<ILogger<NpmWatchHostedService>>()));
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseCookiePolicy();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
