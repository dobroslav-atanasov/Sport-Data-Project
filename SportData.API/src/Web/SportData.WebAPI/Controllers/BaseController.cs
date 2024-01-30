namespace SportData.WebAPI.Controllers;

using Microsoft.AspNetCore.Mvc;

using SportData.Common.Constants;

[ApiController]
[Route(RouteConstants.API_DEFAULT_ROUTE)]
public abstract class BaseController : ControllerBase
{
}