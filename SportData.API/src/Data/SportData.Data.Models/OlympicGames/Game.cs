namespace SportData.Data.Models.OlympicGames;

using SportData.Data.Models.Entities.OlympicGames.Enumerations;

public class Game
{
    public int Id { get; set; }

    public int Year { get; set; }

    public OlympicGameType Type { get; set; }
}