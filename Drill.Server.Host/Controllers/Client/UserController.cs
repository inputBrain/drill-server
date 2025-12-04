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
        var user = await DatabaseContainer.User.CreateModel(request.Email, request.FirstName,request.LastName, DateTime.UtcNow);

        return UserCodec.EncodeUser(user);
    }
}