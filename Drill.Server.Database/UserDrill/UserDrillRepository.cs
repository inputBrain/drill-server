using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Drill.Server.Database.UserDrill;

public class UserDrillRepository(PostgreSqlContext context, ILoggerFactory loggerFactory) : AbstractRepository<UserDrillModel>(context, loggerFactory), IUserDrillRepository
{
    public async Task<UserDrillModel> StartDrill(int userId, int drillId, DateTimeOffset startedAt)
    {
        var model = UserDrillModel.StartDrill(userId, drillId, startedAt);

        var result = await CreateModelAsync(model);
        if (result == null)
        {
            throw new Exception("UserDrillModel is not created");
        }

        return result;
    }

    public async Task<List<UserDrillModel>> StartDrills(List<int> userIds, int drillId, DateTimeOffset startedAt)
    {
        var models = new List<UserDrillModel>();

        foreach (var userId in userIds)
        {
            var model = await StartDrill(userId, drillId, startedAt);
            models.Add(model);
        }

        return models;
    }


    public async Task StoppedDrill(UserDrillModel model, DateTimeOffset stoppedAt)
    {
        model.StopDrill(model, stoppedAt);
        await UpdateModelAsync(model);
    }

    public async Task StopDrills(List<int> userIds, int drillId, DateTimeOffset stoppedAt)
    {
        foreach (var userId in userIds)
        {
            var model = await DbModel
                .Where(x => x.UserId == userId && x.DrillId == drillId && x.StoppedAt == null)
                .FirstOrDefaultAsync();

            if (model != null)
            {
                await StoppedDrill(model, stoppedAt);
            }
        }
    }


    public async Task<UserDrillModel> GetDrillByUserId(int userId)
    {
        var model = await DbModel
            .Include(x => x.Drill)
            .Include(x => x.User)
            .Where(x => x.UserId == userId)
            .FirstOrDefaultAsync();

        if (model == null)
        {
            throw new Exception("UserDrillModel not found in db");
        }

        return model;
    }

    public async Task<List<UserDrillModel>> GetUserDrillsByDrillId(int drillId)
    {
        return await DbModel
            .Include(x => x.User)
            .Include(x => x.Drill)
            .Where(x => x.DrillId == drillId)
            .ToListAsync();
    }

    public async Task<List<UserDrillModel>> GetActiveUserDrills()
    {
        return await DbModel
            .Include(x => x.User)
            .Include(x => x.Drill)
            .Where(x => x.StoppedAt == null)
            .ToListAsync();
    }

    public async Task<List<UserDrillModel>> GetCompletedUserDrills()
    {
        return await DbModel
            .Include(x => x.User)
            .Include(x => x.Drill)
            .Where(x => x.StoppedAt != null)
            .ToListAsync();
    }

    public async Task<List<UserDrillModel>> ListAll()
    {
        return await DbModel
            .Include(x => x.User)
            .Include(x => x.Drill)
            .ToListAsync();
    }

    public async Task DeleteUserDrill(int userId, int drillId)
    {
        var model = await DbModel
            .Where(x => x.UserId == userId && x.DrillId == drillId)
            .FirstOrDefaultAsync();

        if (model == null)
        {
            throw new Exception($"UserDrill with userId {userId} and drillId {drillId} not found");
        }

        await DeleteModel(model);
    }
}