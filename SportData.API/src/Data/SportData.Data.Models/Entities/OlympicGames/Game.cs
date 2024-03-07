namespace SportData.Data.Models.Entities.OlympicGames;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using global::SportData.Data.Common.Models;

[Table("Games", Schema = "dbo")]
public class Game : BaseDeletableEntity<int>
{
    [Required]
    public int Year { get; set; }

    [MaxLength(10)]
    public string Number { get; set; }

    [Required]
    public int OlympicGameTypeId { get; set; }
    public virtual OlympicGameType OlympicGameType { get; set; }

    [Required]
    [MaxLength(100)]
    public string OfficialName { get; set; }

    [Column(TypeName = "Date")]
    public DateTime? OpeningDate { get; set; }

    [Column(TypeName = "Date")]
    public DateTime? ClosingDate { get; set; }

    [Column(TypeName = "Date")]
    public DateTime? StartCompetitionDate { get; set; }

    [Column(TypeName = "Date")]
    public DateTime? EndCompetitionDate { get; set; }

    //public int ParticipantAthletes { get; set; }

    //public int ParticipantMenAthletes { get; set; }

    //public int ParticipantWomenAthletes { get; set; }

    //public int ParticipantNOCs { get; set; }

    //public int MedalEvents { get; set; }

    //public int MedalDisciplines { get; set; }

    //public int MedalSports { get; set; }

    [MaxLength(500)]
    public string OpenBy { get; set; }

    [MaxLength(5000)]
    public string Torchbearers { get; set; }

    [MaxLength(500)]
    public string AthleteOathBy { get; set; }

    [MaxLength(500)]
    public string JudgeOathBy { get; set; }

    [MaxLength(500)]
    public string CoachOathBy { get; set; }

    [MaxLength(500)]
    public string OlympicFlagBearers { get; set; }

    [MaxLength(50000)]
    public string Description { get; set; }

    [MaxLength(10000)]
    public string BidProcess { get; set; }

    public virtual ICollection<Host> Hosts { get; set; } = new HashSet<Host>();

    public virtual ICollection<Event> Events { get; set; } = new HashSet<Event>();
}