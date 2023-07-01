using DigitalDetoxApi.DTO;
using DigitalDetoxApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DigitalDetoxApi.Controllers;

public class AccountController : BaseApiController
{
    private AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    
    [HttpPost("Register"), AllowAnonymous]
    public async Task<ActionResult> Register(RegisterDto registerDto)
    {
        var user = new IdentityUser()
        {
            Email = registerDto.Email,
            EmailConfirmed = false,
        };
        
        var usersWithSameEmail = await _userManager.FindByEmailsAsync(user.Email);
        
        if (usersWithSameEmail.Any(appUser => appUser.EmailConfirmed))
        {
            return BadRequest("Email already taken");
        }
        
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        
        foreach (var identityError in result.Errors)
        {
            Console.WriteLine(identityError);
        }

        return BadRequest();

    }
}