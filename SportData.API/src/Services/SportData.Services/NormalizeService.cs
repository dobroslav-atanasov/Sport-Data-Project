﻿namespace SportData.Services;

using System.Text.RegularExpressions;

using SportData.Data.Models.Converters;
using SportData.Data.Models.Entities.Enumerations;
using SportData.Data.Models.Entities.OlympicGames.Enumerations;
using SportData.Services.Interfaces;

public class NormalizeService : INormalizeService
{
    public string CleanEventName(string text)
    {
        var name = string.Empty;
        if (text.StartsWith("Open"))
        {
            name = text.Replace("Open", string.Empty).Trim();
        }
        else
        {
            name = text.Replace("Men", string.Empty).Replace("Women", string.Empty).Replace("Mixed", string.Empty).Trim();
        }

        return name;
    }

    public AthleteType MapAthleteType(string text)
    {
        text = text.Replace(" •", ",");
        var type = AthleteType.None;
        switch (text)
        {
            case "Competed in Intercalated Games":
            case "Competed in Intercalated Games, Non-starter":
            case "Competed in Olympic Games":
            case "Competed in Olympic Games (non-medal events)":
            case "Competed in Olympic Games (non-medal events), Competed in Intercalated Games":
            case "Competed in Olympic Games (non-medal events), Non-starter":
            case "Competed in Olympic Games, Competed in Intercalated Games":
            case "Competed in Olympic Games, Competed in Intercalated Games, Non-starter":
            case "Competed in Olympic Games, Competed in Olympic Games (non-medal events)":
            case "Competed in Olympic Games, Competed in Olympic Games (non-medal events), Competed in Intercalated Games":
            case "Competed in Olympic Games, Competed in Olympic Games (non-medal events), Non-starter":
            case "Competed in Olympic Games, Competed in Youth Olympic Games":
            case "Competed in Olympic Games, Competed in Youth Olympic Games (non-medal events)":
            case "Competed in Olympic Games, Competed in Youth Olympic Games, Non-starter":
            case "Competed in Olympic Games, Non-starter":
            case "Competed in Olympic Games, Other":
            case "Competed in Youth Olympic Games":
            case "Competed in Youth Olympic Games, Non-starter":
            case "Non-starter":
            case "Non-starter, Other":
            case "Other":
                type = AthleteType.Athlete;
                break;
            case "Coach":
                type = AthleteType.Coach;
                break;
            case "Referee":
                type = AthleteType.Referee;
                break;
            case "Coach, Other":
            case "Competed in Olympic Games (non-medal events), Coach":
            case "Competed in Olympic Games, Coach":
            case "Competed in Olympic Games, Competed in Olympic Games (non-medal events), Coach":
            case "Competed in Olympic Games, Non-starter, Coach":
            case "Non-starter, Coach":
                type = AthleteType.Athlete | AthleteType.Coach;
                break;
            case "Coach, Referee":
                type = AthleteType.Coach | AthleteType.Referee;
                break;
            case "Competed in Olympic Games, Competed in Intercalated Games, Non-starter, Referee":
            case "Competed in Olympic Games, Competed in Intercalated Games, Referee":
            case "Competed in Olympic Games, Competed in Olympic Games (non-medal events), Competed in Intercalated Games, Referee":
            case "Competed in Olympic Games, Competed in Olympic Games (non-medal events), Referee":
            case "Competed in Olympic Games, Non-starter, Referee":
            case "Competed in Olympic Games, Referee":
            case "Non-starter, Referee":
            case "Referee, Other":
                type = AthleteType.Athlete | AthleteType.Referee;
                break;
            case "Competed in Olympic Games, Coach, Referee":
            case "Competed in Olympic Games, Non-starter, Coach, Referee":
            case "Non-starter, Coach, Referee":
                type = AthleteType.Athlete | AthleteType.Coach | AthleteType.Referee;
                break;
            case "Competed in Olympic Games, IOC member":
            case "Competed in Olympic Games, Non-starter, IOC member":
                type = AthleteType.Athlete | AthleteType.IOCMember;
                break;
            case "Competed in Olympic Games, IOC member, Referee":
                type = AthleteType.Athlete | AthleteType.Referee | AthleteType.IOCMember;
                break;
            case "IOC member":
                type = AthleteType.IOCMember;
                break;
            case "IOC member, Coach":
                type = AthleteType.Coach | AthleteType.IOCMember;
                break;
            case "IOC member, Referee":
                type = AthleteType.Referee | AthleteType.IOCMember;
                break;
        }

        return type;
    }

    public string MapCityNameAndYearToNOCCode(string cityName, int year)
    {
        var text = $"{cityName} {year}";
        var code = string.Empty;
        switch (text)
        {
            case "Athens 1896": code = "GRE"; break;
            case "Paris 1900": code = "FRA"; break;
            case "St. Louis 1904": code = "USA"; break;
            case "London 1908": code = "GBR"; break;
            case "Stockholm 1912": code = "SWE"; break;
            case "Berlin 1916": code = "GER"; break;
            case "Antwerp 1920": code = "BEL"; break;
            case "Paris 1924": code = "FRA"; break;
            case "Amsterdam 1928": code = "NED"; break;
            case "Los Angeles 1932": code = "USA"; break;
            case "Berlin 1936": code = "GER"; break;
            case "Helsinki 1940": code = "FIN"; break;
            case "London 1944": code = "GBR"; break;
            case "London 1948": code = "GBR"; break;
            case "Helsinki 1952": code = "FIN"; break;
            case "Melbourne 1956": code = "AUS"; break;
            case "Rome 1960": code = "ITA"; break;
            case "Tokyo 1964": code = "JPN"; break;
            case "Mexico City 1968": code = "MEX"; break;
            case "Munich 1972": code = "FRG"; break;
            case "Montreal 1976": code = "CAN"; break;
            case "Moscow 1980": code = "URS"; break;
            case "Los Angeles 1984": code = "USA"; break;
            case "Seoul 1988": code = "KOR"; break;
            case "Barcelona 1992": code = "ESP"; break;
            case "Atlanta 1996": code = "USA"; break;
            case "Sydney 2000": code = "AUS"; break;
            case "Athens 2004": code = "GRE"; break;
            case "Beijing 2008": code = "CHN"; break;
            case "London 2012": code = "GBR"; break;
            case "Rio de Janeiro 2016": code = "BRA"; break;
            case "Tokyo 2020": code = "JPN"; break;
            case "Paris 2024": code = "FRA"; break;
            case "Los Angeles 2028": code = "USA"; break;
            case "Brisbane 2032": code = "AUS"; break;
            case "Chamonix 1924": code = "FRA"; break;
            case "St. Moritz 1928": code = "SUI"; break;
            case "Lake Placid 1932": code = "USA"; break;
            case "Garmisch-Partenkirchen 1936": code = "GER"; break;
            case "Garmisch-Partenkirchen 1940": code = "GER"; break;
            case "Cortina d'Ampezzo 1944": code = "ITA"; break;
            case "St. Moritz 1948": code = "SUI"; break;
            case "Oslo 1952": code = "NOR"; break;
            case "Cortina d'Ampezzo 1956": code = "ITA"; break;
            case "Squaw Valley 1960": code = "USA"; break;
            case "Innsbruck 1964": code = "AUT"; break;
            case "Grenoble 1968": code = "FRA"; break;
            case "Sapporo 1972": code = "JPN"; break;
            case "Innsbruck 1976": code = "AUT"; break;
            case "Lake Placid 1980": code = "USA"; break;
            case "Sarajevo 1984": code = "YUG"; break;
            case "Calgary 1988": code = "CAN"; break;
            case "Albertville 1992": code = "FRA"; break;
            case "Lillehammer 1994": code = "NOR"; break;
            case "Nagano 1998": code = "JPN"; break;
            case "Salt Lake City 2002": code = "USA"; break;
            case "Turin 2006": code = "ITA"; break;
            case "Vancouver 2010": code = "CAN"; break;
            case "Sochi 2014": code = "RUS"; break;
            case "PyeongChang 2018": code = "KOR"; break;
            case "Beijing 2022": code = "CHN"; break;
            case "Milano-Cortina d'Ampezzo 2026": code = "ITA"; break;
            case "Stockholm 1956": code = "SWE"; break;
        }

        return code;
    }

