using Drill.Api.Codec;
using Drill.Api.Payload.UserDrill;
using Drill.Api.UserDrill;
using Drill.Server.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Drill.Server.Host.Controllers.Client;

public class UserDrillController : AbstractClientController<UserDrillController>
{
    public UserDrillController(ILogger<UserDrillController> logger, IDatabaseContainer databaseContainer) : base(logger, databaseContainer)
    {
    }


    [HttpGet("list")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(List<UserDrillDto>), 200)]
    public async Task<List<UserDrillDto>> ListAll()
    {
        var userDrills = await DatabaseContainer.UserDrill.ListAll();
        return userDrills.Select(UserDrillCodec.EncodeUserDrill).ToList();
    }

    [HttpGet("active")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(List<UserDrillDto>), 200)]
    public async Task<List<UserDrillDto>> GetActive()
    {
        var userDrills = await DatabaseContainer.UserDrill.GetActiveUserDrills();
        return userDrills.Select(UserDrillCodec.EncodeUserDrill).ToList();
    }

    [HttpGet("completed")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(List<UserDrillDto>), 200)]
    public async Task<List<UserDrillDto>> GetCompleted()
    {
        var userDrills = await DatabaseContainer.UserDrill.GetCompletedUserDrills();
        return userDrills.Select(UserDrillCodec.EncodeUserDrill).ToList();
    }

    [HttpDelete]
    [AllowAnonymous]
    [ProducesResponseType(200)]
    public async Task DeleteUserDrill([FromBody] DeleteUserDrill request)
    {
        await DatabaseContainer.UserDrill.DeleteUserDrill(request.UserId, request.DrillId);
    }
}
