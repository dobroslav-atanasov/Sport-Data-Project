namespace SportData.Data.Models.OlympicGames.Disciplines;

public class BMXRacing : BaseModel
{
    public int? Points { get; set; }

    public TimeSpan? Time { get; set; }

    public List<BMXRacingRun> Runs { get; set; } = new();
}

public class BMXRacingRun
{
    public int Number { get; set; }

    public int? Points { get; set; }

    public TimeSpan? Time { get; set; }
}