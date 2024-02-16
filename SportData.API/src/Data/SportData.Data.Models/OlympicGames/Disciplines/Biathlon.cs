namespace SportData.Data.Models.OlympicGames.Disciplines;

public class Biathlon : BaseModel
{
    public TimeSpan? Time { get; set; }

    public int? Misses { get; set; }

    public TimeSpan? Race { get; set; }

    public TimeSpan? StartBehind { get; set; }

    public TimeSpan? Skiing { get; set; }

    public TimeSpan? Exchange { get; set; }

    public int? ExtraShots { get; set; }

    public string Position { get; set; }

    public List<Shooting> Shootings { get; set; } = new();

    public List<Biathlon> Athletes { get; set; } = new();
}

public class Shooting
{
    public int Number { get; set; }

    public TimeSpan? Time { get; set; }

    public int? Misses { get; set; }

    public int? ExtraShots { get; set; }
}