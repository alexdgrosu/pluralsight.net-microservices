using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using GloboTicket.Web.Models;
using GloboTicket.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GloboTicket.Web
{
    public class Startup
    {
        protected IConfiguration Configuration { get; }
        protected IHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var mvcBuilder = services.AddControllersWithViews();

            var httpClients = new List<IHttpClientBuilder>
            {
                services.AddHttpClient<IEventCatalogService, EventCatalogService>(config =>
                    config.BaseAddress = new Uri(Configuration["ApiConfigs:EventCatalog:Uri"])),
                services.AddHttpClient<IShoppingBasketService, ShoppingBasketService>(config =>
                    config.BaseAddress = new Uri(Configuration["ApiConfigs:ShoppingBasket:Uri"]))
            };

            if (Environment.IsDevelopment())
            {
                mvcBuilder.AddRazorRuntimeCompilation();
                httpClients.ForEach(client =>
                {
                    client.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                    {
                        ClientCertificateOptions = ClientCertificateOption.Manual,
                        ServerCertificateCustomValidationCallback = (_message, _cert, _certChain, _policyErrors) => true
                    });
                });
            }

            services.AddSingleton<Settings>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=EventCatalog}/{action=Index}/{id?}"
                );
            });
        }
    }
}
