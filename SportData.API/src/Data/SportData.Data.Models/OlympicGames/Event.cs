namespace SportData.Data.Models.OlympicGames;

using SportData.Data.Models.Entities.Enumerations;

public class Event
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string OriginalName { get; set; }

    public string NormalizedName { get; set; }

    public bool IsTeamEvent { get; set; }

    public GenderType Gender { get; set; }
}