    public GenderType MapGenderType(string text)
    {
        if (text.ToLower().StartsWith("men"))
        {
            return GenderType.Men;
        }
        else if (text.ToLower().StartsWith("women"))
        {
            return GenderType.Women;
        }
        else if (text.ToLower().StartsWith("mixed") || text.ToLower().StartsWith("open"))
        {
            return GenderType.Mixed;
        }

        return GenderType.None;
    }

    public string MapOlympicGamesCountriesAndWorldCountries(string code)
    {
        return code switch
        {
            "AFG" => "AFG",
            "ALB" => "ALB",
            "ALG" => "DZA",
            "ASA" => "ASM",
            "AND" => "AND",
            "ANG" => "AGO",
            "ANT" => "ATG",
            "ARG" => "ARG",
            "ARM" => "ARM",
            "ARU" => "ABW",
            "AUS" => "AUS",
            "AUT" => "AUT",
            "AZE" => "AZE",
            "BAH" => "BHS",
            "BRN" => "BHR",
            "BAN" => "BGD",
            "BAR" => "BRB",
            "BLR" => "BLR",
            "BEL" => "BEL",
            "BIZ" => "BLZ",
            "BEN" => "BEN",
            "BER" => "BMU",
            "BHU" => "BTN",
            "BOL" => "BOL",
            "BIH" => "BIH",
            "BOT" => "BWA",
            "BRA" => "BRA",
            "IVB" => "VGB",
            "BRU" => "BRN",
            "BUL" => "BGR",
            "BUR" => "BFA",
            "BDI" => "BDI",
            "CAM" => "KHM",
            "CMR" => "CMR",
            "CAN" => "CAN",
            "CPV" => "CPV",
            "CAY" => "CYM",
            "CAF" => "CAF",
            "CHA" => "TCD",
            "CHI" => "CHL",
            "COL" => "COL",
            "COM" => "COM",
            "CGO" => "COG",
            "COK" => "COK",
            "CRC" => "CRI",
            "CIV" => "CIV",
            "CRO" => "HRV",
            "CUB" => "CUB",
            "CYP" => "CYP",
            "CZE" => "CZE",
            "PRK" => "PRK",
            "COD" => "COD",
            "DEN" => "DNK",
            "DJI" => "DJI",
            "DMA" => "DMA",
            "DOM" => "DOM",
            "ECU" => "ECU",
            "EGY" => "EGY",
            "ESA" => "SLV",
            "GEQ" => "GNQ",
            "ERI" => "ERI",
            "EST" => "EST",
            "SWZ" => "SWZ",
            "ETH" => "ETH",
            "FSM" => "FSM",
            "FIJ" => "FJI",
            "FIN" => "FIN",
            "FRA" => "FRA",
            "GAB" => "GAB",
            "GEO" => "GEO",
            "GER" => "DEU",
            "GHA" => "GHA",
            "GBR" => "GBR",
            "GRE" => "GRC",
            "GRN" => "GRD",
            "GUM" => "GUM",
            "GUA" => "GTM",
            "GUI" => "GIN",
            "GBS" => "GNB",
            "GUY" => "GUY",
            "HAI" => "HTI",
            "HON" => "HND",
            "HKG" => "HKG",
            "HUN" => "HUN",
            "ISL" => "ISL",
            "IND" => "IND",
            "INA" => "IDN",
            "IRQ" => "IRQ",
            "IRL" => "IRL",
            "IRI" => "IRN",
            "ISR" => "ISR",
            "ITA" => "ITA",
            "JAM" => "JAM",
            "JPN" => "JPN",
            "JOR" => "JOR",
            "KAZ" => "KAZ",
            "KEN" => "KEN",
            "KSA" => "SAU",
            "KIR" => "KIR",
            "KOS" => "UNK",
            "KUW" => "KWT",
            "KGZ" => "KGZ",
            "LAO" => "LAO",
            "LAT" => "LVA",
            "LBN" => "LBN",
            "LES" => "LSO",
            "LBR" => "LBR",
            "LBA" => "LBY",
            "LIE" => "LIE",
            "LTU" => "LTU",
            "LUX" => "LUX",
            "MAD" => "MDG",
            "MAW" => "MWI",
            "MAS" => "MYS",
            "MDV" => "MDV",
            "MLI" => "MLI",
            "MLT" => "MLT",
            "MHL" => "MHL",
            "MTN" => "MRT",
            "MRI" => "MUS",
            "MEX" => "MEX",
            "MON" => "MCO",
            "MGL" => "MNG",
            "MNE" => "MNE",
            "MAR" => "MAR",
            "MOZ" => "MOZ",
            "MYA" => "MMR",
            "NAM" => "NAM",
            "NRU" => "NRU",
            "NEP" => "NPL",
            "NED" => "NLD",
            "NZL" => "NZL",
            "NCA" => "NIC",
            "NIG" => "NER",
            "NGR" => "NGA",
            "MKD" => "MKD",
            "NOR" => "NOR",
            "OMA" => "OMN",
            "PAK" => "PAK",
            "PLW" => "PLW",
            "PLE" => "PSE",
            "PAN" => "PAN",
            "PNG" => "PNG",
            "PAR" => "PRY",
            "CHN" => "CHN",
            "PER" => "PER",
            "PHI" => "PHL",
            "POL" => "POL",
            "POR" => "PRT",
            "PUR" => "PRI",
            "QAT" => "QAT",
            "KOR" => "KOR",
            "MDA" => "MDA",
            "ROU" => "ROU",
            "RUS" => "RUS",
            "RWA" => "RWA",
            "SKN" => "KNA",
            "LCA" => "LCA",
            "VIN" => "VCT",
            "SAM" => "WSM",
            "SMR" => "SMR",
            "STP" => "STP",
            "SEN" => "SEN",
            "SRB" => "SRB",
            "YUG" => "SRB",
            "SEY" => "SYC",
            "SLE" => "SLE",
            "SGP" => "SGP",
            "SVK" => "SVK",
            "SLO" => "SVN",
            "SOL" => "SLB",
            "SOM" => "SOM",
            "RSA" => "ZAF",
            "SSD" => "SSD",
            "ESP" => "ESP",
            "SRI" => "LKA",
            "SUD" => "SDN",
            "SUR" => "SUR",
            "SWE" => "SWE",
            "SUI" => "CHE",
            "SYR" => "SYR",
            "TJK" => "TJK",
            "THA" => "THA",
            "GAM" => "GMB",
            "TLS" => "TLS",
            "TOG" => "TGO",
            "TGA" => "TON",
            "TTO" => "TTO",
            "TUN" => "TUN",
            "TUR" => "TUR",
            "TKM" => "TKM",
            "TUV" => "TUV",
            "UGA" => "UGA",
            "UKR" => "UKR",
            "UAE" => "ARE",
            "TAN" => "TZA",
            "USA" => "USA",
            "ISV" => "VIR",
            "URU" => "URY",
            "UZB" => "UZB",
            "VAN" => "VUT",
            "VEN" => "VEN",
            "VIE" => "VNM",
            "YEM" => "YEM",
            "ZAM" => "ZMB",
            "ZIM" => "ZWE",
            _ => null
        };
    }

