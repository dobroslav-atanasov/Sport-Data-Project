namespace SportData.Services.Data.OlympicGamesDb.Interfaces;

using SportData.Data.Models.Entities.OlympicGames;

public interface IHostsService
{
    Task<Host> AddOrUpdateAsync(Host host);
}
