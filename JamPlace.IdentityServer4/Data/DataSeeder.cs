using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using JamPlace.IdentityServer4.IdentityServerConfig;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JamPlace.IdentityServer4.Data
{
    internal class DataSeeder : IDataSeeder
    {
        private const string superadminName = "superadmin@email.com";
        private const string superadminPassword = "JamPlace!123";
        private const string superadminEmail = "superadmin@email.com";
        private const string role = "superAdmin";
        private const string userRole = "user";
        private readonly IIdentityServerConfigurationSetup _identityServerConfig;
        private readonly ConfigurationDbContext _configurationDbContext;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly PersistedGrantDbContext _persistedGrantDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataSeeder(IIdentityServerConfigurationSetup identityServerConfig, ConfigurationDbContext configurationDbContext, ApplicationDbContext applicationDbContext,
            PersistedGrantDbContext persistedGrantDbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _identityServerConfig = identityServerConfig;
            _configurationDbContext = configurationDbContext;
            _applicationDbContext = applicationDbContext;
            _persistedGrantDbContext = persistedGrantDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public  async Task SetupDatabase()
        {
            Console.WriteLine("Seeding database...");
            PerformMigrations();

            SeedConfigurationData();
            await SeedUserData();
            Console.WriteLine("Done seeding database.");
        }

        private void PerformMigrations()
        {
            _applicationDbContext.Database.Migrate();
            _configurationDbContext.Database.Migrate();
            _persistedGrantDbContext.Database.Migrate();
        }

        private void SeedConfigurationData()
        {

            if (!_configurationDbContext.Clients.Any())
            {
                Console.WriteLine("Clients being populated");
                foreach (var client in _identityServerConfig.GetClients().ToList())
                    _configurationDbContext.Clients.Add(client.ToEntity());
                _configurationDbContext.SaveChanges();
            }
            else
                Console.WriteLine("Clients already populated");


            if (!_configurationDbContext.IdentityResources.Any())
            {
                Console.WriteLine("IdentityResources being populated");
                foreach (var resource in _identityServerConfig.GetIdentityResources().ToList())
                    _configurationDbContext.IdentityResources.Add(resource.ToEntity());
                _configurationDbContext.SaveChanges();
            }
            else
                Console.WriteLine("IdentityResources already populated");


            if (!_configurationDbContext.ApiResources.Any())
            {
                Console.WriteLine("ApiResources being populated");
                foreach (var resource in _identityServerConfig.GetApiResources().ToList())
                    _configurationDbContext.ApiResources.Add(resource.ToEntity());
                _configurationDbContext.SaveChanges();
            }
            else
                Console.WriteLine("ApiResources already populated");
        }

        private async Task SeedUserData()
        {
            await CreateDefaultUserRole(_roleManager, userRole);
            if (!_applicationDbContext.Users.Any())
            {
                var superadmin = _userManager.FindByNameAsync(superadminName).Result;
                if (superadmin == null)
                {
                    superadmin = new IdentityUser
                    {
                        UserName = superadminName,
                        Email = superadminEmail,
                        EmailConfirmed = true,
                        TwoFactorEnabled = false,
                    };
                    var result = _userManager.CreateAsync(superadmin, superadminPassword).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    result = _userManager.AddClaimsAsync(superadmin, new Claim[]
                    {
                            new Claim(ClaimTypes.Name, superadminName),
                            new Claim(ClaimTypes.Email, superadminEmail),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean)
                    }).Result;
                    await CreateDefaultAdministratorRole(_roleManager, role);

                    result = _userManager.AddToRoleAsync(superadmin, role).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Console.WriteLine("superadmin created");
                }
                else
                {
                    Console.WriteLine("superadmin already exists");
                }
            }
        }
        

        private static async Task CreateDefaultAdministratorRole(RoleManager<IdentityRole> rm, string administratorRole)
        {

            var ir = await rm.CreateAsync(new IdentityRole(administratorRole));
            if (!ir.Succeeded)
            {
                var exception = new ApplicationException($"Default role `{administratorRole}` cannot be created");
                throw exception;
            }
        }

        private static async Task CreateDefaultUserRole(RoleManager<IdentityRole> rm, string administratorRole)
        {

            var ir = await rm.CreateAsync(new IdentityRole(administratorRole));
            if (!ir.Succeeded)
            {
                var exception = new ApplicationException($"Default role `{administratorRole}` cannot be created because: {ir?.Errors.FirstOrDefault()?.Description}");
                throw exception;
            }
        }
    }
}
