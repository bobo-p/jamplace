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

namespace JamPlace.Api
{
    public class Startup
    {
        private const string CorsPolicyName = "MainPolicy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
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
            }).CreateMapper());

            
            services.AddTransient<IBasicJamEventRepository, BasicJamEventRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IJamUserRepository, JamUserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(CorsPolicyName);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }          
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
