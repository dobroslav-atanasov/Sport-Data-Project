namespace SportData.Services.Data.OlympicGamesDb.Interfaces;

using SportData.Data.Models.Entities.OlympicGames;

public interface ISportsService
{
    Task<Sport> AddOrUpdateAsync(Sport sport);
}