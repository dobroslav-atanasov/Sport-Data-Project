namespace SportData.Services.Interfaces;

using SportData.Data.Models.Jwt;

public interface IUserService
{
    User GetUser(string username);

    bool IsAthenticated(string password, string passwordHash);
}