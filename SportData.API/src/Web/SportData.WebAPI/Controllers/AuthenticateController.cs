namespace SportData.WebAPI.Controllers;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SportData.Common.Constants;
using SportData.Data.Models.Authentication;
using SportData.Data.Models.Entities.SportData;
using SportData.Services.Interfaces;

public class AuthenticateController : BaseController
{
    private readonly UserManager<User> userManager;
    private readonly RoleManager<Role> roleManager;
    private readonly SignInManager<User> signInManager;
    private readonly IJwtService jwtService;
    private readonly IConfiguration configuration;

    public AuthenticateController(UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager, IJwtService jwtService, IConfiguration configuration)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.signInManager = signInManager;
        this.jwtService = jwtService;
        this.configuration = configuration;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register(RegisterModel input)
    {
        var userExist = await this.userManager.FindByNameAsync(input.Username);
        if (userExist != null)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "User already exists!" });
        }

        var user = new User
        {
            Email = input.Email,
            UserName = input.Username,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        var result = await this.userManager.CreateAsync(user, input.Password);
        if (!result.Succeeded)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "User can not be registered!" });
        }

        await this.userManager.AddToRoleAsync(user, ApplicationRoles.USER);

        return this.Ok(new ResponseModel { Status = "Success", Message = "User created successfully!" });
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login(LoginModel input)
    {
        var user = await this.userManager.FindByNameAsync(input.Username);
        if (user != null && await this.userManager.CheckPasswordAsync(user, input.Password))
        {
            var roles = await this.userManager.GetRolesAsync(user);
            var token = this.jwtService.GenerateToken(user, roles);
            var refreshToken = this.jwtService.GenerateRefreshToken();
            token.RefreshToken = refreshToken;

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(int.Parse(this.configuration["Jwt:RefreshTokenValidityInDays"]));

            await this.userManager.UpdateAsync(user);

            return this.Ok(token);
        }

        return this.Unauthorized(new ResponseModel { Status = "Error", Message = "Invalid username or password!" });
    }

    [HttpPost]
    [Route("RefreshToken")]
    public async Task<IActionResult> RefreshToken(TokenInputModel input)
    {
        if (input == null)
        {
            return this.BadRequest("Invalid token!");
        }

        var principal = this.jwtService.ValidateToken(input.AccessToken);
        if (principal == null)
        {
            return this.BadRequest("Invalid access token or refresh token!");
        }

        var username = principal.Identity.Name;

        var user = await this.userManager.FindByNameAsync(username);
        if (user == null || user.RefreshToken != input.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return this.BadRequest("Invalid access token or refresh token!");
        }

        var roles = await this.userManager.GetRolesAsync(user);
        var newAccessToken = this.jwtService.GenerateToken(user, roles);
        var newRefreshToken = this.jwtService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await this.userManager.UpdateAsync(user);

        return this.Ok(new
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
        });
    }

    [HttpPost]
    [Route("Revoke/{username}")]
    public async Task<IActionResult> Revoke(string username)
    {
        var user = await this.userManager.FindByNameAsync(username);
        if (user == null)
        {
            return this.BadRequest("Invalid username!");
        }

        user.RefreshToken = null;
        await this.userManager.UpdateAsync(user);

        return NoContent();
    }

    [HttpPost]
    [Route("RevokeAll")]
    public async Task<IActionResult> RevokeAll()
    {
        var users = await this.userManager.Users.ToListAsync();

        foreach (var user in users)
        {
            user.RefreshToken = null;
            await this.userManager.UpdateAsync(user);
        }

        return NoContent();
    }
}