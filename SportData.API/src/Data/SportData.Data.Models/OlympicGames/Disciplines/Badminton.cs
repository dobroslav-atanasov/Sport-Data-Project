namespace SportData.Data.Models.OlympicGames.Disciplines;

public class Badminton : BaseModel
{
    public int? Game1 { get; set; }

    public int? Game2 { get; set; }

    public int? Game3 { get; set; }

    public List<Badminton> Athletes { get; set; } = new();
}