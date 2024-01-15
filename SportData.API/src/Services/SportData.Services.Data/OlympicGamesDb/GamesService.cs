namespace SportData.Services.Data.OlympicGamesDb;

using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using SportData.Data.Factories.Interfaces;
using SportData.Data.Models.Cache;
using SportData.Data.Models.Entities.OlympicGames;
using SportData.Services.Data.OlympicGamesDb.Interfaces;

public class GamesService : IGamesService
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly IMapper mapper;

    public GamesService(IDbContextFactory dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<Game> AddOrUpdateAsync(Game game)
    {
        var context = this.dbContextFactory.CreateOlympicGamesDbContext();

        var dbGame = await context.Games.FirstOrDefaultAsync(x => x.Year == game.Year && x.Type == game.Type);
        if (dbGame == null)
        {
            await context.AddAsync(game);
            await context.SaveChangesAsync();
        }
        else
        {
            var isUpdated = dbGame.IsUpdated(game);
            if (isUpdated)
            {
                context.Update(dbGame);
                await context.SaveChangesAsync();
            }

            game = dbGame;
        }

        return game;
    }

    public ICollection<GameCacheModel> GetGameCacheModels()
    {
        var context = this.dbContextFactory.CreateOlympicGamesDbContext();

        var games = context
            .Games
            .AsNoTracking()
            .ToList();

        var models = this.mapper.Map<List<GameCacheModel>>(games);

        return models;
    }
}