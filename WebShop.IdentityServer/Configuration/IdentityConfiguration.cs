using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace WebShop.IdentityServer.Configuration;

public class IdentityConfiguration
{
    public const string Admin = "Admin";
    public const string Client = "Client";

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
          new IdentityResources.OpenId(),
          new IdentityResources.Email(),
          new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            // webshop é aplicação que vai acessar
            // o IdentityServer para obter o token
            new ApiScope("webshop", "WebShop API"),
            new ApiScope(name: "read", "Read data."),
            new ApiScope(name: "write", "Write data."),
            new ApiScope(name: "delete", "Delete data.")

        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            //cliente genérico
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("abracadabra#simsalabim".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials, // precisa das credenciais do usuario
                AllowedScopes = {"read", "write", "profile"}
                },
            new Client
            {
                ClientId = "webshop",
                ClientSecrets = {new Secret("abracadabra#simsalabim".Sha256())},
                AllowedGrantTypes = GrantTypes.Code, //via código
                RedirectUris = { "https://localhost:7164/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:7164/signout-callback-oidc" },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "webshop"
                }
            }
        };
}
