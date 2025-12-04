namespace Drill.Api.Drill;

public class CreateDrill
{
    public string Title { get; set; }
    
    public float PricePerMinute { get; set; }
    
    
    public sealed class CreateDrillResponse(Payload.Drill.DrillDto drill)
    {
        public Payload.Drill.DrillDto Drill { get; set; } = drill;
    }
}