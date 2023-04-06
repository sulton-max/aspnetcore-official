using IdentityAuth.Models.Dtos;
using IdentityAuth.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAuth.Controllers;

public class AuthController : CustomControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpDto model)
    {
        var result = await _authenticationService.SignUpAsync(model);
        return Ok(result);
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInDto model)
    {
        var result = await _authenticationService.SignInAsync(model);
        return Ok(result);
    }

    [HttpPost("signout")]
    public new async Task<IActionResult> SignOut()
    {
        await _authenticationService.SignOutAsync();
        return Ok();
    }
}