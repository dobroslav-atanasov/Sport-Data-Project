namespace SportData.Services.Data.OlympicGamesDb;

using System.Collections.Generic;

using SportData.Data.Models.Cache;
using SportData.Services.Data.OlympicGamesDb.Interfaces;

public class DataCacheService : IDataCacheService
{
    private readonly Lazy<ICollection<NOCCacheModel>> nocCacheModels;
    private readonly Lazy<ICollection<GameCacheModel>> gameCacheModels;
    private readonly Lazy<ICollection<DisciplineCacheModel>> disciplineCacheModels;
    private readonly Lazy<ICollection<VenueCacheModel>> venueCacheModels;
    private readonly Lazy<ICollection<EventCacheModel>> eventCacheModels;
    private readonly INOCsService nocsService;
    private readonly IGamesService gamesService;
    private readonly IDisciplinesService disciplinesService;
    private readonly IVenuesService venuesService;
    private readonly IEventsService eventsService;

    public DataCacheService(INOCsService nocsService, IGamesService gamesService, IDisciplinesService disciplinesService, IVenuesService venuesService, IEventsService eventsService)
    {
        nocCacheModels = new Lazy<ICollection<NOCCacheModel>>(() => this.nocsService.GetNOCCacheModels());
        gameCacheModels = new Lazy<ICollection<GameCacheModel>>(() => this.gamesService.GetGameCacheModels());
        disciplineCacheModels = new Lazy<ICollection<DisciplineCacheModel>>(() => this.disciplinesService.GetDisciplineCacheModels());
        venueCacheModels = new Lazy<ICollection<VenueCacheModel>>(() => this.venuesService.GetVenueCacheModels());
        eventCacheModels = new Lazy<ICollection<EventCacheModel>>(() => this.eventsService.GetEventCacheModels());
        this.nocsService = nocsService;
        this.gamesService = gamesService;
        this.disciplinesService = disciplinesService;
        this.venuesService = venuesService;
        this.eventsService = eventsService;
    }

    public ICollection<NOCCacheModel> NOCCacheModels => nocCacheModels.Value;

    public ICollection<GameCacheModel> GameCacheModels => gameCacheModels.Value;

    public ICollection<DisciplineCacheModel> DisciplineCacheModels => disciplineCacheModels.Value;

    public ICollection<VenueCacheModel> VenueCacheModels => venueCacheModels.Value;

    public ICollection<EventCacheModel> EventCacheModels => eventCacheModels.Value;
}