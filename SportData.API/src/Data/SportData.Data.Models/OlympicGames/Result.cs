namespace SportData.Data.Models.OlympicGames;

public class Result<TModel>
{
    public Event Event { get; set; }

    public Discipline Discipline { get; set; }

    public Game Game { get; set; }

    public List<Round<TModel>> Rounds { get; set; } = new();
}