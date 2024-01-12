﻿namespace SportData.Data.Models.Entities.OlympicGames;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using global::SportData.Data.Common.Interfaces;
using global::SportData.Data.Common.Models;
using global::SportData.Data.Models.Entities.Enumerations;
using global::SportData.Data.Models.Entities.OlympicGames.Enumerations;

[Table("Teams", Schema = "dbo")]
public class Team : BaseDeletableEntity<Guid>, IUpdatable<Team>
{
    public Team()
    {
        this.Squads = new HashSet<Squad>();
    }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; }

    public int EventId { get; set; }
    public virtual Event Event { get; set; }

    public int NOCId { get; set; }
    public virtual NOC NOC { get; set; }

    public Guid? CoachId { get; set; }
    public virtual Athlete Coach { get; set; }

    public MedalType Medal { get; set; } = MedalType.None;

    public FinishStatus FinishStatus { get; set; }

    public virtual ICollection<Squad> Squads { get; set; }

    public bool IsUpdated(Team other)
    {
        var isUpdated = false;

        if (this.CoachId != other.CoachId)
        {
            this.CoachId = other.CoachId;
            isUpdated = true;
        }

        if (this.Medal != other.Medal)
        {
            this.Medal = other.Medal;
            isUpdated = true;
        }

        if (this.FinishStatus != other.FinishStatus)
        {
            this.FinishStatus = other.FinishStatus;
            isUpdated = true;
        }

        return isUpdated;
    }
}