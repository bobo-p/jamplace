using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using JamPlace.IdentityServer4.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using IdentityServer4.EntityFramework.DbContexts;
using JamPlace.IdentityServer4.Models.AppConfigModels;
using JamPlace.IdentityServer4.IdentityServerConfig;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IO;
using Microsoft.AspNetCore.HttpOverrides;

namespace JamPlace.IdentityServer4
{
    public class Startup
    {
        private const string CorsPolicyName = "MainPolicy";
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)

               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile(Path.Combine("Config", $"appsettings.{Environment.MachineName}.json"), optional: true);




            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddSingleton(Configuration.GetSection("Urls").Get<Urls>());
            services.AddSingleton(Configuration.GetSection("IdentityServerConfigData").Get<IdentityServerConfigData>());
            var cfg = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.RequireHeaderSymmetry = false;
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddIdentityServer(x =>
            {
                x.IssuerUri = Configuration.GetValue<string>("Urls:IssuerUri");

            })
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<IdentityUser>()
                .AddConfigurationStore(options => {
                    options.ConfigureDbContext = builder =>
                        builder.UseNpgsql(connectionString,
                            db => db.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseNpgsql(connectionString,
                            db => db.MigrationsAssembly(migrationsAssembly));
                });

            services.AddTransient<IIdentityServerConfigurationSetup, IdentityServerConfigurationSetup>();
            services.AddTransient<IDataSeeder, DataSeeder>();

            services.AddControllersWithViews(options => options.EnableEndpointRouting = false);
            services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");

            }).AddRazorRuntimeCompilation();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer();
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName, policy =>
                {
                    policy.AllowAnyHeader()
                          .AllowAnyMethod().AllowAnyOrigin();
                          //.AllowCredentials()
                          //.WithOrigins(Configuration.GetValue<string>("Urls:AppUrl"));
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(CorsPolicyName);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
            app.UseForwardedHeaders();
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityServer();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //    endpoints.MapRazorPages();
            //});
            app.UseMvcWithDefaultRoute();
        }
    }
}
