using Drill.Server.Database;
using Microsoft.AspNetCore.Mvc;

namespace Drill.Server.Host.Controllers
{
    [ApiController]
    public abstract class AbstractBaseController<T> : ControllerBase
    {
        protected readonly ILogger<T> Logger;

        protected readonly IDatabaseContainer DatabaseContainer;
        
        
        public AbstractBaseController(
            ILogger<T> logger,
            IDatabaseContainer databaseContainer
        )
        {
            Logger = logger;
            DatabaseContainer = databaseContainer;
        }
        

        protected IActionResult SendOk()
        {
            return Ok();
        }


        protected IActionResult SendOk(object response)
        {
            return Ok(response);
        }
    }
}