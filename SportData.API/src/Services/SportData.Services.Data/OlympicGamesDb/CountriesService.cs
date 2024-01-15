namespace SportData.Services.Data.OlympicGamesDb;

using Microsoft.EntityFrameworkCore;

using SportData.Data.Factories.Interfaces;
using SportData.Data.Models.Entities.OlympicGames;
using SportData.Services.Data.OlympicGamesDb.Interfaces;

public class CountriesService : ICountriesService
{
    private readonly IDbContextFactory dbContextFactory;

    public CountriesService(IDbContextFactory dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<Country> AddOrUpdateAsync(Country country)
    {
        var context = this.dbContextFactory.CreateOlympicGamesDbContext();

        var dbCountry = await context.Countries.FirstOrDefaultAsync(x => x.Code == country.Code);
        if (dbCountry == null)
        {
            await context.AddAsync(country);
            await context.SaveChangesAsync();
        }
        else
        {
            var isUpdated = dbCountry.IsUpdated(country);
            if (isUpdated)
            {
                context.Update(dbCountry);
                await context.SaveChangesAsync();
            }

            country = dbCountry;
        }

        return country;
    }

    public async Task<Country> GetAsync(string code)
    {
        var context = this.dbContextFactory.CreateOlympicGamesDbContext();
        var country = await context.Countries.FirstOrDefaultAsync(x => x.Code == code);
        return country;
    }
}