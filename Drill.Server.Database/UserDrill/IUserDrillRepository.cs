namespace Drill.Server.Database.UserDrill;

public interface IUserDrillRepository
{
    Task<UserDrillModel> StartDrill(int userId, int drillId, DateTimeOffset startedAt);

    Task StoppedDrill(UserDrillModel model, DateTimeOffset stoppedAt);
    
    Task<UserDrillModel> GetDrillByUserId(int userId);
}