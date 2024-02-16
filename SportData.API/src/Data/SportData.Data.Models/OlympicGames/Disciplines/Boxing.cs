namespace SportData.Data.Models.OlympicGames.Disciplines;

public class Boxing : BaseModel
{
    public int? Points { get; set; }

    public string Trunks { get; set; }

    public int? TotalPoints { get; set; }

    public int? InRound { get; set; }

    public TimeSpan? Time { get; set; }

    public BoxingDecision Decision { get; set; }

    public int? Round1 { get; set; }

    public int? Round2 { get; set; }

    public int? Round3 { get; set; }

    public int? Round4 { get; set; }

    /// <summary>
    /// Judge #1 Score     Original Judge #1 Score
    /// </summary>
    public int? Judge1 { get; set; }

    public int? Judge2 { get; set; }

    public int? Judge3 { get; set; }

    public int? Judge4 { get; set; }

    public int? Judge5 { get; set; }
}

public enum BoxingDecision
{
    None,
    Decision,
    Disqualification,
    Knockout,
    NoContest,
    Walkover,
    Retirement,
    RefereeStopsContest,
    RefereeStopsContestHeadBlow,
    RefereeStopsContestInjured,
    RefereeStopsContestOutclassed,
    RefereeStopsContestOutscored,
}