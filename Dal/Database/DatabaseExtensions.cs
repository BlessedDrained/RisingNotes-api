using Dal.Context;
using MainLib.Api.Auth.Constant;
using Microsoft.AspNetCore.Identity;

namespace Dal.Database;

public static class DatabaseExtensions
{
    public static async Task RecreateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
    }

    public static async Task PopulateRoles(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await roleManager.CreateAsync(new IdentityRole(RoleConstants.Author));
        await roleManager.CreateAsync(new IdentityRole(RoleConstants.Admin));
        await roleManager.CreateAsync(new IdentityRole(RoleConstants.User));
    }
}