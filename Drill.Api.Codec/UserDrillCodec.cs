using Drill.Api.Payload.UserDrill;
using Drill.Server.Database.UserDrill;
using Drill.Utils;

namespace Drill.Api.Codec;

public static class UserDrillCodec
{
    public static UserDrillDto EncodeUserDrill(UserDrillModel model)
    {
        return new UserDrillDto
        {
            Id = model.Id,
            UserId = model.UserId,
            User = UserCodec.EncodeUser(model.User),
            DrillId = model.DrillId,
            Drill = DrillCodec.EncodeDrill(model.Drill),
            StartedAt = Timestamp.ToUnixTime(model.StartedAt),
            StoppedAt = model.StoppedAt.HasValue ? Timestamp.ToUnixTime(model.StoppedAt.Value) : null
        };
    }
}
