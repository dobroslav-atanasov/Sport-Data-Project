namespace SportData.Data.Models.OlympicGames;

using SportData.Data.Models.Converters;

public class Round<TModel>
{
    public string EventName { get; set; }

    public RoundModel RoundModel { get; set; }

    //public RoundType Type { get; set; }

    public string Format { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public Track Track { get; set; }

    public List<Judge> Judges { get; set; } = new();

    public List<TModel> Athletes { get; set; } = new();

    public List<AthleteMatch<TModel>> AthleteMatches { get; set; } = new();

    public List<TModel> Teams { get; set; } = new();

    public List<TeamMatch<TModel>> TeamMatches { get; set; } = new();
}