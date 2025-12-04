using Drill.Server.Database.User;

namespace Drill.Server.Database;

public interface IDatabaseContainer
{
    IUserRepository User { get; }
}