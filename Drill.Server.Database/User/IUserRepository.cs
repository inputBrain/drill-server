namespace Drill.Server.Database.User;

public interface IUserRepository
{
    Task<UserModel> CreateModel(
        string? email,
        string firstName,
        string lastName,
        DateTimeOffset createdAt
    );
}