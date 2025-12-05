using Microsoft.EntityFrameworkCore;
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

    public async Task<List<UserModel>> ListAll()
    {
        return await DbModel
            .ToListAsync();
    }

    public async Task<UserModel?> GetById(int userId)
    {
        return await DbModel
            .FirstOrDefaultAsync(x => x.Id == userId);
    }

    public async Task<UserModel?> UpdateUser(int userId, string? email, string firstName, string lastName)
    {
        var model = await GetById(userId);
        if (model == null)
        {
            return null;
        }

        if (model.IsSame(email, firstName, lastName))
        {
            return model;
        }

        model.Email = email;
        model.FirstName = firstName;
        model.LastName = lastName;

        await UpdateModelAsync(model);

        return model;
    }

    public async Task DeleteUser(int userId)
    {
        var model = await GetById(userId);
        if (model == null)
        {
            throw new Exception($"User with id {userId} not found");
        }

        await DeleteModel(model);
    }
}