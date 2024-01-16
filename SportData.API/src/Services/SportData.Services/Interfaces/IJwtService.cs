namespace SportData.Services.Interfaces;

using SportData.Data.Models.Jwt;

public interface IJwtService
{
    string GenerateToken(User user);
}