    public string NormalizeEventName(string name, int gameYear, string disciplineName)
    {
        name = Regex.Replace(name, @"(\d+)\s+(\d+)", me =>
        {
            return $"{me.Groups[1].Value.Trim()}{me.Groups[2].Value.Trim()}";
        });

        name = Regex.Replace(name, @"(\d+),(\d+)", me =>
        {
            return $"{me.Groups[1].Value.Trim()}{me.Groups[2].Value.Trim()}";
        });

        name = name.Replace(" x ", "x")
            .Replace("82½", "82.5")
            .Replace("67½", "67.5")
            .Replace("333⅓", "333 1/3")
            .Replace(" × ", "x")
            .Replace("¼", "1/4")
            .Replace("⅓", "1/3")
            .Replace("½", "1/2")
            .Replace("²", string.Empty)
            .Replace("kilometer", "kilometers")
            .Replace("metres", "meters")
            .Replace("kilometres", "kilometers")
            .Replace("≤", "-")
            .Replace(">", "+");

        name = name.Replace(" / ", "/")
            .Replace(" meters", "m")
            .Replace(" kilometers", "km")
            .Replace(" miles", "miles")
            .Replace(" mile", "mile")
            .Replace(" km", "km")
            .Replace("Pommelled Horse", "Pommel Horse")
            .Replace("Teams", "Team")
            .Replace("Horse Vault", "Vault")
            .Replace("Alpine Combined", "Combined")
            .Replace("Super Combined", "Combined")
            .Replace("Birds", "Bird")
            .Replace("Pole Archery", "Fixed")
            .Replace("Apparatus Work and Field Sports", string.Empty)
            .Replace("Individual All-Around, Apparatus Work", "Triathlon")
            .Replace("Individual All-Around, 4 Events", "Combined")
            .Replace("European System", string.Empty)
            .Replace("Four/Five", "Four")
            .Replace("Canadian Singles", "C-1")
            .Replace("Canadian Doubles", "C-2")
            .Replace("Kayak Singles", "K-1")
            .Replace("Kayak Doubles", "K-2")
            .Replace("Kayak Fours", "K-4")
            .Replace("Kayak Relay", "K-1")
            .Replace("Two-Man Teams With Cesta", "Team")
            .Replace("Eights", "Eight")
            .Replace("Coxed Fours", "Coxed Four")
            .Replace("Coxed Teams", "Coxed Pair")
            .Replace("Coxless Fours", "Coxless Four")
            .Replace("Coxless Teams", "Coxless Pair")
            .Replace("Covered Courts", "Indoor")
            //.Replace("", "")
            //.Replace("", "")
            .Replace("Target Archery", "Moving Bird");

        if (gameYear == 1924 && disciplineName == "Artistic Gymnastics" && name == "Side Horse, Men")
        {
            name = "Pommel Horse, Men";
        }

        return name;
    }

    public string NormalizeHostCityName(string hostCity)
    {
        return hostCity switch
        {
            "Athina" => "Athens",
            "Antwerpen" => "Antwerp",
            "Ciudad de México" => "Mexico City",
            "Moskva" => "Moscow",
            "Sankt Moritz" => "St. Moritz",
            "Roma" => "Rome",
            "München" => "Munich",
            "Montréal" => "Montreal",
            "Torino" => "Turin",
            _ => hostCity
        };
    }

    public string ReplaceNonEnglishLetters(string name)
    {
        name = name.Replace("-", "-")
            .Replace("‐", "-")
            .Replace("–", "-")
            .Replace(",", string.Empty)
            .Replace(".", string.Empty)
            .Replace("'", string.Empty)
            .Replace("’", string.Empty)
            .Replace("(", string.Empty)
            .Replace(")", string.Empty)
            .Replace("`", string.Empty)
            .Replace("а", "a")
            .Replace("А", "A")
            .Replace("і", "i")
            .Replace("о", "o")
            .Replace("á", "а")
            .Replace("Á", "А")
            .Replace("à", "а")
            .Replace("À", "А")
            .Replace("ă", "а")
            .Replace("ằ", "а")
            .Replace("â", "а")
            .Replace("Â", "А")
            .Replace("ấ", "а")
            .Replace("ầ", "а")
            .Replace("ẩ", "а")
            .Replace("å", "а")
            .Replace("Å", "А")
            .Replace("ä", "а")
            .Replace("Ä", "А")
            .Replace("ã", "а")
            .Replace("ą", "а")
            .Replace("ā", "а")
            .Replace("Ā", "А")
            .Replace("ả", "а")
            .Replace("ạ", "а")
            .Replace("ặ", "а")
            .Replace("ậ", "а")
            .Replace("æ", "ае")
            .Replace("Æ", "Ae")
            .Replace("ć", "c")
            .Replace("Ć", "C")
            .Replace("č", "c")
            .Replace("Č", "C")
            .Replace("ç", "c")
            .Replace("Ç", "C")
            .Replace("ď", "d")
            .Replace("Ď", "D")
            .Replace("đ", "d")
            .Replace("Đ", "D")
            .Replace("ð", "d")
            .Replace("Ð", "D")
            .Replace("é", "e")
            .Replace("É", "E")
            .Replace("è", "e")
            .Replace("È", "E")
            .Replace("ĕ", "e")
            .Replace("ê", "e")
            .Replace("Ê", "E")
            .Replace("ế", "e")
            .Replace("ề", "e")
            .Replace("ễ", "e")
            .Replace("ể", "e")
            .Replace("ě", "e")
            .Replace("ë", "e")
            .Replace("ė", "e")
            .Replace("ę", "e")
            .Replace("ē", "e")
            .Replace("Ē", "E")
            .Replace("ệ", "e")
            .Replace("ə", "e")
            .Replace("Ə", "E")
            .Replace("Ǵ", "G")
            .Replace("ğ", "g")
            .Replace("ģ", "g")
            .Replace("Ģ", "G")
            .Replace("í", "i")
            .Replace("Í", "I")
            .Replace("ì", "i")
            .Replace("î", "i")
            .Replace("ï", "i")
            .Replace("İ", "I")
            .Replace("ī", "i")
            .Replace("ị", "i")
            .Replace("ı", "i")
            .Replace("ķ", "k")
            .Replace("Ķ", "K")
            .Replace("ľ", "l")
            .Replace("Ľ", "L")
            .Replace("ļ", "l")
            .Replace("ł", "l")
            .Replace("Ł", "L")
            .Replace("ń", "n")
            .Replace("ň", "n")
            .Replace("ñ", "n")
            .Replace("ņ", "n")
            .Replace("Ņ", "N")
            .Replace("ó", "o")
            .Replace("Ó", "O")
            .Replace("ò", "o")
            .Replace("ô", "o")
            .Replace("ố", "o")
            .Replace("ồ", "o")
            .Replace("ỗ", "o")
            .Replace("ö", "o")
            .Replace("Ö", "O")
            .Replace("ő", "o")
            .Replace("Ő", "O")
            .Replace("õ", "o")
            .Replace("Õ", "O")
            .Replace("ø", "o")
            .Replace("Ø", "O")
            .Replace("ơ", "o")
            .Replace("ớ", "o")
            .Replace("ờ", "o")
            .Replace("ọ", "o")
            .Replace("œ", "oe")
            .Replace("ř", "r")
            .Replace("Ř", "R")
            .Replace("ś", "s")
            .Replace("Ś", "S")
            .Replace("š", "s")
            .Replace("Š", "S")
            .Replace("ş", "s")
            .Replace("Ş", "S")
            .Replace("ș", "s")
            .Replace("Ș", "S")
            .Replace("ß", "ss")
            .Replace("ť", "t")
            .Replace("Ť", "T")
            .Replace("ţ", "t")
            .Replace("Ţ", "T")
            .Replace("ț", "t")
            .Replace("Ț", "T")
            .Replace("ú", "u")
            .Replace("Ú", "U")
            .Replace("ù", "u")
            .Replace("û", "u")
            .Replace("ů", "u")
            .Replace("ü", "u")
            .Replace("Ü", "U")
            .Replace("ű", "u")
            .Replace("ũ", "u")
            .Replace("ū", "u")
            .Replace("Ū", "U")
            .Replace("ủ", "u")
            .Replace("ư", "u")
            .Replace("ứ", "u")
            .Replace("ữ", "u")
            .Replace("ụ", "u")
            .Replace("ý", "y")
            .Replace("Ý", "Y")
            .Replace("ỳ", "y")
            .Replace("ÿ", "y")
            .Replace("ỹ", "y")
            .Replace("ỷ", "y")
            .Replace("ź", "z")
            .Replace("Ź", "Z")
            .Replace("ž", "z")
            .Replace("Ž", "Z")
            .Replace("ż", "z")
            .Replace("Ż", "Z")
            .Replace("þ", "th")
            .Replace("Þ", "Th")
            .Replace("ϊ", "i");

        return name;
    }

