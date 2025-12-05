namespace Drill.Server.Database.User;

public interface IUserRepository
{
    Task<UserModel> CreateModel(
        string? email,
        string firstName,
        string lastName,
        DateTimeOffset createdAt
    );

    Task<List<UserModel>> ListAll();

    Task<UserModel?> GetById(int userId);

    Task<UserModel?> UpdateUser(int userId, string? email, string firstName, string lastName);

    Task DeleteUser(int userId);
}