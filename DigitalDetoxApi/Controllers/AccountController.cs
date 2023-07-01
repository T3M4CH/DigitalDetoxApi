using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalDetoxApi.Controllers;

public class AccountController : BaseApiController
{
    [HttpPost("Register"), AllowAnonymous]
    public async Task<ActionResult> Register()
    {
        // var user = new AppUser
        // {
        //     Name = registerDto.Email,
        //     Email = registerDto.Email,
        //     UserName = registerDto.UserName,
        //     EmailConfirmed = false,
        // };
        //
        // var usersWithSameEmail = await _userManager.FindByEmailsAsync(user.Email);
        //
        // if (usersWithSameEmail.Any(appUser => appUser.EmailConfirmed))
        // {
        //     return BadRequest("Email already taken");
        // }
        //
        // var result = await _userManager.CreateAsync(user, registerDto.Password);
        //
        // foreach (var identityError in result.Errors)
        // {
        //     Console.WriteLine(identityError);
        //
        // }

        return BadRequest();

    }
}