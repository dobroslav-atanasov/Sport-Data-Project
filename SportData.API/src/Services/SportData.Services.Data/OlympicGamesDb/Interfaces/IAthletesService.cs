namespace SportData.Services.Data.OlympicGamesDb.Interfaces;

using SportData.Data.Models.Entities.OlympicGames;

public interface IAthletesService
{
    Task<Athlete> AddOrUpdateAsync(Athlete athlete);

    Task<Athlete> GetAsync(int number);
}