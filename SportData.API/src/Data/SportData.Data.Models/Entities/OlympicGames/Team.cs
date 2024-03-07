namespace SportData.Data.Models.Entities.OlympicGames;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using global::SportData.Data.Common.Models;

[Table("Teams", Schema = "dbo")]
public class Team : BaseDeletableEntity<Guid>
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; }

    public Guid EventId { get; set; }
    public virtual Event Event { get; set; }

    public int NOCId { get; set; }
    public virtual NOC NOC { get; set; }

    public Guid? CoachId { get; set; }
    public virtual Athlete Coach { get; set; }

    public int MedalId { get; set; }
    public virtual Medal Medal { get; set; }

    public int FinishTypeId { get; set; }
    public virtual FinishType FinishType { get; set; }

    public virtual ICollection<Squad> Squads { get; set; } = new HashSet<Squad>();

    public virtual ICollection<ResultTeam> ResultsTeams { get; set; } = new HashSet<ResultTeam>();
}