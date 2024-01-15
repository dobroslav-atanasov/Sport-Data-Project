namespace SportData.Services.Data.OlympicGamesDb.Interfaces;

using SportData.Data.Models.Cache;
using SportData.Data.Models.Entities.OlympicGames;

public interface IGamesService
{
    Task<Game> AddOrUpdateAsync(Game game);

    ICollection<GameCacheModel> GetGameCacheModels();
}