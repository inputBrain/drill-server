using System.ComponentModel.DataAnnotations;
using Drill.Api.Payload.Drill;
using Drill.Api.Payload.User;

namespace Drill.Api.Payload.UserDrill;

public class UserDrillDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public UserDto User { get; set; }

    [Required]
    public int DrillId { get; set; }

    [Required]
    public DrillDto Drill { get; set; }

    [Required]
    public long StartedAt { get; set; }

    [Required]
    public long? StoppedAt { get; set; }
}
