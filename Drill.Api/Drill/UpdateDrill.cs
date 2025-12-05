using System.ComponentModel.DataAnnotations;

namespace Drill.Api.Drill;

public class UpdateDrill
{
    [Required]
    public int drillId { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [Required]
    public float PricePerMinute { get; set; }
    
    public sealed class UpdateDrillResponse(Payload.Drill.DrillDto drill)
    {
        public Payload.Drill.DrillDto Drill { get; set; } = drill;
    }
}