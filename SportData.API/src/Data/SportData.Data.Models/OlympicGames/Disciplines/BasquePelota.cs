namespace SportData.Data.Models.OlympicGames.Disciplines;

public class BasquePelota : BaseModel
{
    public List<BasquePelota> Athletes { get; set; } = new();
}