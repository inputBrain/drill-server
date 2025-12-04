using Drill.Api.Payload.Drill;
using Drill.Api.Payload.User;

namespace Drill.Api.Payload.UserDrill;

public class UserDrillDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public UserDto User { get; set; }

    public int DrillId { get; set; }

    public DrillDto Drill { get; set; }

    public long StartedAt { get; set; }

    public long? StoppedAt { get; set; }
}
