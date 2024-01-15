namespace SportData.Services.Data.OlympicGamesDb.Interfaces;

using SportData.Data.Models.Entities.OlympicGames;

public interface ICountriesService
{
    Task<Country> AddOrUpdateAsync(Country country);

    Task<Country> GetAsync(string code);
}