namespace SportData.Services.Data.OlympicGamesDb.Interfaces;

using SportData.Data.Models.Cache;
using SportData.Data.Models.Entities.OlympicGames;

public interface ICitiesService
{
    Task<City> AddOrUpdateAsync(City city);

    Task<City> GetAsync(string name);

    ICollection<CityCacheModel> GetCityCacheModels();
}