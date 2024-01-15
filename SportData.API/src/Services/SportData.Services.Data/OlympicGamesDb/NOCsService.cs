namespace SportData.Services.Data.OlympicGamesDb;

using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using SportData.Data.Factories.Interfaces;
using SportData.Data.Models.Cache;
using SportData.Data.Models.Entities.OlympicGames;
using SportData.Services.Data.OlympicGamesDb.Interfaces;

public class NOCsService : INOCsService
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly IMapper mapper;

    public NOCsService(IDbContextFactory dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<NOC> AddOrUpdateAsync(NOC noc)
    {
        using var context = dbContextFactory.CreateOlympicGamesDbContext();

        var dbNoc = await context.NOCs.FirstOrDefaultAsync(x => x.Code == noc.Code);
        if (dbNoc == null)
        {
            await context.AddAsync(noc);
            await context.SaveChangesAsync();
        }
        else
        {
            var isUpdated = dbNoc.IsUpdated(noc);
            if (isUpdated)
            {
                context.Update(dbNoc);
                await context.SaveChangesAsync();
            }

            noc = dbNoc;
        }

        return noc;
    }

    public ICollection<NOCCacheModel> GetNOCCacheModels()
    {
        using var context = dbContextFactory.CreateOlympicGamesDbContext();

        var nocs = context
            .NOCs
            .AsNoTracking()
            .ToList();

        var models = this.mapper.Map<List<NOCCacheModel>>(nocs);

        return models;
    }
}