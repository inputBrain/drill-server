using System.ComponentModel.DataAnnotations;

namespace Drill.Api.Drill;

public class CreateDrill
{
    [Required]
    public string Title { get; set; }
    
    [Required]
    public float PricePerMinute { get; set; }
    
    
    public sealed class CreateDrillResponse(Payload.Drill.DrillDto drill)
    {
        public Payload.Drill.DrillDto Drill { get; set; } = drill;
    }
}