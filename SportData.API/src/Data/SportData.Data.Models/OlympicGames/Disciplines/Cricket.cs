namespace SportData.Data.Models.OlympicGames.Disciplines;

public class Cricket : BaseModel
{
    public List<Cricket> Athletes { get; set; } = new();
}