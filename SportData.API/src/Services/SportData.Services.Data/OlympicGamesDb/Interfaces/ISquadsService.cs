namespace SportData.Services.Data.OlympicGamesDb.Interfaces;

using SportData.Data.Models.Entities.OlympicGames;

public interface ISquadsService
{
    Task<Squad> AddOrUpdateAsync(Squad squad);
}