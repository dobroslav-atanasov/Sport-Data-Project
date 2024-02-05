namespace SportData.WebAPI.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SportData.Common.Constants;
using SportData.Data.Models.Responses;

[Authorize(Roles = $"{Roles.SUPERADMIN},{Roles.ADMIN}")]
public class CrawlerController : BaseController
{
    [HttpGet]
    [Route(RouteConstants.CRAWLER_START)]
    public async Task<IActionResult> Start(int id)
    {
        switch (id)
        {
            case 1:

                break;
            default:
                return this.BadRequest(new ResponseModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Invalid crawler!"
                });
        }

        return this.Ok();
    }
}