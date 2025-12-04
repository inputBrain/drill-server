using Microsoft.Extensions.Logging;

namespace Drill.Server.Database.User;

public class UserRepository(PostgreSqlContext context, ILoggerFactory loggerFactory) : AbstractRepository<UserModel>(context, loggerFactory), IUserRepository
{
    public async Task<UserModel> CreateModel(
        string? email,
        string firstName,
        string lastName,
        DateTimeOffset createdAt
    )
    {
        var model = UserModel.CreateModel(email, firstName, lastName, createdAt);

        var result = await CreateModelAsync(model);
        if (result == null)
        {
            throw new Exception("UserModel is not created");
        }

        return result;
    }
}