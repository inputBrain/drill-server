using Drill.Api.Payload.User;

namespace Drill.Api.Payload.Drill;

public class DrillDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public float PricePerMinute { get; set; }

    public long CreatedAt { get; set; }

    public List<UserDto> Users { get; set; } = new();
} 