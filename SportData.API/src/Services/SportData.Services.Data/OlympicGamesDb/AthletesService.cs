namespace SportData.Services.Data.OlympicGamesDb;

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

    public async Task<Athlete> AddOrUpdateAsync(Athlete athlete)
    {
        using var context = dbContextFactory.CreateOlympicGamesDbContext();

        var dbAthlete = await context.Athletes.FirstOrDefaultAsync(x => x.Number == athlete.Number);
        if (dbAthlete == null)
        {
            await context.AddAsync(athlete);
            await context.SaveChangesAsync();
        }
        else
        {
            var isUpdated = dbAthlete.IsUpdated(athlete);
            if (isUpdated)
            {
                context.Update(dbAthlete);
                await context.SaveChangesAsync();
            }

            athlete = dbAthlete;
        }

        return athlete;
    }

    public async Task<Athlete> GetAsync(int number)
    {
        using var context = dbContextFactory.CreateOlympicGamesDbContext();
        var athlete = await context.Athletes.FirstOrDefaultAsync(x => x.Number == number);
        return athlete;
    }
}