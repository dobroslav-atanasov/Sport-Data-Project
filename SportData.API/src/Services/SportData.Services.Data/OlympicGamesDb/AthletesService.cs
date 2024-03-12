namespace SportData.Services.Data.OlympicGamesDb;

using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using SportData.Data.Factories.Interfaces;
using SportData.Data.Models.Entities.OlympicGames;
using SportData.Services.Data.OlympicGamesDb.Interfaces;

public class AthletesService : IAthletesService
{
    private readonly IDbContextFactory dbContextFactory;

    public AthletesService(IDbContextFactory dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<Athlete> AddAsync(Athlete athlete)
    {
        using var context = this.dbContextFactory.CreateOlympicGamesDbContext();

        await context.AddAsync(athlete);
        await context.SaveChangesAsync();

        return athlete;
    }

    public async Task<Club> AddClubAsync(Club club)
    {
        using var context = this.dbContextFactory.CreateOlympicGamesDbContext();

        await context.AddAsync(club);
        await context.SaveChangesAsync();

        return club;
    }

    public async Task<Athlete> GetAsync(Expression<Func<Athlete, bool>> expression)
    {
        using var context = this.dbContextFactory.CreateOlympicGamesDbContext();

        var athlete = await context.Athletes
            .Where(expression)
            .FirstOrDefaultAsync();

        return athlete;
    }

    public async Task<Club> GetClubAsync(Expression<Func<Club, bool>> expression)
    {
        using var context = this.dbContextFactory.CreateOlympicGamesDbContext();

        var club = await context.Clubs
            .Where(expression)
            .FirstOrDefaultAsync();

        return club;
    }

    public Athlete Update(Athlete athlete)
    {
        using var context = this.dbContextFactory.CreateOlympicGamesDbContext();

        var entry = context.Entry(athlete);
        if (entry.State == EntityState.Detached)
        {
            context.Attach(athlete);
        }

        entry.State = EntityState.Modified;

        context.SaveChanges();

        return athlete;
    }
}