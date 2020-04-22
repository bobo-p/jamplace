using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JamPlace.DataLayer;
using JamPlace.DataLayer.Entities;
using JamPlace.DataLayer.Repositories;
using JamPlace.DomainLayer.Interfaces.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using AutoMapper;
using JamPlace.DataLayer.Mapper;
using System.IO;
using Microsoft.AspNetCore.HttpOverrides;
using JamPlace.DomainLayer.Interfaces.Services;
using JamPlace.DomainLayer.Services;
using JamPlace.Api.Mapper;

namespace JamPlace.Api
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
            services.AddControllers();
            services.AddAuthorization();
            services.AddEntityFrameworkNpgsql()
             .AddDbContext<ApplicationDbContext>(options =>
             {
                 options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging();
             });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = Configuration.GetValue<string>("App:IdentityServer");
                o.Audience = Configuration.GetValue<string>("App:Audience");
                o.RequireHttpsMetadata = false;
            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.RequireHeaderSymmetry = false;
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName, policy =>
                {
                    policy.AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials()
                          .WithOrigins(Configuration.GetValue<string>("App:ClientRootAddress"));
                });
            });
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DataObjectsMapperProfile());
                cfg.AddProfile(new ApiMapperProfile());
            }).CreateMapper());

            
            services.AddTransient<IJamEventRepository, JamEventRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IJamUserRepository, JamUserRepository>();
            services.AddTransient<ISongRepository, SongRepository>();
            services.AddTransient<IEquipmentRepository, EquipmentRepository>();
            services.AddTransient<IJamEventService, JamEventService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(CorsPolicyName);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseForwardedHeaders();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
