namespace SportData.Services.Data.OlympicGamesDb.Interfaces;

using System.Linq.Expressions;

using SportData.Data.Models.Entities.OlympicGames;

public interface IEventsService
{
    Task<Event> AddAsync(Event @event);

    Task<Event> GetAsync(Expression<Func<Event, bool>> expression);

    Event Update(Event @event);
}