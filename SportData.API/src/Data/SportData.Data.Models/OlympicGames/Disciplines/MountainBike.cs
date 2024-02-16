namespace SportData.Data.Models.OlympicGames.Disciplines;

public class MountainBike : BaseModel
{
    public TimeSpan? Time { get; set; }

    public List<MountainBIkeIntermediate> Intermediates { get; set; }
}

public class MountainBIkeIntermediate
{
    public int Number { get; set; }

    public TimeSpan? Time { get; set; }
}