namespace SportData.Data.Models.OlympicGames;

public class Track
{
    public Guid PersonId { get; set; }

    public string PersonName { get; set; }

    public double? Length { get; set; }

    /// <summary>
    /// Gates   Curves  Total
    /// </summary>
    public int? Turns { get; set; }

    public double? StartAltitude { get; set; }

    public double? HeightDifference { get; set; }

    public int? Downstream { get; set; }

    public int? Upstream { get; set; }

    public double? MaximumClimb { get; set; }

    public double? TotalClimb { get; set; }

    public decimal? Intermediate1 { get; set; }

    public decimal? Intermediate2 { get; set; }

    public decimal? Intermediate3 { get; set; }

    public decimal? Intermediate4 { get; set; }

    public decimal? Intermediate5 { get; set; }

    public decimal? Intermediate6 { get; set; }

    public decimal? Intermediate7 { get; set; }

    public decimal? Intermediate8 { get; set; }

    public decimal? Intermediate9 { get; set; }
}