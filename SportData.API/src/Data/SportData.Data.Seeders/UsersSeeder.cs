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
        var userManager = services.GetService<UserManager<User>>();

        await this.SeedUserAsync(userManager, "dobriSuperadmin", "1234", "dobriSuperadmin@sportData.com", [ApplicationRoles.SUPERADMIN, ApplicationRoles.ADMIN, ApplicationRoles.EDITOR, ApplicationRoles.USER]);
        await this.SeedUserAsync(userManager, "dobriAdmin", "1234", "dobriAdmin@sportData.com", [ApplicationRoles.ADMIN, ApplicationRoles.EDITOR, ApplicationRoles.USER]);
        await this.SeedUserAsync(userManager, "dobriEditor", "1234", "dobriEditor@sportData.com", [ApplicationRoles.EDITOR, ApplicationRoles.USER]);
        await this.SeedUserAsync(userManager, "dobriUser", "1234", "dobriUser@sportData.com", [ApplicationRoles.USER]);
    }

    private async Task SeedUserAsync(UserManager<User> userManager, string username, string password, string email, List<string> roles)
    {
        var user = await userManager.FindByNameAsync(username);
        if (user == null)
        {
            user = new User
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

            foreach (var role in roles)
            {
                await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}