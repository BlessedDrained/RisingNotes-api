using IdentityServer4;
using IdentityServer4.Models;

namespace Api.IdentityServer;

public static class IdentityServerConfiguration
{
    public static List<ApiResource> ApiResoures => new()
    {
        new("Api")
        {
            Scopes = {"Api"}
        },
    };

    public static List<IdentityResource> IdentityResources => new()
    {
        new IdentityResources.Email(),
        new IdentityResources.Profile(),
        new IdentityResources.OpenId()
    };

    public static List<ApiScope> ApiScopes => new()
    {
        new("Api"),
    };

    public static List<Client> Clients => new()
    {
        new()
        {
            ClientId = "Api",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            AllowedScopes = {"Api", IdentityServerConstants.StandardScopes.OpenId},
            RequireClientSecret = true,
            ClientSecrets = {new Secret("megaclientsecret".Sha256())},
            AllowOfflineAccess = true,
            UpdateAccessTokenClaimsOnRefresh = true,
            RefreshTokenUsage = TokenUsage.ReUse
        }
    };
}