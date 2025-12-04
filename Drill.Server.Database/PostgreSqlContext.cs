using Drill.Server.Database.Drill;
using Drill.Server.Database.User;
using Drill.Server.Database.UserDrill;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Drill.Server.Database;

public sealed class PostgreSqlContext : DbContext
{
    public IDatabaseContainer Db { get; set; }
    
    public DbSet<UserModel> User { get; set; }
    public DbSet<DrillModel> Drill { get; set; }
    public DbSet<UserDrillModel> UserDrill { get; set; }

    
    public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options, ILoggerFactory loggerFactory) : base(options)
    {
        Db = new DatabaseContainer(this, loggerFactory);
    }
}