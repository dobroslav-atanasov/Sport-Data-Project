namespace SportData.Data.Seeders;

using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using SportData.Common.Constants;
using SportData.Data.Models.Entities.SportData;
using SportData.Data.Seeders.Interfaces;

public class UsersSeeder : ISeeder
{
    public async Task SeedAsync(IServiceProvider services)
    {
        var userManager = services.GetService<UserManager<ApplicationUser>>();
        var roleManager = services.GetService<RoleManager<ApplicationRole>>();

        await this.SeedUserAsync(userManager, roleManager, "dobriSuperadmin", "1234", "dobriSuperadmin@sportData.com", ApplicationRoles.SUPERADMIN);
        await this.SeedUserAsync(userManager, roleManager, "dobriAdmin", "1234", "dobriAdmin@sportData.com", ApplicationRoles.ADMIN);
        await this.SeedUserAsync(userManager, roleManager, "dobriEditor", "1234", "dobriEditor@sportData.com", ApplicationRoles.EDITOR);
        await this.SeedUserAsync(userManager, roleManager, "dobriUser", "1234", "dobriUser@sportData.com", ApplicationRoles.USER);
    }

    private async Task SeedUserAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, string username, string password, string email, string roleName)
    {
        var user = await userManager.FindByNameAsync(username);
        if (user == null)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            user = new ApplicationUser
            {
                UserName = username,
                Email = email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
            }

            await userManager.AddToRoleAsync(user, roleName);
        }
    }
}