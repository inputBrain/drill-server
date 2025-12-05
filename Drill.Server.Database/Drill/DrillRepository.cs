using Microsoft.EntityFrameworkCore;
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

    public async Task<List<DrillModel>> ListAll()
    {
        return await DbModel
            .ToListAsync();
    }

    public async Task<DrillModel?> GetById(int drillId)
    {
        return await DbModel
            .FirstOrDefaultAsync(x => x.Id == drillId);
    }

    public async Task<DrillModel?> UpdateDrill(int drillId, string title, float pricePerMinute)
    {
        var model = await GetById(drillId);
        if (model == null)
        {
            return null;
        }

        if (model.IsSame(title, pricePerMinute))
        {
            return model;
        }

        model.Title = title;
        model.PricePerMinute = pricePerMinute;

        await UpdateModelAsync(model);

        return model;
    }

    public async Task DeleteDrill(int drillId)
    {
        var model = await GetById(drillId);
        if (model == null)
        {
            throw new Exception($"Drill with id {drillId} not found");
        }

        await DeleteModel(model);
    }
}