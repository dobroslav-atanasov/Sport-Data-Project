namespace SportData.Services.Interfaces;

using SportData.Data.Models.Entities.SportData;

public interface IJwtService
{
    string GenerateToken(ApplicationUser user, string role);
}