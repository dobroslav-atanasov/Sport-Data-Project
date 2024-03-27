﻿namespace SportData.Data.Models.Cache;

using SportData.Data.Models.Entities.OlympicGames;
using SportData.Services.Mapper.Interfaces;

public class EventCache : IMapFrom<Event>
{
    public Guid Id { get; set; }

    public string OriginalName { get; set; }

    public string Name { get; set; }

    public string NormalizedName { get; set; }

    public int EventGenderTypeId { get; set; }

    public bool IsTeamEvent { get; set; }

    public int DisciplineId { get; set; }

    public int GameId { get; set; }
}