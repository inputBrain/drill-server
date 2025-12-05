namespace Drill.Server.Database.UserDrill;

public interface IUserDrillRepository
{
    Task<UserDrillModel> StartDrill(int userId, int drillId, DateTimeOffset startedAt);

    Task<List<UserDrillModel>> StartDrills(List<int> userIds, int drillId, DateTimeOffset startedAt);

    Task StoppedDrill(UserDrillModel model, DateTimeOffset stoppedAt);

    Task StopDrills(List<int> userIds, int drillId, DateTimeOffset stoppedAt);

    Task<UserDrillModel> GetDrillByUserId(int userId);

    Task<List<UserDrillModel>> GetUserDrillsByDrillId(int drillId);

    Task<List<UserDrillModel>> GetActiveUserDrills();

    Task<List<UserDrillModel>> GetCompletedUserDrills();

    Task<List<UserDrillModel>> ListAll();

    Task DeleteUserDrill(int userId, int drillId);
}