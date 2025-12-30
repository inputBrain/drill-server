using System.ComponentModel.DataAnnotations;

namespace Drill.Api.User;

public sealed class CreateUser
{
    [Required]
    public string? Email { get; set; }
    
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }
    
    
    public sealed class CreateUserResponse(Payload.User.UserDto user)
    {
        public Payload.User.UserDto User { get; set; } = user;
    }
}