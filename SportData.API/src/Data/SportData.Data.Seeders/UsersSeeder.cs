﻿namespace SportData.Data.Seeders;

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

        await this.SeedUserAsync(userManager, "dobriSuperadmin", "1234", "dobriSuperadmin@sportData.com", [Roles.SUPERADMIN, Roles.ADMIN, Roles.EDITOR, Roles.USER]);
        await this.SeedUserAsync(userManager, "dobriAdmin", "1234", "dobriAdmin@sportData.com", [Roles.ADMIN, Roles.EDITOR, Roles.USER]);
        await this.SeedUserAsync(userManager, "dobriEditor", "1234", "dobriEditor@sportData.com", [Roles.EDITOR, Roles.USER]);
        await this.SeedUserAsync(userManager, "dobriUser", "1234", "dobriUser@sportData.com", [Roles.USER]);
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
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = "Dobroslav",
                LastName = "Atanasov",
                BirthDate = DateTime.ParseExact("12.02.1987", "dd.MM.yyyy", null),
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