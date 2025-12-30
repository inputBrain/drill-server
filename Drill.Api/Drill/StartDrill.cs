using System.ComponentModel.DataAnnotations;

namespace Drill.Api.Drill;

public class StartDrill
{
    [Required]
    public List<int> UserIds { get; set; }
    
    [Required]
    public int drillId { get; set; }
    
    
    public sealed class StartDrillResponse(Payload.Drill.DrillDto drill)
    {
        public Payload.Drill.DrillDto Drill { get; set; } = drill;
    }
}