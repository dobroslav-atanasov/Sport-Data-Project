﻿namespace SportData.Data.Models.Entities.OlympicGames;

using System.ComponentModel.DataAnnotations.Schema;

using global::SportData.Data.Common.Interfaces;

[Table("Hosts", Schema = "dbo")]
public class Host : ICreatableEntity, IDeletableEntity
{
    public int CityId { get; set; }
    public virtual City City { get; set; }

    public int GameId { get; set; }
    public virtual Game Game { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }
}