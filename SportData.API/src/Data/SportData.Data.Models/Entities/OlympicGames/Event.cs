namespace SportData.Data.Models.Entities.OlympicGames;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using global::SportData.Data.Common.Models;

[Table("Events", Schema = "dbo")]
public class Event : BaseDeletableEntity<Guid>
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; }

    [Required]
    [MaxLength(200)]
    public string OriginalName { get; set; }

    [Required]
    [MaxLength(200)]
    public string NormalizedName { get; set; }

    [Required]
    public int EventGenderTypeId { get; set; }
    public virtual EventGenderType EventGenderType { get; set; }

    [Required]
    public int DisciplineId { get; set; }
    public virtual Discipline Discipline { get; set; }

    [Required]
    public int GameId { get; set; }
    public virtual Game Game { get; set; }

    [Column(TypeName = "Date")]
    public DateTime? StartDate { get; set; }

    [Column(TypeName = "Date")]
    public DateTime? EndDate { get; set; }

    [Required]
    public bool IsTeamEvent { get; set; } = false;

    [MaxLength(200)]
    public string AdditionalInfo { get; set; }

    //public int Athletes { get; set; }

    //public int NOCs { get; set; }

    public string Format { get; set; }

    public string Description { get; set; }

    public virtual ICollection<EventVenue> EventsVenues { get; set; } = new HashSet<EventVenue>();

    public virtual ICollection<Participation> Participations { get; set; } = new HashSet<Participation>();
}