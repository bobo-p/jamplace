using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JamPlace.IdentityServer4.IdentityServerConfig
{
    internal interface IIdentityServerConfigurationSetup
    {
        IEnumerable<ApiResource> GetApiResources();
        IEnumerable<IdentityResource> GetIdentityResources();
        IEnumerable<Client> GetClients();
    }
}
