using Drill.Api.Payload.Drill;
using Drill.Server.Database.Drill;
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
}