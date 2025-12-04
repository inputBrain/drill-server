namespace Drill.Server.Database.Drill;

public interface IDrillRepository
{
    Task<DrillModel> CreateModel(string title, float pricePerMinute, DateTimeOffset createdAt);

    Task<List<DrillModel>> ListAll();

    Task<DrillModel?> GetById(int drillId);
}