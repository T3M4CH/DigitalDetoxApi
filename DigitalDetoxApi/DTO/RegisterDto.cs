using System.ComponentModel.DataAnnotations;

namespace DigitalDetoxApi.DTO;

public class RegisterDto
{
    [EmailAddress, Required, MaxLength(50)]
    public string Email { get; set; }

    public string Password { get; set; }
}