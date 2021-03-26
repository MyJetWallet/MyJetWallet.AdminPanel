using System;
using System.Reflection;
using System.Text;
using Autofac;
using Backoffice.Middlewares;
using Backoffice.Modules;
using Backoffice.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MW.Blazor;
using MySettingsReader;
using Prometheus;
using SimpleTrading.BaseMetrics;
using SimpleTrading.ServiceStatusReporterConnector;
using Sotsera.Blazor.Toaster.Core.Models;

namespace Backoffice
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);
            TagSelectorStyle.Bootstrap.RemoveTagClass = "oi oi-circle-x";

            // var settings = new SettingsModel
            // {
            //     PersonalDataGrpcHostPort = "http://127.0.0.1:9000",
            //     AccountsGrpcHostPort = "http://127.0.0.1:9000",
            //     TransactionsGrpcHostPort = "http://127.0.0.1:9000",
            //     ActiveDealsGrpcService = "http://127.0.0.1:9000",
            //     LogsGrpcUrl = "http://127.0.0.1:9000",
            //     KycGrpcUrl = "http://127.0.0.1:9000",
            //     StatusesGrpcUrl = "http://127.0.0.1:9000",
            //     AuthGrpcServiceUrl = "http://127.0.0.1:9000",
            //     PhonePoolServiceUrl = "http://127.0.0.1:9000",
            //     TableStorageConnectionString =
            //         "DefaultEndpointsProtocol=https;AccountName=monfexpa;AccountKey=Mm31LgHvNKWSnkhHEXWAK9zJipsxfRTgrGm23GiSasZHszO5agEwLfhaZLFoC7647mxmIH2+9rikJFlOvdJ7WQ==;EndpointSuffix=core.windows.net",
            //     TokenKey = "test",
            //     DepositManagerGrpcService = "http://127.0.0.1:9001",
            //     HpApi = "https://trading-api-test.handelpro.biz",
            //     MonfexApi = "https://trading-api-test.mnftx.biz",
            //     AlianceApi = "https://trading-api-test.allianzmarket.biz"
            //     
            // };
            var settings = Program.Settings;
            
            TokenStore.Token = Encoding.UTF8.GetBytes(settings.TokenKey);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHttpContextAccessor();

            services.AddToaster(config =>
            {
                config.PositionClass = Defaults.Classes.Position.TopRight;
                config.PreventDuplicates = false;
                config.NewestOnTop = true;
            });

            services.AddHostedService<ApplicationLifetimeManager>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ApiTraceMiddleware>();
            
            app.BindMetricsMiddleware();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.BindServicesTree(Assembly.GetExecutingAssembly());
            app.BindIsAlive();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapControllers();
                endpoints.MapFallbackToPage("/_Host");
                endpoints.MapMetrics();
                
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<MyNoSqlModule>();
            builder.RegisterModule<ClientsModule>();
        }
    }
}