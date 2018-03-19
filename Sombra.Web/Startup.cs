using System;
using EasyNetQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sombra.Web
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
            services.AddMvc();
            services.AddScoped(c => CreateBus());
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
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static IBus CreateBus()
        {
            var rabbitMqHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST");
            var rabbitMqUser = Environment.GetEnvironmentVariable("RABBITMQ_USER");
            var rabbitMqPassword = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD");

            return RabbitHutch.CreateBus($"host={rabbitMqHost};username={rabbitMqUser};password={rabbitMqPassword}");
        }
    }
}
