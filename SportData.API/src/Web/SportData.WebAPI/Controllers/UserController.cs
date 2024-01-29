namespace SportData.WebAPI.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using SportData.Data.Models.Entities.SportData;
using SportData.Services.Interfaces;

[Authorize]
public class UserController : BaseController
{
    private readonly IJwtService jwtService;
    private readonly UserManager<User> userManager;
    private readonly RoleManager<Role> roleManager;

    public UserController(IJwtService jwtService, UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        this.jwtService = jwtService;
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    //[HttpPost]
    //[AllowAnonymous]
    //[Route("CreateToken")]
    //public IActionResult CreateToken(User user)
    //{
    //    if (user.Username == "dobri" && user.Password == "123")
    //    {
    //        var token = this.jwtService.GenerateToken(user);
    //        return this.Ok(token);
    //    }

    //    return this.Unauthorized();
    //}

    //public async Task<IActionResult> CreateUser()
    //{
    //    //this.userManager.addr
    //    //this.userManager.CreateAsync()
    //    return this.Ok();
    //}

    [HttpGet]
    [Route("Test")]
    public IActionResult Test()
    {
        return this.Ok("Hi");
    }
}