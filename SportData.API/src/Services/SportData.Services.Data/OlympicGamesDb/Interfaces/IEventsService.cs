namespace SportData.Services.Data.OlympicGamesDb.Interfaces;

using SportData.Data.Models.Cache;
using SportData.Data.Models.Entities.OlympicGames;

public interface IEventsService
{
    Task<Event> AddOrUpdateAsync(Event @event);

    ICollection<EventCacheModel> GetEventCacheModels();
}