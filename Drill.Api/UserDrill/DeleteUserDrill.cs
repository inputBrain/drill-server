using System.ComponentModel.DataAnnotations;

namespace Drill.Api.UserDrill;

public class DeleteUserDrill
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public int DrillId { get; set; }
}
