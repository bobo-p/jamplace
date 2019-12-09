using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JamPlace.IdentityServer4.Models.AppConfigModels
{
    public class IdentityServerConfigData
    {
        public string ApiResourceName { get; set; }
        public string ApiResourceDisplayName {get;set;}
        public string ClientId {get;set;}
        public string ClientName {get;set;}
    }
}
