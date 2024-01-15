namespace SportData.Services.Data.OlympicGamesDb.Interfaces;

using SportData.Data.Models.Cache;
using SportData.Data.Models.Entities.OlympicGames;

public interface IDisciplinesService
{
    Task<Discipline> AddOrUpdateAsync(Discipline discipline);

    ICollection<DisciplineCacheModel> GetDisciplineCacheModels();
}