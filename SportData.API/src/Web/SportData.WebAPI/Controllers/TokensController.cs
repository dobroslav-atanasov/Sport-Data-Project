﻿namespace SportData.WebAPI.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SportData.Common.Constants;
using SportData.Data.Models.Entities.SportData;
using SportData.Data.Models.Users;
using SportData.Data.ViewModels.Users;
using SportData.Services.Interfaces;

public class TokensController : BaseController
{
    private readonly UserManager<User> userManager;
    private readonly IJwtService jwtService;
    private readonly IConfiguration configuration;

    public TokensController(UserManager<User> userManager, IJwtService jwtService, IConfiguration configuration)
    {
        this.userManager = userManager;
        this.jwtService = jwtService;
        this.configuration = configuration;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateToken(LoginModel input)
    {
        var user = await this.userManager.FindByNameAsync(input.Username);
        if (user != null && await this.userManager.CheckPasswordAsync(user, input.Password))
        {
            var roles = await this.userManager.GetRolesAsync(user);
            var token = this.jwtService.GenerateToken(user, roles);
            var refreshToken = this.jwtService.GenerateRefreshToken();
            token.RefreshToken = refreshToken;
            token.Username = user.UserName;

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(int.Parse(this.configuration[GlobalConstants.JWT_REFRESH_TOKEN_VALIDITY_IN_DAYS]));

            await this.userManager.UpdateAsync(user);

            return this.Ok(token);
        }

        return this.Unauthorized(new { Message = "Invalid username or password!" });
    }

    [HttpPost(RouteConstants.TOKENS_CREATE_REFRESH_TOKEN)]
    [AllowAnonymous]
    public async Task<IActionResult> CreateRefreshToken(TokenModel input)
    {
        if (input == null)
        {
            return this.BadRequest(new { Message = "Invalid token!" });
        }

        //var principal = this.jwtService.ValidateToken(input.AccessToken);
        //if (principal == null)
        //{
        //    return this.BadRequest(new { Message = "Invalid access token or refresh token!" });
        //}

        //var username = principal.Identity.Name;

        var user = await this.userManager.FindByNameAsync(input.Username);
        if (user == null || user.RefreshToken != input.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return this.BadRequest(new { Message = "Invalid access token or refresh token!" });
        }

        var roles = await this.userManager.GetRolesAsync(user);
        var newToken = this.jwtService.GenerateToken(user, roles);
        var newRefreshToken = this.jwtService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await this.userManager.UpdateAsync(user);

        var tokenModel = new TokenModel
        {
            AccessToken = newToken.AccessToken,
            RefreshToken = newRefreshToken,
            //ExpirationInSeconds = user.RefreshTokenExpiryTime.Second
        };

        return this.Ok(tokenModel);
    }

    [HttpDelete]
    [Authorize(Roles = $"{Roles.SUPERADMIN},{Roles.ADMIN}")]
    public async Task<IActionResult> DeleteRefreshToken(string username)
    {
        var user = await this.userManager.FindByNameAsync(username);
        if (user == null)
        {
            return this.BadRequest(new { Message = "Invalid username!" });
        }

        user.RefreshToken = null;
        await this.userManager.UpdateAsync(user);

        return this.Ok(new { Message = $"Refresh token of user: {username} was revoked." });
    }

    [HttpPost(RouteConstants.TOKENS_DELETE_REFRESH_TOKENS)]
    [Authorize(Roles = $"{Roles.SUPERADMIN},{Roles.ADMIN}")]
    public async Task<IActionResult> DeleteRefreshTokens()
    {
        var users = await this.userManager.Users.ToListAsync();

        foreach (var user in users)
        {
            user.RefreshToken = null;
            await this.userManager.UpdateAsync(user);
        }

        return this.Ok(new { Message = $"Refresh token revoked for all users." });
    }
}