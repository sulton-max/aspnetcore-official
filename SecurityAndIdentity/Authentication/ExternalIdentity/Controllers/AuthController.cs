using ExternalIdentity.Extensions;
using ExternalIdentity.Models.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ExternalIdentity.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpGet("[action]")]
    public async ValueTask<IActionResult> SignIn([FromQuery] SignInDto model)
    {
        var provider = await HttpContext.GetAuthenticationProviderName(model.Provider);
        return !string.IsNullOrWhiteSpace(provider)
            ? Challenge(new AuthenticationProperties
            {
                RedirectUri = "/api/users/me",
            }, provider)
            : BadRequest();
    }
}