namespace SportData.Data.Models.OlympicGames.Disciplines;

public class AlpineSkiing : BaseModel
{
    public TimeSpan? Time { get; set; }

    public double? Points { get; set; }

    public TimeSpan? Downhill { get; set; }

    public TimeSpan? Slalom { get; set; }

    public TimeSpan? PenaltyTime { get; set; }

    public TimeSpan? Run1Time { get; set; }

    public TimeSpan? Run2Time { get; set; }

    public int Race { get; set; }

    public List<AlpineSkiing> Athletes { get; set; } = new();
}