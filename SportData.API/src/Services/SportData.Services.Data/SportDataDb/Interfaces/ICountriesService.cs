namespace SportData.Services.Data.SportDataDb.Interfaces;

using SportData.Data.Models.Entities.SportData;

public interface ICountriesService
{
    Task<Country> AddOrUpdateAsync(Country country);

    Task<Country> GetAsync(string code);
}