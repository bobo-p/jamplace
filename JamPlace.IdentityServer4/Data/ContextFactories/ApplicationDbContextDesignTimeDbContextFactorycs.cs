using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JamPlace.IdentityServer4.Data.ContextFactories
{
    public class ApplicationDbContextDesignTimeDbContextFactorycs : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            builder.UseNpgsql("Host=localhost; Database=maplus.identityserver;Username=postgres;Password=zaq1@WSX");
            return new ApplicationDbContext(builder.Options);
        }
    }
}
