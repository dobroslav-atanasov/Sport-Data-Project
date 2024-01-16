namespace SportData.Data.Models.Entities.SportData;

using System;

using global::SportData.Data.Common.Interfaces;

using Microsoft.AspNetCore.Identity;

public class ApplicationRole : IdentityRole, ICreatableEntity
{
    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }
}