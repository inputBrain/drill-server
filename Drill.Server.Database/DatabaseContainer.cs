using Drill.Server.Database.Drill;
using Drill.Server.Database.User;
using Drill.Server.Database.UserDrill;
using Microsoft.Extensions.Logging;

namespace Drill.Server.Database;

public class DatabaseContainer : IDatabaseContainer
{
    public IUserRepository User { get; }

    public IDrillRepository Drill { get; set; }

    public IUserDrillRepository UserDrill { get; set; }


    public DatabaseContainer(PostgreSqlContext context, ILoggerFactory loggerFactory)
    {
        User = new UserRepository(context, loggerFactory);
        Drill = new DrillRepository(context, loggerFactory);
        UserDrill = new UserDrillRepository(context, loggerFactory);
    }
}