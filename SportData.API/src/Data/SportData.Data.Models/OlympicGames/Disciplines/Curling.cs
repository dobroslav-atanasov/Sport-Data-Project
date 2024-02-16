namespace SportData.Data.Models.OlympicGames.Disciplines;

public class Curling : BaseModel
{
    public string Position { get; set; }

    public int? Percent { get; set; }

    public List<End> Ends { get; set; } = new();

    public List<Cricket> Athletes { get; set; } = new();
}

public class End
{
    public int Number { get; set; }

    public int? Points { get; set; }
}