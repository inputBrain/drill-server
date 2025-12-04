using Drill.Api.Payload.Drill;
using Drill.Server.Database.Drill;
using Drill.Server.Database.UserDrill;
using Drill.Utils;

namespace Drill.Api.Codec;

public static class DrillCodec
{
    public static DrillDto EncodeDrill(DrillModel model)
    {
        return new DrillDto
        {
            Id = model.Id,
            Title = model.Title,
            PricePerMinute = model.PricePerMinute,
            CreatedAt = Timestamp.ToUnixTime(model.CreatedAt)
        };
    }

    public static DrillDto EncodeDrillWithUsers(DrillModel model, List<UserDrillModel> userDrills)
    {
        return new DrillDto
        {
            Id = model.Id,
            Title = model.Title,
            PricePerMinute = model.PricePerMinute,
            CreatedAt = Timestamp.ToUnixTime(model.CreatedAt),
            Users = userDrills
                .Where(ud => ud.StoppedAt == null)
                .Select(ud => UserCodec.EncodeUser(ud.User))
                .ToList()
        };
    }
}