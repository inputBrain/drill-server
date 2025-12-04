using Drill.Server.Database.User;
using Microsoft.Extensions.Logging;

namespace Drill.Server.Database;

public class DatabaseContainer : IDatabaseContainer
{
    public IUserRepository User { get; }



    public DatabaseContainer(PostgreSqlContext context, ILoggerFactory loggerFactory)
    {
        User = new UserRepository(context, loggerFactory);
    }
}