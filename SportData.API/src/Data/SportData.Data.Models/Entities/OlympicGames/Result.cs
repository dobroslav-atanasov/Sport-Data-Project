namespace SportData.Data.Models.Entities.OlympicGames;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using global::SportData.Data.Common.Models;

[Table("Results", Schema = "dbo")]
public class Result : BaseDeletableEntity<Guid>
{
    public Guid EventId { get; set; }
    public virtual Event Event { get; set; }

    [Required]
    public string Json { get; set; }

    public virtual ICollection<ResultParticipation> ResultsParticipations { get; set; } = new HashSet<ResultParticipation>();

    public virtual ICollection<ResultTeam> ResultsTeams { get; set; } = new HashSet<ResultTeam>();
}