namespace SportData.Data.Models.OlympicGames;

using SportData.Data.Models.Entities.Enumerations;
using SportData.Data.Models.Entities.OlympicGames.Enumerations;

public abstract class BaseMatch
{
    public int Number { get; set; }

    public string Location { get; set; }

    public int? Attendance { get; set; }

    public DateTime? Date { get; set; }

    public MedalType Medal { get; set; }

    public string Info { get; set; }

    public int ResultId { get; set; }

    public DecisionType Decision { get; set; }

    public List<Judge> Judges { get; set; } = new();
}