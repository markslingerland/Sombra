using System;
using System.IO;
using AutoMapper;
using EasyNetQ;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sombra.Messaging;
using Sombra.Web.Infrastructure;
using Sombra.Web.Infrastructure.Filters;
using Sombra.Web.Infrastructure.Authentication;
using Sombra.Web.Services;
using Sombra.Web.Infrastructure.Messaging;

namespace Sombra.Web
{
    public class Startup
    {
        private static string _rabbitMqConnectionString;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            SetupConfiguration();

            services.AddMvc(options =>

            {
                options.Filters.Add(new RequestLoggingFilter());
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                options.Filters.Add(new ValidatorActionFilter());
                options.Filters.Add(new ExceptionLoggingFilterAttribute());
                options.Filters.Add(new SubdomainActionFilter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMemoryCache();
            services.AddCors();

            services.AddScoped(c => RabbitHutch.CreateBus(_rabbitMqConnectionString));
            services.AddScoped<ICachingBus, CachingRabbitBus>();

            services.AddAutoMapper();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<AuthenticationManager>();
            services.AddScoped<UserService>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            Logger.SetServiceProvider(services.BuildServiceProvider());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseCors(
                options => options
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .WithOrigins("http://*.ikdoneer.nu")
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .AllowAnyHeader()

            );
            app.UseMvc(Routing.Setup);
        }

        private static void SetupConfiguration()
        {
            var isContainerized = Environment.GetEnvironmentVariable("CONTAINER_TYPE") != null;
            if (isContainerized)
            {
                _rabbitMqConnectionString = Environment.GetEnvironmentVariable("RABBITMQ_CONNECTIONSTRING");
            }
            else
            {
                var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();

                _rabbitMqConnectionString = config["RABBITMQ_CONNECTIONSTRING"];
            }
        }
    }
}
