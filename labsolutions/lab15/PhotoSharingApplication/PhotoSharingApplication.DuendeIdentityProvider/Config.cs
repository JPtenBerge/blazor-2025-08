using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace PhotoSharingApplication.DuendeIdentityProvider
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
        [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        ];

        public static IEnumerable<ApiScope> ApiScopes =>
        [
            new (name: "comments", displayName: "Comments API")
        ];

        public static IEnumerable<Client> Clients =>
        [
            // interactive client using code flow + pkce
            new ()
            {
                ClientId = "photosharingapplication",
                ClientSecrets = [ new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) ],

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = [ "https://localhost:7246/signin-oidc" ],
                FrontChannelLogoutUri = "https://localhost:7246/signout-oidc",
                PostLogoutRedirectUris = [ "https://localhost:7246/signout-callback-oidc" ],

                AllowOfflineAccess = true,
                AllowedScopes = [ 
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile, 
                    "comments" 
                ]
            }
        ];
    }
}
