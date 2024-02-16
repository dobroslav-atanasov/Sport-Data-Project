namespace SportData.Data.Models.OlympicGames.Disciplines;

using SportData.Data.Models.Entities.OlympicGames.Enumerations;

public class CanoeSlalom : BaseModel
{
    public TimeSpan? Time { get; set; }

    public List<CanoeSlalomRun> Runs { get; set; } = new();

    public List<CanoeSlalom> Athletes { get; set; } = new();
}

public class CanoeSlalomRun
{
    public int Number { get; set; }

    public TimeSpan? TotalTime { get; set; }

    public TimeSpan? Time { get; set; }

    public decimal? PenaltySeconds { get; set; }

    public FinishStatus FinishStatus { get; set; }
}