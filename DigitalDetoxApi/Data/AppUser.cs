using Microsoft.AspNetCore.Identity;

namespace DigitalDetoxApi.Data;

public class AppUser : IdentityUser
{
    public int Balance { get; set; }
}