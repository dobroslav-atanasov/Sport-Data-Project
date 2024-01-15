namespace SportData.Services.Data.OlympicGamesDb;

using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using SportData.Data.Factories.Interfaces;
using SportData.Data.Models.Cache;
using SportData.Services.Data.OlympicGamesDb.Interfaces;

public class EventsService : IEventsService
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly IMapper mapper;

    public EventsService(IDbContextFactory dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<SportData.Data.Models.Entities.OlympicGames.Event> AddOrUpdateAsync(SportData.Data.Models.Entities.OlympicGames.Event @event)
    {
        var context = this.dbContextFactory.CreateOlympicGamesDbContext();

        var dbEvent = await context.Events.FirstOrDefaultAsync(x => x.OriginalName == @event.OriginalName && x.DisciplineId == @event.DisciplineId && x.GameId == @event.GameId);
        if (dbEvent == null)
        {
            await context.AddAsync(@event);
            await context.SaveChangesAsync();
        }
        else
        {
            var isUpdated = dbEvent.IsUpdated(@event);
            if (isUpdated)
            {
                context.Update(dbEvent);
                await context.SaveChangesAsync();
            }

            @event = dbEvent;
        }

        return @event;
    }

    public ICollection<EventCacheModel> GetEventCacheModels()
    {
        var context = this.dbContextFactory.CreateOlympicGamesDbContext();

        var events = context
            .Events
            .AsNoTracking()
            .ToList();

        var models = this.mapper.Map<List<EventCacheModel>>(events);

        return models;
    }
}