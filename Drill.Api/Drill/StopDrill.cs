namespace Drill.Api.Drill;

public class StopDrill
{
    public List<int> UserIds { get; set; }
    
    public int drillId { get; set; }
    
    public sealed class StopDrillResponse(Payload.Drill.DrillDto drill)
    {
        public Payload.Drill.DrillDto Drill { get; set; } = drill;
    }
}