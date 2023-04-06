using System.Security.Claims;
using ExternalIdentity.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExternalIdentity.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    [HttpGet("[action]")]
    public ValueTask<IActionResult> Me()
    {
        var user = HttpContext.User;
        var identity = new
        {
            UserId = user.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier))?.Value,
            Email = user.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email))?.Value
        };

        return new ValueTask<IActionResult>(Ok(identity));
    }
}