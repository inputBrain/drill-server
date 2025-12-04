namespace Drill.Server.Database.Drill;

public interface IDrillRepository
{
    Task<DrillModel> CreateModel(string title, float pricePerMinute, DateTimeOffset createdAt);
}