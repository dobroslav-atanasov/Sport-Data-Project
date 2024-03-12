namespace SportData.Services.Data.OlympicGamesDb;

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using SportData.Data.Factories.Interfaces;
using SportData.Data.Models.Entities.OlympicGames;
using SportData.Services.Data.OlympicGamesDb.Interfaces;

public class EventsService : IEventsService
{
    private readonly IDbContextFactory dbContextFactory;

    public EventsService(IDbContextFactory dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<Event> AddAsync(Event @event)
    {
        using var context = this.dbContextFactory.CreateOlympicGamesDbContext();

        await context.AddAsync(@event);
        await context.SaveChangesAsync();

        return @event;
    }

    public async Task<Event> GetAsync(Expression<Func<Event, bool>> expression)
    {
        using var context = this.dbContextFactory.CreateOlympicGamesDbContext();

        var @event = await context.Events
            .Where(expression)
            .FirstOrDefaultAsync();

        return @event;
    }

    public Event Update(Event @event)
    {
        using var context = this.dbContextFactory.CreateOlympicGamesDbContext();

        var entry = context.Entry(@event);
        if (entry.State == EntityState.Detached)
        {
            context.Attach(@event);
        }

        entry.State = EntityState.Modified;

        context.SaveChanges();

        return @event;
    }
}