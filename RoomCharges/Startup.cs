using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewRelic.LogEnrichers.Serilog;
using RoomCharges.Services;
using Serilog;
using Serilog.Core;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using System;

namespace RoomCharges
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<BusinessConfigurationService>();
            services.AddSingleton<ReservationSearchService>();
            services.AddSingleton<FolioService>();
            services.AddSingleton<EmailService>();
            services.AddScoped<BusinessMenuService>();

            var logger = SetupLogger();
            if (logger != null)
            {
                services.AddSingleton(logger);
            }
        }

        private Logger SetupLogger()
        {
            var logLocation = Configuration.GetValue<string>("LogDiskLocation");
            var loggerConfig = new LoggerConfiguration();

            loggerConfig
               .Enrich.WithThreadName()
               .Enrich.WithThreadId()
               .Enrich.WithNewRelicLogsInContext()
               .Enrich.WithExceptionDetails()
               .WriteTo.File(
                    formatter: new CompactJsonFormatter(),
                    path: logLocation + @"file.log.json",
                    rollingInterval: RollingInterval.Day)
               .WriteTo.File(
                   formatter: new NewRelicFormatter(),
                   path: logLocation + @"newrelic.log.json",
                   rollingInterval: RollingInterval.Day);

            var logger = loggerConfig.CreateLogger();

            logger.Information($"Starting PreReg logging at {DateTime.Now}");
            return logger;
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
