namespace SportData.Data.Models.Entities.OlympicGames;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using global::SportData.Data.Common.Interfaces;
using global::SportData.Data.Common.Models;

[Table("Athletes", Schema = "dbo")]
public class Athlete : BaseDeletableEntity<Guid>, IUpdatable<Athlete>
{
    public int Code { get; set; }

    public int GenderId { get; set; }
    public virtual Gender Gender { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } // Used name

    [Required]
    [MaxLength(200)]
    public string TranslateName { get; set; }

    [MaxLength(200)]
    public string FullName { get; set; }

    [MaxLength(500)]
    public string OriginalName { get; set; }

    [MaxLength(100)]
    public string Citizenship { get; set; } // Nationality

    public DateTime? BirthDate { get; set; }

    [MaxLength(100)]
    public string BirthCity { get; set; }

    [MaxLength(100)]
    public string BirthCountry { get; set; }

    public DateTime? DiedDate { get; set; }

    [MaxLength(100)]
    public string DiedCity { get; set; }

    [MaxLength(100)]
    public string DiedCountry { get; set; }

    public int? HeightInCentimeters { get; set; }

    public double? HeightInInches { get; set; }

    public int? WeightInKilograms { get; set; }

    public int? WeightInPounds { get; set; }

    [MaxLength(10000)]
    public string Description { get; set; }

    public virtual ICollection<AthleteClub> AthletesClubs { get; set; } = new HashSet<AthleteClub>();

    public virtual ICollection<Participation> Participations { get; set; } = new HashSet<Participation>();

    public virtual ICollection<Role> Roles { get; set; } = new HashSet<Role>();

    public bool IsUpdated(Athlete other)
    {
        var isUpdated = false;

        if (Name != other.Name)
        {
            Name = other.Name;
            isUpdated = true;
        }

        if (TranslateName != other.TranslateName)
        {
            TranslateName = other.TranslateName;
            isUpdated = true;
        }

        if (FullName != other.FullName)
        {
            FullName = other.FullName;
            isUpdated = true;
        }

        if (GenderId != other.GenderId)
        {
            GenderId = other.GenderId;
            isUpdated = true;
        }

        if (OriginalName != other.OriginalName)
        {
            OriginalName = other.OriginalName;
            isUpdated = true;
        }

        if (Citizenship != other.Citizenship)
        {
            Citizenship = other.Citizenship;
            isUpdated = true;
        }

        if (BirthDate != other.BirthDate)
        {
            BirthDate = other.BirthDate;
            isUpdated = true;
        }

        if (DiedDate != other.DiedDate)
        {
            DiedDate = other.DiedDate;
            isUpdated = true;
        }

        if (BirthCity != other.BirthCity)
        {
            BirthCity = other.BirthCity;
            isUpdated = true;
        }

        if (BirthCountry != other.BirthCountry)
        {
            BirthCountry = other.BirthCountry;
            isUpdated = true;
        }

        if (DiedCity != other.DiedCity)
        {
            DiedCity = other.DiedCity;
            isUpdated = true;
        }

        if (DiedCountry != other.DiedCountry)
        {
            DiedCountry = other.DiedCountry;
            isUpdated = true;
        }

        if (HeightInCentimeters != other.HeightInCentimeters)
        {
            HeightInCentimeters = other.HeightInCentimeters;
            isUpdated = true;
        }

        if (HeightInInches != other.HeightInInches)
        {
            HeightInInches = other.HeightInInches;
            isUpdated = true;
        }

        if (WeightInKilograms != other.WeightInKilograms)
        {
            WeightInKilograms = other.WeightInKilograms;
            isUpdated = true;
        }

        if (WeightInPounds != other.WeightInPounds)
        {
            WeightInPounds = other.WeightInPounds;
            isUpdated = true;
        }

        if (Description != other.Description)
        {
            Description = other.Description;
            isUpdated = true;
        }

        return isUpdated;
    }
}