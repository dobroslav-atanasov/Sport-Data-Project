namespace SportData.Services;

using SportData.Data.Models.Jwt;
using SportData.Services.Interfaces;

public class UserService : IUserService
{
    private readonly List<User> users;

    public UserService()
    {
        this.users = new List<User>
        {
            new() { Id = Guid.NewGuid(), Username = "user1", Password = BCrypt.Net.BCrypt.HashPassword("password1"), Email = "user1@test.com",  Role = "Admin"},
            new() { Id = Guid.NewGuid(), Username = "user2", Password = BCrypt.Net.BCrypt.HashPassword("password2"), Email = "user2@test.com",  Role = "User"},
        };
    }

    public User GetUser(string username)
    {
        return this.users.FirstOrDefault(x => x.Username == username);
    }

    public bool IsAthenticated(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}