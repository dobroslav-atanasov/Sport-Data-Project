namespace SportData.Services.Data.OlympicGamesDb.Interfaces;

using SportData.Data.Models.Entities.OlympicGames;

public interface IResultsService
{
    Task<Result> AddOrUpdateAsync(Result result);
}