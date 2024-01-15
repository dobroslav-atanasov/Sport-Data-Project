namespace SportData.Data.Models.OlympicGames.Disciplines;

public class Bobsleigh : BaseModel
{
    public TimeSpan? Time { get; set; }

    public List<BobsleighRun> Runs { get; set; } = new();

    public List<Bobsleigh> Athletes { get; set; } = new();
}

public class BobsleighRun
{
    public int Number { get; set; }

    public TimeSpan? Time { get; set; }

    public TimeSpan? Intermediate1 { get; set; }

    public TimeSpan? Intermediate2 { get; set; }

    public TimeSpan? Intermediate3 { get; set; }

    public TimeSpan? Intermediate4 { get; set; }

    public TimeSpan? Intermediate5 { get; set; }

    public TimeSpan? Split1 { get; set; }

    public TimeSpan? Split2 { get; set; }

    public TimeSpan? Split3 { get; set; }

    public TimeSpan? Split4 { get; set; }

    public TimeSpan? Split5 { get; set; }
}