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
    public async Task<DrillDto> CreateUser([FromBody] CreateDrill request)
    {
        var user = await DatabaseContainer.Drill.CreateModel(request.Title, request.PricePerMinute, DateTimeOffset.UtcNow);

        return DrillCodec.EncodeDrill(user);
    }
}