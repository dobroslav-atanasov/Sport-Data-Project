namespace SportData.Data.Models.Entities.OlympicGames;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using global::SportData.Data.Common.Models;

[Table("NOCs", Schema = "dbo")]
public class NOC : BaseDeletableEntity<int>
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(200)]
    public string OfficialName { get; set; }

    [Required]
    [StringLength(3)]
    public string Code { get; set; }

    [MinLength(10)]
    public string FlagCode { get; set; }

    public int? Created { get; set; }

    public int? Recognized { get; set; }

    public int? Disbanded { get; set; }

    [MaxLength(20)]
    public string Abbreviation { get; set; }

    [MaxLength(100)]
    public string ContinentalAssociation { get; set; }

    [StringLength(3)]
    public string RelatedNOCCode { get; set; }

    [MaxLength(10000)]
    public string Description { get; set; }

    public virtual ICollection<NOCPresident> NOCPresidents { get; set; } = new HashSet<NOCPresident>();

    public virtual ICollection<Participation> Participations { get; set; } = new HashSet<Participation>();
}