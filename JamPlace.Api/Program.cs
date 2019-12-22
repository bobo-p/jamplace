using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JamPlace.DataLayer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace JamPlace.Api
{
    public class Program
    {
        private static IConfiguration _configuration;
        public static void Main(string[] args)
        {
            _configuration = CreateConfiguration();
            var url = _configuration.GetValue<string>("ApiHostUrl");

            var host=CreateHostBuilder(args, url).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    context.Database.SetCommandTimeout(TimeSpan.FromSeconds(30));
                    context.Database.MigrateAsync().Wait(TimeSpan.FromSeconds(15));
                }
                catch (Exception e)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "An error occurred while migrating JamPlace database.");
                }
                host.Run();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args,string url) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls(url);
                });
        private static IConfiguration CreateConfiguration()
        {
            var confFolder = Directory.GetCurrentDirectory();

            var builder = new ConfigurationBuilder()
                .SetBasePath(confFolder)
                .AddJsonFile(Path.Combine("Config", $"appsettings.{Environment.MachineName}.json"), optional: true)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true);

            builder.AddEnvironmentVariables();
            return builder.Build();
        }
    }
}
