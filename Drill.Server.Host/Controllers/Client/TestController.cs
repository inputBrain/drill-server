using Drill.Server.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Drill.Server.Host.Controllers.Client;

public class TestController : AbstractClientController<TestController>
{
    public TestController(ILogger<TestController> logger, IDatabaseContainer databaseContainer) : base(logger, databaseContainer)
    {
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetStatus()
    {
        return SendOk("OK");
    }
}