using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DigitalDetoxApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using DigitalDetoxApi.Data;
using DigitalDetoxApi.DTO;
using DigitalDetoxApi.Token.Interfaces;

namespace DigitalDetoxApi.Controllers;

public class AccountController : BaseApiController
{
    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;

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

    [AllowAnonymous, HttpPost("Login")]
    public async Task<ActionResult> Login(RegisterDto registerDto)
    {
        var user = await _userManager.FindByNameAsync(registerDto.Email);

        if (user != null)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, registerDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Password Wrong");

            var token = await _tokenService.CreateToken(user);

            return Ok(new { Token = token });
        }

        return BadRequest("User not found");
    }

    [Authorize, HttpGet("GetBalance")]
    public async Task<ActionResult> GetBalance()
    {
        var user = await _userManager.FindByNameAsync(User.GetUsername());

        if (user != null)
        {
            return Ok(user.Balance);
        }

        return BadRequest("Пользователь не найден");
    }

    [Authorize, HttpGet("Pay")]
    public async Task<ActionResult> Pay(int value)
    {
        var user = await _userManager.FindByNameAsync(User.GetUsername());

        if (user != null)
        {
            if (user.Balance >= value)
            {
                user.Balance -= value;
                return Ok(user.Balance);
            }

            return BadRequest("Недостаточно средств");
        }

        return BadRequest("Пользователь не найден");
    }
}