using System.ComponentModel.DataAnnotations;
using Drill.Api.Payload.User;

namespace Drill.Api.Payload.Drill;

public class DrillDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public float PricePerMinute { get; set; }

    [Required]
    public long CreatedAt { get; set; }

    [Required]
    public List<UserDto> Users { get; set; } = new();
} 