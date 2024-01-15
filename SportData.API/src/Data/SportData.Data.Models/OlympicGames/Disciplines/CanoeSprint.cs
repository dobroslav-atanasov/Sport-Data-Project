namespace SportData.Data.Models.OlympicGames.Disciplines;

public class CanoeSprint : BaseModel
{
    public int? Lane { get; set; }

    public TimeSpan? Time { get; set; }

    public TimeSpan? Exchange { get; set; }

    public List<CanoeSprintIntermediate> Intermediates { get; set; } = new();

    public List<CanoeSprint> Athletes { get; set; } = new();
}

public class CanoeSprintIntermediate
{
    public int Number { get; set; }

    public int Meters { get; set; }

    public TimeSpan? Time { get; set; }
}