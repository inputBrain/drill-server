using Drill.Api.Codec;
using Drill.Api.Drill;
using Drill.Api.Payload.Drill;
using Drill.Server.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Drill.Server.Host.Controllers.Client;

public class DrillController : AbstractClientController<DrillController>
{
    public DrillController(ILogger<DrillController> logger, IDatabaseContainer databaseContainer) : base(logger, databaseContainer)
    {
    }


    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CreateDrill.CreateDrillResponse), 200)]
    public async Task<DrillDto> CreateDrill([FromBody] CreateDrill request)
    {
        var drill = await DatabaseContainer.Drill.CreateModel(request.Title, request.PricePerMinute, DateTimeOffset.UtcNow);

        return DrillCodec.EncodeDrill(drill);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(StartDrill.StartDrillResponse), 200)]
    public async Task<StartDrill.StartDrillResponse> StartDrill([FromBody] StartDrill request)
    {
        var drill = await DatabaseContainer.Drill.GetById(request.drillId);
        if (drill == null)
        {
            throw new Exception($"Drill with id {request.drillId} not found");
        }

        await DatabaseContainer.UserDrill.StartDrills(request.UserIds, request.drillId, DateTimeOffset.UtcNow);

        var userDrills = await DatabaseContainer.UserDrill.GetUserDrillsByDrillId(request.drillId);
        var drillDto = DrillCodec.EncodeDrillWithUsers(drill, userDrills);

        return new StartDrill.StartDrillResponse(drillDto);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(StopDrill.StopDrillResponse), 200)]
    public async Task<StopDrill.StopDrillResponse> StopDrill([FromBody] StopDrill request)
    {
        var drill = await DatabaseContainer.Drill.GetById(request.drillId);
        if (drill == null)
        {
            throw new Exception($"Drill with id {request.drillId} not found");
        }

        await DatabaseContainer.UserDrill.StopDrills(request.UserIds, request.drillId, DateTimeOffset.UtcNow);

        var userDrills = await DatabaseContainer.UserDrill.GetUserDrillsByDrillId(request.drillId);
        var drillDto = DrillCodec.EncodeDrillWithUsers(drill, userDrills);

        return new StopDrill.StopDrillResponse(drillDto);
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(List<DrillDto>), 200)]
    public async Task<List<DrillDto>> ListAllDrills()
    {
        var drills = await DatabaseContainer.Drill.ListAll();
        var result = new List<DrillDto>();

        foreach (var drill in drills)
        {
            var userDrills = await DatabaseContainer.UserDrill.GetUserDrillsByDrillId(drill.Id);
            var drillDto = DrillCodec.EncodeDrillWithUsers(drill, userDrills);
            result.Add(drillDto);
        }

        return result;
    }

    [HttpPatch]
    [AllowAnonymous]
    [ProducesResponseType(typeof(UpdateDrill.UpdateDrillResponse), 200)]
    public async Task<UpdateDrill.UpdateDrillResponse> UpdateDrill([FromBody] UpdateDrill request)
    {
        var drill = await DatabaseContainer.Drill.UpdateDrill(request.drillId, request.Title, request.PricePerMinute);

        if (drill == null)
        {
            throw new Exception($"Drill with id {request.drillId} not found");
        }

        return new UpdateDrill.UpdateDrillResponse(DrillCodec.EncodeDrill(drill));
    }

    [HttpDelete]
    [AllowAnonymous]
    [ProducesResponseType(200)]
    public async Task DeleteDrill([FromBody] DeleteDrill request)
    {
        await DatabaseContainer.Drill.DeleteDrill(request.drillId);
    }
}