namespace SportData.Services.Data.OlympicGamesDb;

using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using SportData.Data.Factories.Interfaces;
using SportData.Data.Models.Entities.OlympicGames;
using SportData.Services.Data.OlympicGamesDb.Interfaces;

public class HostsService : IHostsService
{
    private readonly IDbContextFactory dbContextFactory;

    public HostsService(IDbContextFactory dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<Host> AddOrUpdateAsync(Host host)
    {
        var context = this.dbContextFactory.CreateOlympicGamesDbContext();

        var dbHost = await context.Hosts.FirstOrDefaultAsync(x => x.CityId == host.CityId && x.GameId == host.GameId);
        if (dbHost == null)
        {
            await context.AddAsync(host);
            await context.SaveChangesAsync();
        }
        else
        {
            host = dbHost;
        }

        return host;
    }
}