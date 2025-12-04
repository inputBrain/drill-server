using Drill.Server.Database.Drill;
using Drill.Server.Database.User;
using Drill.Server.Database.UserDrill;

namespace Drill.Server.Database;

public interface IDatabaseContainer
{
    IUserRepository User { get; }
    IDrillRepository Drill { get; }
    IUserDrillRepository UserDrill { get; }
}