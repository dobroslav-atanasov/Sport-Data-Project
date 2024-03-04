namespace SportData.Data.ViewModels.Countries;

using SportData.Data.Models.Entities.SportData;
using SportData.Services.Mapper.Interfaces;

public class CountryViewModel : IMapFrom<Country>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Code { get; set; }

    public string TwoDigitsCode { get; set; }
}