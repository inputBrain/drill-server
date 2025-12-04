using Microsoft.Extensions.Logging;

namespace Drill.Server.Database.Drill;

public class DrillRepository(PostgreSqlContext context, ILoggerFactory loggerFactory) : AbstractRepository<DrillModel>(context, loggerFactory), IDrillRepository
{
    public async Task<DrillModel> CreateModel(string title, float pricePerMinute, DateTimeOffset createdAt)
    {
        var model = DrillModel.CreateModel(title, pricePerMinute, createdAt);

        var result = await CreateModelAsync(model);
        if (result == null)
        {
            throw new Exception("DrillModel is not created");
        }

        return result;
    }
}