namespace SportData.Data.Models.Entities.OlympicGames;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using global::SportData.Data.Common.Models;

[Table("Venues", Schema = "dbo")]
public class Venue : BaseDeletableEntity<int>
{
    [Required]
    public int Number { get; set; }

    [Required]
    [MaxLength(500)]
    public string Name { get; set; }

    [Required]
    [MaxLength(500)]
    public string City { get; set; }

    [MaxLength(500)]
    public string EnglishName { get; set; }

    [MaxLength(1000)]
    public string FullName { get; set; }

    public double? LatitudeCoordinate { get; set; }

    public double? LongitudeCoordinate { get; set; }

    public int? OpenedYear { get; set; }

    public int? DemolishedYear { get; set; }

    [MaxLength(100)]
    public string Capacity { get; set; }

    public virtual ICollection<EventVenue> EventsVenues { get; set; } = new HashSet<EventVenue>();
}