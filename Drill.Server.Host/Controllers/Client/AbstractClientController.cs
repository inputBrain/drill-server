using Drill.Server.Database;
using Microsoft.AspNetCore.Mvc;

namespace Drill.Server.Host.Controllers.Client
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Produces("application/json")]
    [Route("[controller]/[action]")]
    public abstract class AbstractClientController<T> : AbstractBaseController<T>
    {
        protected AbstractClientController(ILogger<T> logger, IDatabaseContainer databaseContainer) : base(logger, databaseContainer)
        {
        }
    }
}