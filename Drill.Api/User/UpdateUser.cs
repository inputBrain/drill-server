using System.ComponentModel.DataAnnotations;

namespace Drill.Api.User;

public sealed class UpdateUser
{
    [Required]
    public int UserId { get; set; }
    
    public string? Email { get; set; }
    
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }
    
    
    public sealed class UpdateUserResponse(Payload.User.UserDto user)
    {
        public Payload.User.UserDto User { get; set; } = user;
    }
}