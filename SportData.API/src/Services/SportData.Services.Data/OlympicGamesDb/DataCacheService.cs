namespace SportData.Services.Data.OlympicGamesDb;

using System.Collections.Generic;

using SportData.Data.Models.Cache;
using SportData.Data.Models.Entities.OlympicGames;
using SportData.Data.Repositories;
using SportData.Services.Data.OlympicGamesDb.Interfaces;
using SportData.Services.Mapper.Extensions;

public class DataCacheService : IDataCacheService
{
    private readonly Lazy<ICollection<OlympicGameTypeCache>> olympicGameTypes;
    private readonly Lazy<ICollection<EventGenderTypeCache>> eventGenderTypes;
    private readonly Lazy<ICollection<GameCache>> games;
    private readonly Lazy<ICollection<DisciplineCache>> disciplines;
    private readonly Lazy<ICollection<VenueCache>> venues;
    private readonly Lazy<ICollection<GenderCache>> genders;
    private readonly Lazy<ICollection<AthleteTypeCache>> athleteTypes;
    private readonly Lazy<ICollection<ClubCache>> clubs;

    private readonly OlympicGamesRepository<OlympicGameType> olympicGameTypesRepository;
    private readonly OlympicGamesRepository<EventGenderType> eventGenderTypeRepository;
    private readonly OlympicGamesRepository<Game> gameRepository;
    private readonly OlympicGamesRepository<Discipline> disciplineRepository;
    private readonly OlympicGamesRepository<Venue> venueRepository;
    private readonly OlympicGamesRepository<Gender> genderRepository;
    private readonly OlympicGamesRepository<AthleteType> athleteTypeRepository;
    private readonly OlympicGamesRepository<Club> clubRepository;

    public DataCacheService(OlympicGamesRepository<OlympicGameType> olympicGameTypesRepository, OlympicGamesRepository<EventGenderType> eventGenderTypeRepository,
        OlympicGamesRepository<Game> gameRepository, OlympicGamesRepository<Discipline> disciplineRepository, OlympicGamesRepository<Venue> venueRepository,
        OlympicGamesRepository<Gender> genderRepository, OlympicGamesRepository<AthleteType> athleteTypeRepository, OlympicGamesRepository<Club> clubRepository)
    {
        this.olympicGameTypes = new Lazy<ICollection<OlympicGameTypeCache>>(() => this.GetAllOlympicTypes());
        this.eventGenderTypes = new Lazy<ICollection<EventGenderTypeCache>>(() => this.GetAllEventGenderTypes());
        this.games = new Lazy<ICollection<GameCache>>(() => this.GetAllGames());
        this.disciplines = new Lazy<ICollection<DisciplineCache>>(() => this.GetAllDisciplines());
        this.venues = new Lazy<ICollection<VenueCache>>(() => this.GetAllVenues());
        this.genders = new Lazy<ICollection<GenderCache>>(() => this.GetAllGenders());
        this.athleteTypes = new Lazy<ICollection<AthleteTypeCache>>(() => this.GetAllAthleteTypes());
        this.clubs = new Lazy<ICollection<ClubCache>>(() => this.GetAllClubs());
        this.olympicGameTypesRepository = olympicGameTypesRepository;
        this.eventGenderTypeRepository = eventGenderTypeRepository;
        this.gameRepository = gameRepository;
        this.disciplineRepository = disciplineRepository;
        this.venueRepository = venueRepository;
        this.genderRepository = genderRepository;
        this.athleteTypeRepository = athleteTypeRepository;
        this.clubRepository = clubRepository;
    }

    private ICollection<ClubCache> GetAllClubs()
    {
        return this.clubRepository
            .AllAsNoTracking()
            .To<ClubCache>()
            .ToList();
    }

    private ICollection<AthleteTypeCache> GetAllAthleteTypes()
    {
        return this.athleteTypeRepository
            .AllAsNoTracking()
            .To<AthleteTypeCache>()
            .ToList();
    }

    private ICollection<GenderCache> GetAllGenders()
    {
        return this.genderRepository
            .AllAsNoTracking()
            .To<GenderCache>()
            .ToList();
    }

    private ICollection<VenueCache> GetAllVenues()
    {
        return this.venueRepository
            .AllAsNoTracking()
            .To<VenueCache>()
            .ToList();
    }

    private ICollection<DisciplineCache> GetAllDisciplines()
    {
        return this.disciplineRepository
            .AllAsNoTracking()
            .To<DisciplineCache>()
            .ToList();
    }

    private ICollection<GameCache> GetAllGames()
    {
        return this.gameRepository
            .AllAsNoTracking()
            .To<GameCache>()
            .ToList();
    }

    private ICollection<EventGenderTypeCache> GetAllEventGenderTypes()
    {
        return this.eventGenderTypeRepository
            .AllAsNoTracking()
            .To<EventGenderTypeCache>()
            .ToList();
    }

    private ICollection<OlympicGameTypeCache> GetAllOlympicTypes()
    {
        return this.olympicGameTypesRepository
            .AllAsNoTracking()
            .To<OlympicGameTypeCache>()
            .ToList();
    }

    public ICollection<OlympicGameTypeCache> OlympicGameTypes => this.olympicGameTypes.Value;

    public ICollection<EventGenderTypeCache> EventGenderTypes => this.eventGenderTypes.Value;

    public ICollection<GameCache> Games => this.games.Value;

    public ICollection<DisciplineCache> Disciplines => this.disciplines.Value;

    public ICollection<VenueCache> Venues => this.venues.Value;

    public ICollection<GenderCache> Genders => this.genders.Value;

    public ICollection<AthleteTypeCache> AthleteTypes => this.athleteTypes.Value;

    public ICollection<ClubCache> Clubs => this.clubs.Value;
}