    public RoundModel MapRound(string title)
    {
        switch (title)
        {
            case "1/8-Final Repêchage": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.Eightfinals, Group = 0, Description = null };
            case "1/8-Final Repêchage Final": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.Eightfinals, Group = 0, Description = null };
            //case "100 yards": return new RoundModel { Name = title, Type = RoundType.Yards100, SubType = RoundType.None, Group = 0, Description = null };
            case "2nd-Place Final Round": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.FinalRound, Group = 0, Description = null };
            case "2nd-Place Round One": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.RoundOne, Group = 0, Description = null };
            case "2nd-Place Semi-Finals": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.Semifinals, Group = 0, Description = null };
            case "2nd-Place Tournament": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.FinalRound, Group = 0, Description = "Silver Medal" };
            case "3rd-Place Final Round": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.FinalRound, Group = 0, Description = null };
            case "3rd-Place Quarter-Finals": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.Quarterfinals, Group = 0, Description = null };
            case "3rd-Place Round One": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.RoundOne, Group = 0, Description = null };
            case "3rd-Place Semi-Finals": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.Semifinals, Group = 0, Description = null };
            case "3rd-Place Tournament": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.FinalRound, Group = 0, Description = "Bronze Medal" };
            //case "Apparatus": return new RoundModel { Name = title, Type = RoundType.Apparatus, SubType = RoundType.None, Group = 0, Description = null };
            case "B Final": return new RoundModel { Name = title, Type = RoundType.Final, SubType = RoundType.None, Group = 2, Description = null };
            //case "Balance Beam": return new RoundModel { Name = title, Type = RoundType.BalanceBeam, SubType = RoundType.None, Group = 0, Description = null };
            case "Classification 5-8": return new RoundModel { Name = title, Type = RoundType.Classification, SubType = RoundType.None, Group = 0, Description = "5-8" };
            case "Classification 9-12": return new RoundModel { Name = title, Type = RoundType.Classification, SubType = RoundType.None, Group = 0, Description = "9-12" };
            case "Classification Final 1": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.Quarterfinals, Group = 0, Description = null };
            case "Classification Final 2": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.Quarterfinals, Group = 0, Description = null };
            case "Classification Round": return new RoundModel { Name = title, Type = RoundType.Classification, SubType = RoundType.None, Group = 0, Description = null };
            case "Classification Round 13-15": return new RoundModel { Name = title, Type = RoundType.Classification, SubType = RoundType.None, Group = 0, Description = "13-15" };
            case "Classification Round 13-16": return new RoundModel { Name = title, Type = RoundType.Classification, SubType = RoundType.None, Group = 0, Description = "13-16" };
            case "Classification Round 17-20": return new RoundModel { Name = title, Type = RoundType.Classification, SubType = RoundType.None, Group = 0, Description = "17-20" };
            case "Classification Round 17-23": return new RoundModel { Name = title, Type = RoundType.Classification, SubType = RoundType.None, Group = 0, Description = "17-23" };
            case "Classification Round 21-23": return new RoundModel { Name = title, Type = RoundType.Classification, SubType = RoundType.None, Group = 0, Description = "21-23" };
            case "Classification Round 2-3": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.FinalRound, Group = 0, Description = "Silver Medal" };
            case "Classification Round 3rd Place": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.FinalRound, Group = 0, Description = "Bronze Medal" };
            case "Classification Round 5-11": return new RoundModel { Name = title, Type = RoundType.Classification, SubType = RoundType.None, Group = 0, Description = "5-11" };
            case "Classification Round 5-8": return new RoundModel { Name = title, Type = RoundType.Classification, SubType = RoundType.None, Group = 0, Description = "5-8" };
            case "Classification Round 5-82": return new RoundModel { Name = title, Type = RoundType.Classification, SubType = RoundType.None, Group = 0, Description = "5-82" };
            case "Classification Round 7-10": return new RoundModel { Name = title, Type = RoundType.Classification, SubType = RoundType.None, Group = 0, Description = "7-10" };
            case "Classification Round 7-12": return new RoundModel { Name = title, Type = RoundType.Classification, SubType = RoundType.None, Group = 0, Description = "7-12" };
            case "Classification Round 9-11": return new RoundModel { Name = title, Type = RoundType.Classification, SubType = RoundType.None, Group = 0, Description = "9-11" };
            case "Classification Round 9-12": return new RoundModel { Name = title, Type = RoundType.Classification, SubType = RoundType.None, Group = 0, Description = "9-12" };
            case "Classification Round 9-123": return new RoundModel { Name = title, Type = RoundType.Classification, SubType = RoundType.None, Group = 0, Description = "9-123" };
            case "Classification Round 9-16": return new RoundModel { Name = title, Type = RoundType.Classification, SubType = RoundType.None, Group = 0, Description = "9-16" };
            case "Classification Round Five": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.RoundFive, Group = 0, Description = null };
            case "Classification Round for 5/6": return new RoundModel { Name = title, Type = RoundType.Classification, SubType = RoundType.None, Group = 0, Description = "for 5/6" };
            case "Classification Round Four": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.RoundFour, Group = 0, Description = null };
            case "Classification Round One": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.RoundOne, Group = 0, Description = null };
            case "Classification Round Six": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.RoundSix, Group = 0, Description = null };
            case "Classification Round Three": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.RoundThree, Group = 0, Description = null };
            case "Classification Round Two": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.RoundTwo, Group = 0, Description = null };
            case "Compulsory Dance": return new RoundModel { Name = title, Type = RoundType.CompulsoryDance, SubType = RoundType.None, Group = 0, Description = null };
            case "Compulsory Dance 1": return new RoundModel { Name = title, Type = RoundType.CompulsoryDance, SubType = RoundType.None, Group = 0, Description = null };
            case "Compulsory Dance 2": return new RoundModel { Name = title, Type = RoundType.CompulsoryDance, SubType = RoundType.None, Group = 0, Description = null };
            case "Compulsory Dances": return new RoundModel { Name = title, Type = RoundType.CompulsoryDance, SubType = RoundType.None, Group = 0, Description = null };
            case "Compulsory Dances Summary": return new RoundModel { Name = title, Type = RoundType.CompulsoryDance, SubType = RoundType.None, Group = 0, Description = null };
            case "Compulsory Figures": return new RoundModel { Name = title, Type = RoundType.CompulsoryFigures, SubType = RoundType.None, Group = 0, Description = null };
            case "Consolation Round": return new RoundModel { Name = title, Type = RoundType.ConsolationRound, SubType = RoundType.None, Group = 0, Description = null };
            case "Consolation Round - Final": return new RoundModel { Name = title, Type = RoundType.ConsolationRound, SubType = RoundType.Final, Group = 0, Description = null };
            case "Consolation Round - Round One": return new RoundModel { Name = title, Type = RoundType.ConsolationRound, SubType = RoundType.RoundOne, Group = 0, Description = null };
            case "Consolation Round - Semi-Finals": return new RoundModel { Name = title, Type = RoundType.ConsolationRound, SubType = RoundType.Semifinals, Group = 0, Description = null };
            case "Consolation Round: Final": return new RoundModel { Name = title, Type = RoundType.ConsolationRound, SubType = RoundType.Final, Group = 0, Description = null };
            case "Consolation Round: Quarter-Finals": return new RoundModel { Name = title, Type = RoundType.ConsolationRound, SubType = RoundType.Quarterfinals, Group = 0, Description = null };
            case "Consolation Round: Semi-Finals": return new RoundModel { Name = title, Type = RoundType.ConsolationRound, SubType = RoundType.Semifinals, Group = 0, Description = null };
            case "Consolation Tournament": return new RoundModel { Name = title, Type = RoundType.ConsolationRound, SubType = RoundType.None, Group = 0, Description = null };
            //case "Drill Section": return new RoundModel { Name = title, Type = RoundType.DrillSection, SubType = RoundType.None, Group = 0, Description = null };
            case "Eighth-Finals": return new RoundModel { Name = title, Type = RoundType.Eightfinals, SubType = RoundType.None, Group = 0, Description = null };
            case "Elimination Round": return new RoundModel { Name = title, Type = RoundType.EliminationRound, SubType = RoundType.None, Group = 0, Description = null };
            case "Elimination Rounds": return new RoundModel { Name = title, Type = RoundType.EliminationRound, SubType = RoundType.None, Group = 0, Description = null };
            case "Elimination Rounds, Round Five Repêchage": return new RoundModel { Name = title, Type = RoundType.EliminationRound, SubType = RoundType.Repechage, Group = 0, Description = "Round Five" };
            case "Elimination Rounds, Round Four": return new RoundModel { Name = title, Type = RoundType.EliminationRound, SubType = RoundType.RoundFour, Group = 0, Description = null };
            case "Elimination Rounds, Round Four Repêchage": return new RoundModel { Name = title, Type = RoundType.EliminationRound, SubType = RoundType.Repechage, Group = 0, Description = "Round Four" };
            case "Elimination Rounds, Round One": return new RoundModel { Name = title, Type = RoundType.EliminationRound, SubType = RoundType.RoundOne, Group = 0, Description = null };
            case "Elimination Rounds, Round One Repêchage": return new RoundModel { Name = title, Type = RoundType.EliminationRound, SubType = RoundType.Repechage, Group = 0, Description = "RoundOne" };
            case "Elimination Rounds, Round Three": return new RoundModel { Name = title, Type = RoundType.EliminationRound, SubType = RoundType.RoundThree, Group = 0, Description = null };
            case "Elimination Rounds, Round Three Repêchage": return new RoundModel { Name = title, Type = RoundType.EliminationRound, SubType = RoundType.Repechage, Group = 0, Description = "RoundThree" };
            case "Elimination Rounds, Round Two": return new RoundModel { Name = title, Type = RoundType.EliminationRound, SubType = RoundType.RoundTwo, Group = 0, Description = null };
            case "Elimination Rounds, Round Two Repêchage": return new RoundModel { Name = title, Type = RoundType.EliminationRound, SubType = RoundType.Repechage, Group = 0, Description = "Round Two" };
            case "Figures": return new RoundModel { Name = title, Type = RoundType.CompulsoryFigures, SubType = RoundType.None, Group = 0, Description = null };
            case "Final": return new RoundModel { Name = title, Type = RoundType.Final, SubType = RoundType.None, Group = 0, Description = null };
            case "Final Pool": return new RoundModel { Name = title, Type = RoundType.FinalRound, SubType = RoundType.None, Group = 0, Description = null };
            case "Final Pool, Barrage 1-2": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.FinalRound, Group = 0, Description = "Gold Medal" };
            case "Final Pool, Barrage 2-3": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.FinalRound, Group = 0, Description = "Silver Medal" };
            case "Final Pool, Barrage 3-4": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.FinalRound, Group = 0, Description = "Bronze Medal" };
            case "Final Round": return new RoundModel { Name = title, Type = RoundType.FinalRound, SubType = RoundType.None, Group = 0, Description = null };
            case "Final Round 1": return new RoundModel { Name = title, Type = RoundType.FinalRound, SubType = RoundType.RoundOne, Group = 0, Description = null };
            case "Final Round 2": return new RoundModel { Name = title, Type = RoundType.FinalRound, SubType = RoundType.RoundTwo, Group = 0, Description = null };
            case "Final Round 3": return new RoundModel { Name = title, Type = RoundType.FinalRound, SubType = RoundType.RoundThree, Group = 0, Description = null };
            case "Final Round One": return new RoundModel { Name = title, Type = RoundType.FinalRound, SubType = RoundType.RoundOne, Group = 0, Description = null };
            case "Final Round Three": return new RoundModel { Name = title, Type = RoundType.FinalRound, SubType = RoundType.RoundThree, Group = 0, Description = null };
            case "Final Round Two": return new RoundModel { Name = title, Type = RoundType.FinalRound, SubType = RoundType.RoundTwo, Group = 0, Description = null };
            case "Final Round2": return new RoundModel { Name = title, Type = RoundType.FinalRound, SubType = RoundType.None, Group = 0, Description = null };
            case "Final, Swim-Off": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.FinalRound, Group = 0, Description = "Swim Off" };
            case "Final1": return new RoundModel { Name = title, Type = RoundType.Final, SubType = RoundType.None, Group = 0, Description = null };
            case "First Final": return new RoundModel { Name = title, Type = RoundType.Final, SubType = RoundType.None, Group = 0, Description = "First" };
            case "Fleet Races": return new RoundModel { Name = title, Type = RoundType.FleetRaces, SubType = RoundType.None, Group = 0, Description = null };
            //case "Floor Exercise": return new RoundModel { Name = title, Type = RoundType.FloorExercise, SubType = RoundType.None, Group = 0, Description = null };
            case "Free Dance": return new RoundModel { Name = title, Type = RoundType.FreeSkating, SubType = RoundType.None, Group = 0, Description = null };
            case "Free Skating": return new RoundModel { Name = title, Type = RoundType.FreeSkating, SubType = RoundType.None, Group = 0, Description = null };
            case "Grand Prix": return new RoundModel { Name = title, Type = RoundType.GrandPrix, SubType = RoundType.None, Group = 0, Description = null };
            case "Grand Prix Freestyle": return new RoundModel { Name = title, Type = RoundType.GrandPrix, SubType = RoundType.None, Group = 0, Description = "Freestyle" };
            case "Grand Prix Special": return new RoundModel { Name = title, Type = RoundType.GrandPrix, SubType = RoundType.None, Group = 0, Description = "Special" };
            case "Group A": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 1, Description = "Group" };
            case "Group A - Final": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.Final, Group = 1, Description = "Group" };
            case "Group A - Round Five": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.RoundFive, Group = 1, Description = "Group" };
            case "Group A - Round Four": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.RoundFour, Group = 1, Description = "Group" };
            case "Group A - Round One": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.RoundOne, Group = 1, Description = "Group" };
            case "Group A - Round Seven": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.RoundSeven, Group = 1, Description = "Group" };
            case "Group A - Round Six": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.RoundSix, Group = 1, Description = "Group" };
            case "Group A - Round Three": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.RoundThree, Group = 1, Description = "Group" };
            case "Group A - Round Two": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.RoundTwo, Group = 1, Description = "Group" };
            case "Group A1": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 1, Description = "Group" };
            case "Group B": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 2, Description = "Group" };
            case "Group B - Final": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.Final, Group = 2, Description = "Group" };
            case "Group B - Round Five": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.RoundFive, Group = 2, Description = "Group" };
            case "Group B - Round Four": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.RoundFour, Group = 2, Description = "Group" };
            case "Group B - Round One": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.RoundOne, Group = 2, Description = "Group" };
            case "Group B - Round Seven": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.RoundSeven, Group = 2, Description = "Group" };
            case "Group B - Round Six": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.RoundSix, Group = 2, Description = "Group" };
            case "Group B - Round Three": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.RoundThree, Group = 2, Description = "Group" };
            case "Group B - Round Two": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.RoundTwo, Group = 2, Description = "Group" };
            case "Group B2": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 2, Description = "Group" };
            case "Group C": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 3, Description = "Group" };
            case "Group C3": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 3, Description = "Group" };
            case "Group D": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 4, Description = "Group" };
            case "Group E": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 5, Description = "Group" };
            //case "Group Exercises": return new RoundModel { Name = title, Type = RoundType.GroupExercise, SubType = RoundType.None, Group = 0, Description = null };
            case "Group F": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 6, Description = "Group" };
            case "Group G": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 7, Description = "Group" };
            case "Group H": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 8, Description = "Group" };
            case "Group I": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 9, Description = "Group" };
            case "Group J": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 10, Description = "Group" };
            case "Group K": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 11, Description = "Group" };
            case "Group L": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 12, Description = "Group" };
            case "Group M": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 13, Description = "Group" };
            case "Group N": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 14, Description = "Group" };
            case "Group O": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 15, Description = "Group" };
            case "Group One": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 1, Description = "Group" };
            case "Group P": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 16, Description = "Group" };
            case "Group Two": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 2, Description = "Group" };
            //case "Horizontal Bar": return new RoundModel { Name = title, Type = RoundType.HorizontalBar, SubType = RoundType.None, Group = 0, Description = null };
            //case "Horse Vault": return new RoundModel { Name = title, Type = RoundType.HorseVault, SubType = RoundType.None, Group = 0, Description = null };
            //case "Individual Standings": return new RoundModel { Name = title, Type = RoundType.IndividualStandings, SubType = RoundType.None, Group = 0, Description = null };
            case "Jump-Off": return new RoundModel { Name = title, Type = RoundType.PlayOff, SubType = RoundType.None, Group = 0, Description = null };
            case "Jump-Off for 1-2": return new RoundModel { Name = title, Type = RoundType.PlayOff, SubType = RoundType.None, Group = 0, Description = "Gold Medal" };
            case "Jump-Off for 3-9": return new RoundModel { Name = title, Type = RoundType.PlayOff, SubType = RoundType.None, Group = 0, Description = "Bronze Medal" };
            //case "Long Jump": return new RoundModel { Name = title, Type = RoundType.LongJump, SubType = RoundType.None, Group = 0, Description = null };
            case "Lucky Loser Round": return new RoundModel { Name = title, Type = RoundType.RoundLuckyLoser, SubType = RoundType.None, Group = 0, Description = null };
            case "Medal Pool": return new RoundModel { Name = title, Type = RoundType.RoundTwo, SubType = RoundType.None, Group = 0, Description = null };
            case "Original Final": return new RoundModel { Name = title, Type = RoundType.Final, SubType = RoundType.None, Group = 0, Description = "Original" };
            case "Original Round One": return new RoundModel { Name = title, Type = RoundType.RoundOne, SubType = RoundType.None, Group = 0, Description = "Original" };
            case "Original Set Pattern Dance": return new RoundModel { Name = title, Type = RoundType.OriginalSetPatternDance, SubType = RoundType.None, Group = 0, Description = null };
            //case "Parallel Bars": return new RoundModel { Name = title, Type = RoundType.ParallelBars, SubType = RoundType.None, Group = 0, Description = null };
            case "Play-Off for Bronze Medal": return new RoundModel { Name = title, Type = RoundType.PlayOff, SubType = RoundType.FinalRound, Group = 0, Description = "Bronze Medal" };
            case "Play-Off for Silver Medal": return new RoundModel { Name = title, Type = RoundType.PlayOff, SubType = RoundType.FinalRound, Group = 0, Description = "Silver Medal" };
            case "Play-offs": return new RoundModel { Name = title, Type = RoundType.PlayOff, SubType = RoundType.None, Group = 0, Description = null };
            //case "Pommelled Horse": return new RoundModel { Name = title, Type = RoundType.PommellHorse, SubType = RoundType.None, Group = 0, Description = null };
            case "Pool A": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 1, Description = "Pool" };
            case "Pool B": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 2, Description = "Pool" };
            case "Pool C": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 3, Description = "Pool" };
            case "Pool D": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 4, Description = "Pool" };
            case "Pool E": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 5, Description = "Pool" };
            case "Pool F": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 6, Description = "Pool" };
            case "Pool G": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 7, Description = "Pool" };
            case "Pool H": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 8, Description = "Pool" };
            //case "Precision Section": return new RoundModel { Name = title, Type = RoundType.PrecisionSection, SubType = RoundType.None, Group = 0, Description = null };
            case "Preliminary Round": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 0, Description = null };
            case "Qualification": return new RoundModel { Name = title, Type = RoundType.Qualification, SubType = RoundType.None, Group = 0, Description = null };
            case "Qualification Round": return new RoundModel { Name = title, Type = RoundType.Qualification, SubType = RoundType.None, Group = 0, Description = null };
            case "Qualifying": return new RoundModel { Name = title, Type = RoundType.Qualification, SubType = RoundType.None, Group = 0, Description = null };
            case "Qualifying Round": return new RoundModel { Name = title, Type = RoundType.Qualification, SubType = RoundType.None, Group = 0, Description = null };
            case "Qualifying Round 1": return new RoundModel { Name = title, Type = RoundType.Qualification, SubType = RoundType.RoundOne, Group = 0, Description = null };
            case "Qualifying Round 2": return new RoundModel { Name = title, Type = RoundType.Qualification, SubType = RoundType.RoundTwo, Group = 0, Description = null };
            case "Qualifying Round One": return new RoundModel { Name = title, Type = RoundType.Qualification, SubType = RoundType.RoundOne, Group = 0, Description = null };
            case "Qualifying Round Two": return new RoundModel { Name = title, Type = RoundType.Qualification, SubType = RoundType.RoundTwo, Group = 0, Description = null };
            case "Qualifying Round, Group A": return new RoundModel { Name = title, Type = RoundType.Qualification, SubType = RoundType.None, Group = 1, Description = "Group A" };
            case "Qualifying Round, Group A Re-Jump": return new RoundModel { Name = title, Type = RoundType.Qualification, SubType = RoundType.None, Group = 1, Description = "Re Jump" };
            case "Qualifying Round, Group A1": return new RoundModel { Name = title, Type = RoundType.Qualification, SubType = RoundType.None, Group = 1, Description = "Group A" };
            case "Qualifying Round, Group B": return new RoundModel { Name = title, Type = RoundType.Qualification, SubType = RoundType.None, Group = 2, Description = "Group B" };
            case "Qualifying Round, Group B1": return new RoundModel { Name = title, Type = RoundType.Qualification, SubType = RoundType.None, Group = 2, Description = "Group B" };
            case "Qualifying Round, Group C": return new RoundModel { Name = title, Type = RoundType.Qualification, SubType = RoundType.None, Group = 3, Description = "Group C" };
            case "Qualifying Round, Group C3": return new RoundModel { Name = title, Type = RoundType.Qualification, SubType = RoundType.None, Group = 3, Description = "Group C" };
            case "Qualifying Round, Group D": return new RoundModel { Name = title, Type = RoundType.Qualification, SubType = RoundType.None, Group = 4, Description = "Group D" };
            case "Qualifying Round, Group D4": return new RoundModel { Name = title, Type = RoundType.Qualification, SubType = RoundType.None, Group = 4, Description = "Group D" };
            case "Qualifying Round, Group E": return new RoundModel { Name = title, Type = RoundType.Qualification, SubType = RoundType.None, Group = 5, Description = "Group E" };
            case "Qualifying Round, Group F": return new RoundModel { Name = title, Type = RoundType.Qualification, SubType = RoundType.None, Group = 6, Description = "Group F" };
            case "Qualifying Round, Group One": return new RoundModel { Name = title, Type = RoundType.Qualification, SubType = RoundType.None, Group = 1, Description = "Group One" };
            case "Qualifying Round, Group Two": return new RoundModel { Name = title, Type = RoundType.Qualification, SubType = RoundType.None, Group = 2, Description = "Group Two" };
            case "Quarter Finals": return new RoundModel { Name = title, Type = RoundType.Quarterfinals, SubType = RoundType.None, Group = 0, Description = null };
            case "Quarter-Finals": return new RoundModel { Name = title, Type = RoundType.Quarterfinals, SubType = RoundType.None, Group = 0, Description = null };
            case "Quarter-Finals Repêchage": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.Quarterfinals, Group = 0, Description = null };
            case "Quarter-Finals, 64032": return new RoundModel { Name = title, Type = RoundType.Quarterfinals, SubType = RoundType.None, Group = 0, Description = null };
            case "Ranking Round": return new RoundModel { Name = title, Type = RoundType.RankingRound, SubType = RoundType.None, Group = 0, Description = null };
            case "Repêchage": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.None, Group = 0, Description = null };
            case "Repêchage Final": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.Final, Group = 0, Description = null };
            case "Repêchage Heats": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.None, Group = 0, Description = null };
            case "Repêchage Round One": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.RoundOne, Group = 0, Description = null };
            case "Repêchage Round Two": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.RoundTwo, Group = 0, Description = null };
            case "Re-run Final": return new RoundModel { Name = title, Type = RoundType.Final, SubType = RoundType.None, Group = 0, Description = "Re Run" };
            case "Rhythm Dance": return new RoundModel { Name = title, Type = RoundType.RhythmDance, SubType = RoundType.None, Group = 0, Description = null };
            //case "Rings": return new RoundModel { Name = title, Type = RoundType.Rings, SubType = RoundType.None, Group = 0, Description = null };
            case "Round Five": return new RoundModel { Name = title, Type = RoundType.RoundFive, SubType = RoundType.None, Group = 0, Description = null };
            case "Round Four": return new RoundModel { Name = title, Type = RoundType.RoundFour, SubType = RoundType.None, Group = 0, Description = null };
            case "Round Four5": return new RoundModel { Name = title, Type = RoundType.RoundFour, SubType = RoundType.None, Group = 0, Description = null };
            case "Round One": return new RoundModel { Name = title, Type = RoundType.RoundOne, SubType = RoundType.None, Group = 0, Description = null };
            case "Round One Pool Five": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 5, Description = "Pool" };
            case "Round One Pool Four": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 4, Description = "Pool" };
            case "Round One Pool One": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 1, Description = "Pool" };
            case "Round One Pool Six": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 6, Description = "Pool" };
            case "Round One Pool Three": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 3, Description = "Pool" };
            case "Round One Pool Two": return new RoundModel { Name = title, Type = RoundType.PreliminaryRound, SubType = RoundType.None, Group = 2, Description = "Pool" };
            case "Round One Repêchage": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.RoundOne, Group = 0, Description = null };
            case "Round One Repêchage Final": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.RoundOne, Group = 0, Description = "Final" };
            case "Round One Rerace": return new RoundModel { Name = title, Type = RoundType.RoundOne, SubType = RoundType.None, Group = 0, Description = "Rerace" };
            case "Round One, Heat Ten": return new RoundModel { Name = title, Type = RoundType.RoundOne, SubType = RoundType.None, Group = 0, Description = "Heat Ten" };
            case "Round One1": return new RoundModel { Name = title, Type = RoundType.RoundOne, SubType = RoundType.None, Group = 0, Description = null };
            case "Round One9": return new RoundModel { Name = title, Type = RoundType.RoundOne, SubType = RoundType.None, Group = 0, Description = null };
            case "Round Robin": return new RoundModel { Name = title, Type = RoundType.RoundRobin, SubType = RoundType.None, Group = 0, Description = null };
            case "Round Seven": return new RoundModel { Name = title, Type = RoundType.RoundSeven, SubType = RoundType.None, Group = 0, Description = null };
            case "Round Six": return new RoundModel { Name = title, Type = RoundType.RoundSix, SubType = RoundType.None, Group = 0, Description = null };
            case "Round Three": return new RoundModel { Name = title, Type = RoundType.RoundThree, SubType = RoundType.None, Group = 0, Description = null };
            case "Round Three Repêchage": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.RoundThree, Group = 0, Description = null };
            case "Round Two": return new RoundModel { Name = title, Type = RoundType.RoundTwo, SubType = RoundType.None, Group = 0, Description = null };
            case "Round Two Repêchage": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.RoundTwo, Group = 0, Description = null };
            case "Round Two Repêchage Final": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.RoundTwo, Group = 0, Description = "Final" };
            case "Round-Robin": return new RoundModel { Name = title, Type = RoundType.RoundRobin, SubType = RoundType.None, Group = 0, Description = null };
            case "Second Place Tournament - Final": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.FinalRound, Group = 0, Description = "Silver Medal" };
            case "Second Place Tournament - Round One": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.RoundOne, Group = 0, Description = null };
            case "Second Place Tournament - Round Two": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.RoundTwo, Group = 0, Description = null };
            case "Second Place Tournament - Semi-Finals": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.Semifinals, Group = 0, Description = null };
            case "Second-Place Tournament": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.FinalRound, Group = 0, Description = "Silver Medal" };
            case "Second-to-Fifth Place Tournament": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.FinalRound, Group = 0, Description = "Silver Medal" };
            case "Seeding Round": return new RoundModel { Name = title, Type = RoundType.RankingRound, SubType = RoundType.None, Group = 0, Description = null };
            case "Semi-Final": return new RoundModel { Name = title, Type = RoundType.Semifinals, SubType = RoundType.None, Group = 0, Description = null };
            case "Semi-Final Round": return new RoundModel { Name = title, Type = RoundType.Semifinals, SubType = RoundType.None, Group = 0, Description = null };
            case "Semi-Finals": return new RoundModel { Name = title, Type = RoundType.Semifinals, SubType = RoundType.None, Group = 0, Description = null };
            case "Semi-Finals A/B": return new RoundModel { Name = title, Type = RoundType.Semifinals, SubType = RoundType.None, Group = 0, Description = "A/B" };
            case "Semi-Finals C/D": return new RoundModel { Name = title, Type = RoundType.Semifinals, SubType = RoundType.None, Group = 0, Description = "C/D" };
            case "Semi-Finals E/F": return new RoundModel { Name = title, Type = RoundType.Semifinals, SubType = RoundType.None, Group = 0, Description = "E/F" };
            case "Semi-Finals Repêchage": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.Semifinals, Group = 0, Description = null };
            case "Semi-Finals3": return new RoundModel { Name = title, Type = RoundType.Semifinals, SubType = RoundType.None, Group = 0, Description = null };
            case "Shoot-Off": return new RoundModel { Name = title, Type = RoundType.PlayOff, SubType = RoundType.None, Group = 0, Description = null };
            case "Shoot-Off 1": return new RoundModel { Name = title, Type = RoundType.PlayOff, SubType = RoundType.None, Group = 0, Description = null };
            case "Shoot-Off 2": return new RoundModel { Name = title, Type = RoundType.PlayOff, SubType = RoundType.None, Group = 0, Description = null };
            case "Shoot-Off for 1st Place": return new RoundModel { Name = title, Type = RoundType.PlayOff, SubType = RoundType.None, Group = 0, Description = "Gold Medal" };
            case "Shoot-Off for 2nd Place": return new RoundModel { Name = title, Type = RoundType.PlayOff, SubType = RoundType.None, Group = 0, Description = "Silver Medal" };
            case "Shoot-Off for 3rd Place": return new RoundModel { Name = title, Type = RoundType.PlayOff, SubType = RoundType.None, Group = 0, Description = "Bronze Medal" };
            case "Short Dance": return new RoundModel { Name = title, Type = RoundType.ShortProgram, SubType = RoundType.None, Group = 0, Description = null };
            case "Short Program": return new RoundModel { Name = title, Type = RoundType.ShortProgram, SubType = RoundType.None, Group = 0, Description = null };
            //case "Shot Put": return new RoundModel { Name = title, Type = RoundType.ShotPut, SubType = RoundType.None, Group = 0, Description = null };
            //case "Side Horse": return new RoundModel { Name = title, Type = RoundType.SideHorse, SubType = RoundType.None, Group = 0, Description = null };
            //case "Team Drill": return new RoundModel { Name = title, Type = RoundType.TeamDrill, SubType = RoundType.None, Group = 0, Description = null };
            case "Third-Place Tournament": return new RoundModel { Name = title, Type = RoundType.Repechage, SubType = RoundType.FinalRound, Group = 0, Description = "Bronze Medal" };
            case "Tie-Breaker": return new RoundModel { Name = title, Type = RoundType.PlayOff, SubType = RoundType.None, Group = 0, Description = null };
            //case "Uneven Bars": return new RoundModel { Name = title, Type = RoundType.UnevenBars, SubType = RoundType.None, Group = 0, Description = null };
            case "Downhill":
            case "Downhill1":
                return new RoundModel { Name = title, Type = RoundType.Downhill };
            case "Slalom":
            case "Slalom1":
                return new RoundModel { Name = title, Type = RoundType.Slalom };
            case "Run #1":
            case "Run #11":
                return new RoundModel { Name = title, Type = RoundType.RunOne };
            case "Run #2":
            case "Run #21":
                return new RoundModel { Name = title, Type = RoundType.RunTwo };
            case "Part #1":
                return new RoundModel { Name = title, Type = RoundType.RoundOne };
            case "Part #2":
                return new RoundModel { Name = title, Type = RoundType.RoundTwo };
            case "Technical Routine":
                return new RoundModel { Name = title, Type = RoundType.TechnicalRoutine };
            case "Free Routine":
                return new RoundModel { Name = title, Type = RoundType.FreeRoutine };
            default: return null;
        }
    }

    public TableModel MapGroup(string title, string html)
    {
        var group = new TableModel
        {
            Title = title,
            Html = html,
            Round = new RoundModel()
        };

        switch (title)
        {
            case "A Final": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "B Final": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Barrage for 1/2": group.Number = 0; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = false; break;
            case "Consolation Final": group.Number = 0; group.Round.Name = title; group.Round.Type = RoundType.Final; group.IsGroup = false; break;
            case "Final A": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Final B": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Final C": group.Number = 3; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Final D": group.Number = 4; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Final E": group.Number = 5; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Final F": group.Number = 6; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Final Heat": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.Final; group.IsGroup = true; break;
            case "Final Heat One": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.Final; group.IsGroup = true; break;
            case "Final Heat Two": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.Final; group.IsGroup = true; break;
            case "Final Pool Barrage 2-3": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Final Pool, Barrage #1 1-2": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Final Pool, Barrage #2 1-2": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Final Pool, Barrage 1-2": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Final Pool, Barrage 1-3": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Final Pool, Barrage 1-4": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Final Pool, Barrage 2-3": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Final Pool, Barrage 2-4": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Final Pool, Barrage 2-5": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Final Pool, Barrage 3-4": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Final Pool, Barrage 3-5": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Final Pool, Barrage 4-5": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Final Pool, Barrage 6-7": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Group A": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Group B": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Group C": group.Number = 3; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Group D": group.Number = 4; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Group E": group.Number = 5; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Group F": group.Number = 6; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Group G": group.Number = 7; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat #1": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat #1 Re-Race": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Heat #10": group.Number = 10; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat #11": group.Number = 11; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat #12": group.Number = 12; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat #13": group.Number = 13; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat #14": group.Number = 14; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat #15": group.Number = 15; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat #16": group.Number = 16; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat #17": group.Number = 17; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat #2": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat #3": group.Number = 3; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat #4": group.Number = 4; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat #5": group.Number = 5; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat #6": group.Number = 6; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat #7": group.Number = 7; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat #8": group.Number = 8; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat #9": group.Number = 9; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat 1/2": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat 1-6": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat 3/4": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat 5/6": group.Number = 3; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat 5-8": group.Number = 3; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat 7/8": group.Number = 4; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat 7-12": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat 9-12": group.Number = 4; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat Eight": group.Number = 8; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat Eighteen": group.Number = 18; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat Eleven": group.Number = 11; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat Fifteen": group.Number = 15; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat Five": group.Number = 5; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat Four": group.Number = 4; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat Fourteen": group.Number = 14; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat Nine": group.Number = 9; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat One": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat One Re-Run": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Heat Seven": group.Number = 7; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat Seventeen": group.Number = 17; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat Six": group.Number = 6; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat Six Re-Run": group.Number = 6; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat Sixteen": group.Number = 16; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Heat Ten": group.Number = 10; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat Thirteen": group.Number = 13; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat Three": group.Number = 3; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat Three Re-run": group.Number = 3; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Heat Twelve": group.Number = 12; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat Two": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Heat Two Re-run": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Jump-Off for 1-2": group.Number = 0; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = false; break;
            case "Jump-off for 2-4": group.Number = 0; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = false; break;
            case "Jump-Off for 3-4": group.Number = 0; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = false; break;
            case "Jump-off for 3-5": group.Number = 0; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = false; break;
            case "Jump-off for 6-7": group.Number = 0; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = false; break;
            case "Match 1/2": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Match 1-6": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Match 3/4": group.Number = 3; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Match 5-7": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Match 5-8": group.Number = 3; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Match 7-10": group.Number = 3; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Match 9-12": group.Number = 4; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Pool 1": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Pool 1, Barrage": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 1, Barrage 2-5": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 1, Barrage 3-4": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 1, Barrage 3-5": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 1, Barrage 3-6": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 1, Barrage 4-5": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 1, Barrage 4-6": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 1, Barrage 6-8": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 10": group.Number = 10; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 10, Barrage 2-4": group.Number = 10; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 10, Barrage 3-4": group.Number = 10; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 11": group.Number = 11; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Pool 11, Barrage 2-4": group.Number = 11; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 11, Barrage 3-5": group.Number = 11; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 12": group.Number = 12; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Pool 12, Barrage 2-4": group.Number = 12; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 12, Barrage 3-4": group.Number = 12; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 13": group.Number = 13; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Pool 14": group.Number = 14; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Pool 15": group.Number = 15; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Pool 16": group.Number = 16; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Pool 17": group.Number = 17; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Pool 2": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Pool 2, Barrage 2-4": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 2, Barrage 3-4": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 2, Barrage 3-5": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 2, Barrage 3-7": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 2, Barrage 4-5": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 2, Barrage 4-6": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 2, Barrage 5-6": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 2, Barrage 5-8": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 2, Barrage 6-12": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 3": group.Number = 3; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Pool 3, Barrage 3-5": group.Number = 3; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 3, Barrage 4-5": group.Number = 3; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 3, Barrage 4-6": group.Number = 3; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 3, Barrage 5-6": group.Number = 3; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 3, Barrage 6-8": group.Number = 3; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 4": group.Number = 4; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Pool 4, Barrage": group.Number = 4; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 4, Barrage 2-4": group.Number = 4; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 4, Barrage 2-5": group.Number = 4; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 4, Barrage 3-4": group.Number = 4; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 4, Barrage 3-5": group.Number = 4; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 4, Barrage 4-5": group.Number = 4; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 4, Barrage 4-6": group.Number = 4; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 4, Barrage 6-8": group.Number = 4; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 5": group.Number = 5; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Pool 5, Barrage 2-4": group.Number = 5; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 5, Barrage 3-4": group.Number = 5; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 5, Barrage 3-6": group.Number = 5; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 5, Barrage 4-6": group.Number = 5; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 5, Barrage 5-7": group.Number = 5; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 6": group.Number = 6; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Pool 6, Barrage": group.Number = 6; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 6, Barrage 3-4": group.Number = 6; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 6, Barrage 3-5": group.Number = 6; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 6, Barrage 4-5": group.Number = 6; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 6, Barrage 5-6": group.Number = 6; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 7": group.Number = 7; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Pool 7, Barrage 2-4": group.Number = 7; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 7, Barrage 3-5": group.Number = 7; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 7, Barrage 4-6": group.Number = 7; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 8": group.Number = 8; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Pool 8, Barrage 2-4": group.Number = 8; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 8, Barrage 3-4": group.Number = 8; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 8, Barrage 3-5": group.Number = 8; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 8, Barrage 4-5": group.Number = 8; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Pool 9": group.Number = 9; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Pool A": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Pool B": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Pool Five": group.Number = 5; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Pool Four": group.Number = 4; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Pool One": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Pool Three": group.Number = 3; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Pool Two": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Race Eight": group.Number = 8; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Race Five": group.Number = 5; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Race Four": group.Number = 4; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Race Nine": group.Number = 9; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Race One": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Race Seven": group.Number = 7; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Race Six": group.Number = 6; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Race Ten": group.Number = 10; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Race Three": group.Number = 3; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Race Two": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Repêchage Final": group.Number = 0; group.Round.Name = title; group.Round.Type = RoundType.Final; group.IsGroup = false; break;
            case "Re-run of Heat Two": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = true; break;
            case "Round One Pool Four": group.Number = 4; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Round One Pool One": group.Number = 1; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Round One Pool Three": group.Number = 3; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Round One Pool Two": group.Number = 2; group.Round.Name = title; group.Round.Type = RoundType.None; group.IsGroup = true; break;
            case "Swim-Off": group.Number = 0; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = false; break;
            case "Swim-Off for 16th Place": group.Number = 0; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = false; break;
            case "Swim-Off for 16th Place - Race 1": group.Number = 0; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = false; break;
            case "Swim-Off for 16th Place - Race 2": group.Number = 0; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = false; break;
            case "Swim-Off for 8th Place": group.Number = 0; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = false; break;
            case "Swim-Off for Places 7-8": group.Number = 0; group.Round.Name = title; group.Round.Type = RoundType.PlayOff; group.IsGroup = false; break;
        }

        return group;
    }
}