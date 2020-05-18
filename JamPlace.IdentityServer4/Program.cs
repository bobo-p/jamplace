using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JamPlace.IdentityServer4.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace JamPlace.IdentityServer4
{
    public class Program
    {
        private static IConfiguration _configuration;
        public static void Main(string[] args)
        {
            _configuration = CreateConfiguration();

            var url = _configuration.GetValue<string>("IdentityHostUrl");

            var host = CreateHostBuilder(args, url).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                
                try
                {
                    var dataSeeder = services.GetRequiredService<IDataSeeder>();
                    dataSeeder.SetupDatabase().Wait(TimeSpan.FromSeconds(30));

                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex,
                    "An error occurred while migrating and seeding the database.");
                }
            }

            host.Run();
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
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile(Path.Combine("Config", $"appsettings.{Environment.MachineName}.json"), optional: true);
                
                //.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true);

            //builder.AddEnvironmentVariables();
            return builder.Build();
        }
    }
}
