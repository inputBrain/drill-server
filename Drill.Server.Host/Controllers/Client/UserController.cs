using Drill.Api.Codec;
using Drill.Api.Payload.User;
using Drill.Api.User;
using Drill.Server.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Drill.Server.Host.Controllers.Client;

public class UserController : AbstractClientController<UserController>
{
    public UserController(ILogger<UserController> logger , IDatabaseContainer databaseContainer) : base(logger, databaseContainer)
    {
    }


    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CreateUser.CreateUserResponse), 200)]
    public async Task<UserDto> CreateUser([FromBody] CreateUser request)
    {
        var user = await DatabaseContainer.User.CreateModel(request.Email, request.FirstName,request.LastName, DateTimeOffset.UtcNow);

        return UserCodec.EncodeUser(user);
    }

    [HttpGet("list")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(List<UserDto>), 200)]
    public async Task<List<UserDto>> ListAllUsers()
    {
        var users = await DatabaseContainer.User.ListAll();
        return users.Select(UserCodec.EncodeUser).ToList();
    }

    [HttpPatch]
    [AllowAnonymous]
    [ProducesResponseType(typeof(UpdateUser.UpdateUserResponse), 200)]
    public async Task<UpdateUser.UpdateUserResponse> UpdateUser([FromBody] UpdateUser request)
    {
        var user = await DatabaseContainer.User.UpdateUser(request.UserId, request.Email, request.FirstName, request.LastName);

        if (user == null)
        {
            throw new Exception($"User with id {request.UserId} not found");
        }

        return new UpdateUser.UpdateUserResponse(UserCodec.EncodeUser(user));
    }

    [HttpDelete]
    [AllowAnonymous]
    [ProducesResponseType(200)]
    public async Task DeleteUser([FromBody] DeleteUser request)
    {
        await DatabaseContainer.User.DeleteUser(request.UserId);
    }
}