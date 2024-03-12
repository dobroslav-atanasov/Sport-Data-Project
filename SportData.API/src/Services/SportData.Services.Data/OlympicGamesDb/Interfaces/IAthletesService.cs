namespace SportData.Services.Data.OlympicGamesDb.Interfaces;

using System.Linq.Expressions;

using SportData.Data.Models.Entities.OlympicGames;

public interface IAthletesService
{
    Task<Athlete> GetAsync(Expression<Func<Athlete, bool>> expression);

    Task<Athlete> AddAsync(Athlete athlete);

    Athlete Update(Athlete athlete);

    Task<Club> GetClubAsync(Expression<Func<Club, bool>> expression);

    Task<Club> AddClubAsync(Club club);
}