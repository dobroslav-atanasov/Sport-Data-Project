namespace SportData.Services.Data.OlympicGamesDb;

using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using SportData.Data.Factories.Interfaces;
using SportData.Data.Models.Entities.OlympicGames;
using SportData.Services.Data.OlympicGamesDb.Interfaces;

public class EventVenueService : IEventVenueService
{
    private readonly IDbContextFactory dbContextFactory;

    public EventVenueService(IDbContextFactory dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<EventVenue> AddOrUpdateAsync(EventVenue eventVenue)
    {
        var context = this.dbContextFactory.CreateOlympicGamesDbContext();

        var dbEventVenue = await context.EventVenues.FirstOrDefaultAsync(x => x.EventId == eventVenue.EventId && x.VenueId == eventVenue.VenueId);
        if (dbEventVenue == null)
        {
            await context.AddAsync(eventVenue);
            await context.SaveChangesAsync();
        }
        else
        {
            eventVenue = dbEventVenue;
        }

        return eventVenue;
    }
}