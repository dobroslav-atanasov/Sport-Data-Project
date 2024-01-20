namespace SportData.WebAPI.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using SportData.Common.Constants;
using SportData.Data.Models.Authentication;
using SportData.Data.Models.Entities.Enumerations;
using SportData.Data.Models.Entities.SportData;
using SportData.Data.Models.Jwt;

public class AuthenticateController : BaseController
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<ApplicationRole> roleManager;

    public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create(RegisterModel input)
    {
        var userExist = await this.userManager.FindByNameAsync(input.Username);
        if (userExist != null)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = ResponseStatus.Error, Message = "User already exists!" });
        }

        var user = new ApplicationUser
        {
            Email = input.Email,
            UserName = input.Username,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        var result = await this.userManager.CreateAsync(user, input.Password);
        if (!result.Succeeded)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = ResponseStatus.Error, Message = "User can not be registered!" });
        }

        await this.userManager.AddToRoleAsync(user, ApplicationRoles.USER);

        return this.Ok(new ResponseModel { Status = ResponseStatus.Success, Message = "User created successfully!" });
    }

    [HttpPost]
    [Route("CreateWithRole")]
    [Authorize(Roles = ApplicationRoles.SUPERADMIN)]
    public async Task<IActionResult> CreateWithRole(RegisterModel input)
    {
        var userExist = await this.userManager.FindByNameAsync(input.Username);
        if (userExist != null)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = ResponseStatus.Error, Message = "User already exists!" });
        }

        var user = new ApplicationUser
        {
            Email = input.Email,
            UserName = input.Username,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        var result = await this.userManager.CreateAsync(user, input.Password);
        if (!result.Succeeded)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = ResponseStatus.Error, Message = "User can not be registered!" });
        }

        await this.userManager.AddToRoleAsync(user, input.Role);

        return this.Ok(new ResponseModel { Status = ResponseStatus.Success, Message = "User created successfully!" });
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("CreateToken")]
    public IActionResult CreateToken(User user)
    {
        //if (user.Username == "dobri" && user.Password == "123")
        //{
        //    var token = this.jwtService.GenerateToken(user);
        //    return this.Ok(token);
        //}

        return this.Unauthorized();
    }
}