using DigitalDetoxApi.Data;

namespace DigitalDetoxApi.Token.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(AppUser appUser);
}