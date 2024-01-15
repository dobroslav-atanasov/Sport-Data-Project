namespace SportData.Services.Data.OlympicGamesDb;

using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using SportData.Data.Factories.Interfaces;
using SportData.Data.Models.Cache;
using SportData.Data.Models.Entities.OlympicGames;
using SportData.Services.Data.OlympicGamesDb.Interfaces;

public class DisciplinesService : IDisciplinesService
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly IMapper mapper;

    public DisciplinesService(IDbContextFactory dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<Discipline> AddOrUpdateAsync(Discipline discipline)
    {
        var context = this.dbContextFactory.CreateOlympicGamesDbContext();

        var dbDiscipline = await context.Disciplines.FirstOrDefaultAsync(x => x.Name == discipline.Name);
        if (dbDiscipline == null)
        {
            await context.AddAsync(discipline);
            await context.SaveChangesAsync();
        }
        else
        {
            var isUpdated = dbDiscipline.IsUpdated(discipline);
            if (isUpdated)
            {
                context.Update(dbDiscipline);
                await context.SaveChangesAsync();
            }

            discipline = dbDiscipline;
        }

        return discipline;
    }

    public ICollection<DisciplineCacheModel> GetDisciplineCacheModels()
    {
        var context = this.dbContextFactory.CreateOlympicGamesDbContext();

        var disciplines = context
            .Disciplines
            .AsNoTracking()
            .ToList();

        var models = this.mapper.Map<List<DisciplineCacheModel>>(disciplines);

        return models;
    }
}