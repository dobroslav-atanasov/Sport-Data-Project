namespace SportData.Data.Models.OlympicGames.Disciplines;

using SportData.Data.Models.OlympicGames;

public class BMXFreestyle : BaseModel
{
    public double? Points { get; set; }

    public List<BMXFreestyleRun> Runs { get; set; }
}

public class BMXFreestyleRun
{
    public int Number { get; set; }

    public double? Points { get; set; }
}