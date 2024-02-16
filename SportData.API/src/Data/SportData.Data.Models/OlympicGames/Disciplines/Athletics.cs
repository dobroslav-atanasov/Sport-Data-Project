namespace SportData.Data.Models.OlympicGames.Disciplines;

using SportData.Data.Models.Entities.OlympicGames.Enumerations;

public class Athletics : BaseModel
{
    public AthleticsEventType EventType { get; set; }

    public string Position { get; set; }

    public int? Lane { get; set; }

    public double? ReactionTime { get; set; }

    public TimeSpan? Time { get; set; }

    public TimeSpan? TimeAutomatic { get; set; }

    public TimeSpan? TimeHand { get; set; }

    public int? Points { get; set; }

    public int? BentKneeWarnings { get; set; }

    public int? LostOfContactWarnings { get; set; }

    public int? Warnings { get; set; }

    public TimeSpan? TieBreakingTime { get; set; }

    public double? Wind { get; set; }






    //public TimeSpan? ExchangeTime { get; set; }

    //public TimeSpan? SplitTime { get; set; }

    //public int? SplitRank { get; set; }


    //public double? BestMeasurement { get; set; }

    //public double? SecondBestMeasurement { get; set; }

    //public int? TotalAttempts { get; set; }

    //public int? TotalMisses { get; set; }

    //public int? Misses { get; set; }

    //public string EventName { get; set; }

    //public DateTime? Date { get; set; }

    //public List<int> Orders { get; set; } = new();

    //public List<Athletics> Combined { get; set; } = new();

    //public List<AthleticsAttempt> Attempts { get; set; } = new();

    public List<AthleticsSplit> Splits { get; set; } = new();

    public List<Athletics> Athletes { get; set; } = new();
}

public class AthleticsAttempt
{
    public int Number { get; set; }

    public double? Measurement { get; set; }

    public RecordType Record { get; set; }

    public AthleticsTry Try1 { get; set; }

    public AthleticsTry Try2 { get; set; }

    public AthleticsTry Try3 { get; set; }
}

public enum AthleticsTry
{
    None,
    Success,
    Fail,
    Skip,
}

public enum AthleticsEventType
{
    None,
    TrackEvents,
    RoadEvents,
    FieldEvents,
    CombinedEvents,
    CrossCountryEvent,
}

public class AthleticsSplit
{
    //public int Number { get; set; }

    //public int HeatNumber { get; set; }

    public string Distance { get; set; }

    public TimeSpan? Time { get; set; }
}