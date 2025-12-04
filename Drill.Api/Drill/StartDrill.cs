namespace Drill.Api.Drill;

public class StartDrill
{
    public List<int> UserIds { get; set; }
    
    public int drillId { get; set; }
    
    
    public sealed class StartDrillResponse(Payload.Drill.DrillDto drill)
    {
        public Payload.Drill.DrillDto Drill { get; set; } = drill;
    }
}