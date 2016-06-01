using Autofac;
using ExpenseTracker.Data;
using ExpenseTracker.Data.Extensions;
using ExpenseTracker.Entities;
using ExpenseTracker.Web.Infrastructure.Mappings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;

namespace ExpenseTracker.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddDbContext<ExpenseTrackerContext>(options =>
                                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"], op => op.MigrationsAssembly("ExpenseTracker.Web")));

            var defaultPolicy = new AuthorizationPolicyBuilder()
                                    .RequireAuthenticatedUser()
                                    .Build();

            services.//AddMvc()
                AddMvc(setup => setup.Filters.Add(new AuthorizeFilter(defaultPolicy)))
                    .AddJsonOptions(opts => opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            services.AddAuthorization(
                opts => {
                    opts.AddPolicy("Name", policy => policy.RequireClaim(System.Security.Claims.ClaimTypes.Name));
                }
            );
            services.AddAuthentication(opts => opts.SignInScheme = "ExpenseTrackerAuthKey");

            AutoMapperConfiguration.Configure();

            return AutofacWebapiConfig.Initialize(services).Resolve<IServiceProvider>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment()) // use below Middlewares
            {
                //app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    serviceScope.ServiceProvider.GetService<ExpenseTrackerContext>().EnsureSeedData();
                }
            }
            else // in prod env use below Middlewares
            {
                app.UseExceptionHandler("/Home/Error");

                //// For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
                //try
                //{
                //    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                //        .CreateScope())
                //    {
                //        serviceScope.ServiceProvider.GetService<ExpenseTracker.Data.ExpenseTrackerContext>()
                //             .Database.Migrate();
                //    }
                //}
                //catch { }
            }

            //app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            var authenticationOptions = new CookieAuthenticationOptions()
            {
                AuthenticationScheme = "ExpenseTrackerAuthKey",
                CookieName = "ApplicationCookie",
                AutomaticAuthenticate = true
                //AutomaticChallenge = true,
            };

            authenticationOptions.Events = new CookieAuthenticationEvents()
            {
                OnValidatePrincipal = Infrastructure.ValidateCookiePrincipal.ValidateAsync

            };

            app.UseCookieAuthentication(authenticationOptions);

            //app.UseIdentity();

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                                .UseKestrel()
                                .UseContentRoot(Directory.GetCurrentDirectory())
                                .UseIISIntegration()
                                .UseStartup<Startup>()
                                .Build();
            host.Run();
        }
    }
}
