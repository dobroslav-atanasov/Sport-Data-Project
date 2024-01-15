namespace SportData.Services.Data.OlympicGamesDb;

using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using SportData.Data.Factories.Interfaces;
using SportData.Data.Models.Cache;
using SportData.Data.Models.Entities.OlympicGames;
using SportData.Services.Data.OlympicGamesDb.Interfaces;

public class CitiesService : ICitiesService
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly IMapper mapper;

    public CitiesService(IDbContextFactory dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<City> AddOrUpdateAsync(City city)
    {
        using var context = dbContextFactory.CreateOlympicGamesDbContext();

        var dbCity = await context.Cities.FirstOrDefaultAsync(x => x.Name == city.Name);
        if (dbCity == null)
        {
            await context.AddAsync(city);
            await context.SaveChangesAsync();
        }
        else
        {
            var isUpdated = dbCity.IsUpdated(city);
            if (isUpdated)
            {
                context.Update(dbCity);
                await context.SaveChangesAsync();
            }

            city = dbCity;
        }

        return city;
    }

    public async Task<City> GetAsync(string name)
    {
        using var context = dbContextFactory.CreateOlympicGamesDbContext();
        var city = await context.Cities.FirstOrDefaultAsync(x => x.Name == name);
        return city;
    }

    public ICollection<CityCacheModel> GetCityCacheModels()
    {
        using var context = dbContextFactory.CreateOlympicGamesDbContext();

        var cities = context
            .Cities
            .AsNoTracking()
            .ToList();

        var models = this.mapper.Map<List<CityCacheModel>>(cities);

        return models;
    }
}