using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DigitalDetoxApi.Extensions;

public static class UserManagerExtensions
{
    public static async Task<IEnumerable<TUser>> FindByEmailsAsync<TUser>(this UserManager<TUser> userManager, string email)
        where TUser : IdentityUser
    {
        var users = await userManager.Users.Where(user => user.Email == email).ToArrayAsync();

        return users;
    }

    public static async Task<IEnumerable<TUser>> FindUnconfirmedEmailsAsync<TUser>(this UserManager<TUser> userManager)
        where TUser : IdentityUser
    {
        return await userManager.Users.Where(user => !user.EmailConfirmed).ToArrayAsync();
    }

    public static async Task<TUser?> FindByConfirmedEmailAsync<TUser>(this UserManager<TUser> userManager, string email)
        where TUser : IdentityUser
    {
        var users = await userManager.FindByEmailsAsync(email);

        return users.FirstOrDefault(user => user.EmailConfirmed);
    }
}