using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JamPlace.IdentityServer4.Data
{
    internal interface IDataSeeder
    {
        Task SetupDatabase();
    }
}
