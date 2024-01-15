namespace SportData.Services.Interfaces;

using SportData.Data.Models.Converters;
using SportData.Data.Models.Entities.Enumerations;
using SportData.Data.Models.Entities.OlympicGames.Enumerations;

public interface INormalizeService
{
    string MapOlympicGamesCountriesAndWorldCountries(string code);

    string NormalizeHostCityName(string hostCity);

    string NormalizeEventName(string name, int gameYear, string disciplineName);

    string ReplaceNonEnglishLetters(string name);

    AthleteType MapAthleteType(string text);

    string MapCityNameAndYearToNOCCode(string cityName, int year);

    GenderType MapGenderType(string text);

    string CleanEventName(string text);

    RoundModel MapRound(string title);

    TableModel MapGroup(string title, string html);
}