using IdentityModel;
using IdentityServer4.Models;
using JamPlace.IdentityServer4.Models.AppConfigModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace JamPlace.IdentityServer4.IdentityServerConfig
{
    public class IdentityServerConfigurationSetup : IIdentityServerConfigurationSetup
    {
        private readonly Urls _urls;
        private readonly IdentityServerConfigData _identityServerConfigData;

        public IdentityServerConfigurationSetup(Urls urls, IdentityServerConfigData identityServerConfigData)
        {
            _urls = urls;
            _identityServerConfigData = identityServerConfigData;
        }

        public IEnumerable<ApiResource> GetApiResources()
        {
            var apiResource = new ApiResource(_identityServerConfigData.ApiResourceName, _identityServerConfigData.ApiResourceDisplayName)
            {
                UserClaims =
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.Profile,
                    JwtClaimTypes.Role
                }
            };
          
            return new List<ApiResource>
            {
               apiResource,               
            };
        }

        public IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                 new Client
                 {
                    ClientId = _identityServerConfigData.ClientId,
                    ClientName = _identityServerConfigData.ClientName,
                    AccessTokenType = AccessTokenType.Jwt,
                    RequireConsent = false,
                    AccessTokenLifetime = 330,// 330 seconds, default 60 minutes
                    IdentityTokenLifetime = 9000,
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = true,
                    
                    RedirectUris = new List<string>
                    {
                        $"{_urls.AppUrl}/auth-callback",
                        $"{_urls.AppUrl}/silent-renew.html"

                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        $"{_urls.AppUrl}/unauthorized",
                        $"{_urls.AppUrl}"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        $"{_urls.AppUrl}"
                    },
                    AllowedScopes = new List<string>
                    {
                        StandardScopes.OpenId,
                        StandardScopes.Email,
                        StandardScopes.Profile,
                        _identityServerConfigData.ApiResourceName,
                        //IdentityServerConstants.LocalApi.ScopeName
                    },
                },
            };
        }

        public IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }
    }
}
