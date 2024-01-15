namespace SportData.Data.Models.OlympicGames;

public class TeamMatch<TModel> : BaseMatch
{
    public TModel Team1 { get; set; }

    public TModel Team2 { get; set; }
}