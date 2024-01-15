namespace SportData.Services.Data.OlympicGamesDb.Interfaces;

using SportData.Data.Models.Entities.OlympicGames;

public interface IEventVenueService
{
    Task<EventVenue> AddOrUpdateAsync(EventVenue eventVenue);
}