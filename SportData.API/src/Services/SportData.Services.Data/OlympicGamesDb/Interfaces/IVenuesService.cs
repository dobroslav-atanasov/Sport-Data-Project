namespace SportData.Services.Data.OlympicGamesDb.Interfaces;

using SportData.Data.Models.Cache;
using SportData.Data.Models.Entities.OlympicGames;

public interface IVenuesService
{
    Task<Venue> AddOrUpdateAsync(Venue venue);

    ICollection<VenueCacheModel> GetVenueCacheModels();
}