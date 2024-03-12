namespace SportData.Services.Data.OlympicGamesDb.Interfaces;

using SportData.Data.Models.Cache;

public interface IDataCacheService
{
    ICollection<OlympicGameTypeCache> OlympicGameTypes { get; }

    ICollection<EventGenderTypeCache> EventGenderTypes { get; }

    ICollection<GameCache> Games { get; }

    ICollection<DisciplineCache> Disciplines { get; }

    ICollection<VenueCache> Venues { get; }

    ICollection<GenderCache> Genders { get; }

    ICollection<AthleteTypeCache> AthleteTypes { get; }

    ICollection<ClubCache> Clubs { get; }
}