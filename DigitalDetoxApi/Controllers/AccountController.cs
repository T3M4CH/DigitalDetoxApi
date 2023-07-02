using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DigitalDetoxApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using DigitalDetoxApi.Data;
using DigitalDetoxApi.DTO;

namespace DigitalDetoxApi.Controllers;

public class AccountController : BaseApiController
{
    public AccountController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }
    
    private readonly UserManager<AppUser> _userManager;
    
    [HttpPost("Register"), AllowAnonymous]
    public async Task<ActionResult> Register(RegisterDto registerDto)
    {
        var user = new AppUser()
        {
            UserName = registerDto.Email,
            EmailConfirmed = false,
        };
        
        // //var usersWithSameEmail = await _userManager.FindByEmailsAsync(user.Email);
        //
        // if (usersWithSameEmail.Any(appUser => appUser.EmailConfirmed))
        // {
        //     return BadRequest("Email already taken");
        // }
        
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        
        foreach (var identityError in result.Errors)
        {
            return BadRequest(identityError.Description);
        }

        if (result.Succeeded)
        {
            return Ok();
        }

        return BadRequest();

    }
}