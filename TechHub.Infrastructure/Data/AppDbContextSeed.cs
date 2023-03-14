using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TechHub.Infrastructure.Data;

public static class AppDbContextSeed
{
    public static async Task SeedIdentityAsync(AppDbContext dbContext,
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (dbContext.Database.IsSqlServer())
        {
            dbContext.Database.Migrate();
        }

        if (await roleManager.FindByNameAsync("Admin") is null)
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        string adminUserName = "TechHubAdmin";
        string adminDisplayName = "TechHub";
        string adminEmail = "techhubadmin@gamil.com";
        var adminUser = new ApplicationUser
        {
            UserName = adminUserName,
            DisplayName = adminDisplayName,
            Email = adminEmail
        };

        if (await userManager.FindByNameAsync(adminUserName) is null)
        {
            await userManager.CreateAsync(adminUser, "Pass@word1");
            adminUser = await userManager.FindByNameAsync(adminUserName);
            await userManager.AddToRoleAsync(adminUser!, "Admin");
        }
    }
}
