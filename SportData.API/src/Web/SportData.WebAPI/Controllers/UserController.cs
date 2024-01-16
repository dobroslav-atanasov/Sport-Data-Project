namespace SportData.WebAPI.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SportData.Data.Models.Jwt;
using SportData.Services.Interfaces;

[Authorize]
public class UserController : BaseController
{
    private readonly IJwtService jwtService;

    public UserController(IJwtService jwtService)
    {
        this.jwtService = jwtService;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("CreateToken")]
    public IActionResult CreateToken(User user)
    {
        if (user.Username == "dobri" && user.Password == "123")
        {
            var token = this.jwtService.GenerateToken(user);
            return this.Ok(token);
        }

        return this.Unauthorized();
    }

    [HttpGet]
    [Route("Test")]
    public IActionResult Test()
    {
        return this.Ok("Hi");
    }
}