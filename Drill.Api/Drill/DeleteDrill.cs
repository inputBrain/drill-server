using System.ComponentModel.DataAnnotations;

namespace Drill.Api.Drill;

public class DeleteDrill
{
    [Required]
    public int drillId { get; set; }
}