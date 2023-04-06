using IdentityAuth.Models.Dtos;

namespace IdentityAuth.Services.Interfaces;

public interface IAuthenticationService
{
    ValueTask<bool> SignUpAsync(SignUpDto signUpDetails);
    ValueTask<bool> SignInAsync(SignInDto signInDetails);
    ValueTask SignOutAsync();
}