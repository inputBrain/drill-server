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


    public async Task StoppedDrill(UserDrillModel model, DateTimeOffset stoppedAt)
    {
        model.StopDrill(model, stoppedAt);
        await UpdateModelAsync(model);
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
}