namespace SportData.Services.Data.OlympicGamesDb.Interfaces;

using SportData.Data.Models.Entities.OlympicGames;

public interface INationalitiesService
{
    Task<Nationality> AddOrUpdateAsync(Nationality nationality);
}