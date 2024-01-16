namespace SportData.Services;

using SportData.Data.Models.Jwt;
using SportData.Services.Interfaces;

public class UserService : IUserService
{
    public User GetUser(string username)
    {
        throw new NotImplementedException();
    }

    public bool IsAthenticated(string password, string passwordHash)
    {
        throw new NotImplementedException();
    }
}