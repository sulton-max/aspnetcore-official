using IdentityAuth.Models.Dtos;
using IdentityAuth.Models.Entities;
using IdentityAuth.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace IdentityAuth.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AuthenticationService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async ValueTask<bool> SignUpAsync(SignUpDto signUpDetails)
    {
        var user = new User
        {
            UserName = signUpDetails.EmailAddress,
            Email = signUpDetails.EmailAddress,
        };

        var result = await _userManager.CreateAsync(user, signUpDetails.Password);
        return result.Succeeded;
    }

    public async ValueTask<bool> SignInAsync(SignInDto signInDetails)
    {
        var result = await _signInManager.PasswordSignInAsync(signInDetails.EmailAddress, signInDetails.Password, true, false);
        return result.Succeeded;
    }

    public async ValueTask SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}