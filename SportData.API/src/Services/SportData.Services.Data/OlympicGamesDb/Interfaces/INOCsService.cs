namespace SportData.Services.Data.OlympicGamesDb.Interfaces;

using SportData.Data.Models.Cache;
using SportData.Data.Models.Entities.OlympicGames;

public interface INOCsService
{
    Task<NOC> AddOrUpdateAsync(NOC noc);

    ICollection<NOCCacheModel> GetNOCCacheModels();
}