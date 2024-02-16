namespace SportData.Data.Models.OlympicGames.Disciplines;

public class CyclingRoad : BaseModel
{
    public TimeSpan? Time { get; set; }

    public int? Points { get; set; }

    public List<CyclingRoadIntermediate> Intermediates { get; set; } = new();

    public List<CyclingRoad> Athletes { get; set; } = new();
}

public class CyclingRoadIntermediate
{
    public int Number { get; set; }

    public TimeSpan? Time { get; set; }
}