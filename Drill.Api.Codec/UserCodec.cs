using Drill.Api.Payload.User;
using Drill.Server.Database.User;
using Drill.Utils;

namespace Drill.Api.Codec;

public static class UserCodec
{
    public static UserDto EncodeUser(UserModel model)
    {
        return new UserDto
        {
            Id = model.Id,
            Email = model.Email ?? "",
            FirstName = model.FirstName,
            LastName = model.LastName,
            CreatedAt = Timestamp.ToUnixTime(model.CreatedAt),
            // Statistics = model.Statistics.Select(EncodeUserStatistic).ToList()
        };
    }
}