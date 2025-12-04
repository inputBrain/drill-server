namespace Drill.Api.User;

public sealed class CreateUser
{
    public string? Email { get; set; }
    
    public string FirstName { get; set; }

    public string LastName { get; set; }
    
    
    public sealed class CreateUserResponse(Payload.User.UserDto user)
    {
        public Payload.User.UserDto User { get; set; } = user;
    }
}