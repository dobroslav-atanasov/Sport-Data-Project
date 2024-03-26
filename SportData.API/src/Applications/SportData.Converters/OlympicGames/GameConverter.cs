﻿namespace SportData.Converters.OlympicGames;

using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using SportData.Common.Extensions;
using SportData.Data.Models.Entities.Crawlers;
using SportData.Data.Models.Entities.OlympicGames;
using SportData.Data.Models.Entities.OlympicGames.Enumerations;
using SportData.Data.Repositories;
using SportData.Services.Data.CrawlerStorageDb.Interfaces;
using SportData.Services.Data.OlympicGamesDb.Interfaces;
using SportData.Services.Interfaces;

public class GameConverter : BaseOlympediaConverter
{
    private readonly IDataCacheService dataCacheService;
    private readonly OlympicGamesRepository<Game> gameRepository;
    private readonly OlympicGamesRepository<City> cityRepository;

    public GameConverter(ILogger<BaseConverter> logger, ICrawlersService crawlersService, ILogsService logsService, IGroupsService groupsService, IZipService zipService,
        IRegExpService regExpService, INormalizeService normalizeService, IOlympediaService olympediaService, IDataCacheService dataCacheService,
        OlympicGamesRepository<Game> gameRepository, OlympicGamesRepository<City> cityRepository)
        : base(logger, crawlersService, logsService, groupsService, zipService, regExpService, normalizeService, olympediaService, dataCacheService)
    {
        this.dataCacheService = dataCacheService;
        this.gameRepository = gameRepository;
        this.cityRepository = cityRepository;
    }

    protected override async Task ProcessGroupAsync(Group group)
    {
        try
        {
            var document = this.CreateHtmlDocument(group.Documents.Single());
            var header = document.DocumentNode.SelectSingleNode("//h1").InnerText;

            var match = this.RegExpService.Match(header, @"(\d+)\s*(summer|winter)");
            if (match != null)
            {
                var year = int.Parse(match.Groups[1].Value);
                var olympicGameTypeEnum = match.Groups[2].Value.Trim().ToEnum<OlympicGameTypeEnum>();
                //var olympicGameTypeId = this.dataCacheService.OlympicGameTypes.FirstOrDefault(x => x.Name.Equals(match.Groups[2].Value.Trim(), StringComparison.CurrentCultureIgnoreCase)).Id;
                var numberMatch = this.RegExpService.Match(document.DocumentNode.OuterHtml, @"<th>Number and Year<\/th>\s*<td>\s*([IVXLC]+)\s*\/(.*?)<\/td>");
                var hostCityMatch = this.RegExpService.Match(document.DocumentNode.OuterHtml, @"<tr>\s*<th>Host city<\/th>\s*<td>\s*([\w'\-\s.]+),\s*([\w'\-\s]+)");
                var hostCityName = this.NormalizeService.NormalizeHostCityName(hostCityMatch?.Groups[1].Value.Trim());
                var openByMatch = this.RegExpService.Match(document.DocumentNode.OuterHtml, @"<tr>\s*<th>Officially opened by<\/th>\s*<td>(.*?)<\/td>\s*<\/tr>");
                var torchbearersMatch = this.RegExpService.Match(document.DocumentNode.OuterHtml, @"<tr>\s*<th>Torchbearer\(s\)<\/th>\s*<td>(.*?)<\/td>\s*<\/tr>");
                var athleteOathByMatch = this.RegExpService.Match(document.DocumentNode.OuterHtml, @"<tr>\s*<th>Taker of the Athlete's Oath<\/th>\s*<td>(.*?)<\/td>\s*<\/tr>");
                var judgeOathByMatch = this.RegExpService.Match(document.DocumentNode.OuterHtml, @"<tr>\s*<th>Taker of the Official's Oath<\/th>\s*<td>(.*?)<\/td>\s*<\/tr>");
                var coachOathByMatch = this.RegExpService.Match(document.DocumentNode.OuterHtml, @"<tr>\s*<th>Taker of the Coach's Oath<\/th>\s*<td>(.*?)<\/td>\s*<\/tr>");
                var olympicFlagBearersMatch = this.RegExpService.Match(document.DocumentNode.OuterHtml, @"<tr>\s*<th>Olympic Flag Bearers<\/th>\s*<td>(.*?)<\/td>\s*<\/tr>");
                var descriptionMatch = this.RegExpService.Match(document.DocumentNode.OuterHtml, @"<h2>\s*Overview\s*(?:<small><\/small>)?\s*<\/h2>\s*<div class=(?:'|"")description(?:'|"")>\s*(.*?)<\/div>");
                var bidProcessMatch = this.RegExpService.Match(document.DocumentNode.OuterHtml, @"<h2>Bid process<\/h2>\s*<div class=(?:'|"")description(?:'|"")>\s*(.*?)<\/div>");

                var game = new Game
                {
                    Year = year,
                    OlympicGameTypeId = (int)olympicGameTypeEnum,
                    Number = numberMatch?.Groups[1].Value.Trim(),
                    OfficialName = this.SetOfficialName(hostCityName, year),
                    OpenBy = openByMatch != null ? this.RegExpService.CutHtml(openByMatch.Groups[1].Value) : null,
                    Torchbearers = torchbearersMatch != null ? this.RegExpService.CutHtml(torchbearersMatch.Groups[1].Value) : null,
                    AthleteOathBy = athleteOathByMatch != null ? this.RegExpService.CutHtml(athleteOathByMatch.Groups[1].Value) : null,
                    JudgeOathBy = judgeOathByMatch != null ? this.RegExpService.CutHtml(judgeOathByMatch.Groups[1].Value) : null,
                    CoachOathBy = coachOathByMatch != null ? this.RegExpService.CutHtml(coachOathByMatch.Groups[1].Value) : null,
                    OlympicFlagBearers = olympicFlagBearersMatch != null ? this.RegExpService.CutHtml(olympicFlagBearersMatch.Groups[1].Value) : null,
                    Description = descriptionMatch != null ? this.RegExpService.CutHtml(descriptionMatch.Groups[1].Value) : null,
                    BidProcess = bidProcessMatch != null ? this.RegExpService.CutHtml(bidProcessMatch.Groups[1].Value) : null
                };

                var openDateMatch = this.RegExpService.Match(document.DocumentNode.OuterHtml, @"<tr>\s*<th>Opening ceremony<\/th>\s*<td>\s*([\d]+)\s*([A-Za-z]+)\s*(\d+)?\s*<\/td>\s*<\/tr>");
                if (openDateMatch != null)
                {
                    var day = int.Parse(openDateMatch.Groups[1].Value);
                    var month = openDateMatch.Groups[2].Value.GetMonthNumber();
                    game.OpeningDate = DateTime.ParseExact($"{day}-{month}-{(game.Year != 2020 ? game.Year : game.Year + 1)}", "d-M-yyyy", null);
                }

                var closeDateMatch = this.RegExpService.Match(document.DocumentNode.OuterHtml, @"<tr>\s*<th>Closing ceremony<\/th>\s*<td>\s*([\d]+)\s*([A-Za-z]+)\s*(\d+)?\s*<\/td>\s*<\/tr>");
                if (closeDateMatch != null)
                {
                    var day = int.Parse(closeDateMatch.Groups[1].Value);
                    var month = closeDateMatch.Groups[2].Value.GetMonthNumber();
                    game.ClosingDate = DateTime.ParseExact($"{day}-{month}-{(game.Year != 2020 ? game.Year : game.Year + 1)}", "d-M-yyyy", null);
                }

                var competitionDateMatch = this.RegExpService.Match(document.DocumentNode.OuterHtml, @"<tr>\s*<th>Competition dates<\/th>\s*<td>\s*(\d+)\s*([A-Za-z]+)?\s*–\s*(\d+)\s*([A-Za-z]+)\s*(\d+)?\s*<\/td>\s*<\/tr>");
                if (competitionDateMatch != null)
                {
                    var startDay = int.Parse(competitionDateMatch.Groups[1].Value);
                    var startMonth = competitionDateMatch.Groups[2].Value != string.Empty ? competitionDateMatch.Groups[2].Value.GetMonthNumber() : competitionDateMatch.Groups[4].Value.GetMonthNumber();
                    var endDay = int.Parse(competitionDateMatch.Groups[3].Value);
                    var endMonth = competitionDateMatch.Groups[4].Value.GetMonthNumber();

                    game.StartCompetitionDate = DateTime.ParseExact($"{startDay}-{startMonth}-{(game.Year != 2020 ? game.Year : game.Year + 1)}", "d-M-yyyy", null);
                    game.EndCompetitionDate = DateTime.ParseExact($"{endDay}-{endMonth}-{(game.Year != 2020 ? game.Year : game.Year + 1)}", "d-M-yyyy", null);
                }

                var city = await this.cityRepository.GetAsync(x => x.Name == hostCityName);
                city ??= new City { Name = hostCityName };

                game.Hosts.Add(new Host
                {
                    Game = game,
                    City = city
                });

                if (game.Year == 1956 && game.OlympicGameTypeId == 1)
                {
                    var equestrianCity = await this.cityRepository.GetAsync(x => x.Name == "Stockholm");
                    equestrianCity ??= new City { Name = "Stockholm" };
                    game.Hosts.Add(new Host
                    {
                        Game = game,
                        City = equestrianCity
                    });
                }

                var dbGame = await this.gameRepository.GetAsync(x => x.Year == year && x.OlympicGameTypeId == (int)olympicGameTypeEnum);
                if (dbGame != null)
                {
                    var equals = game.Equals(dbGame);
                    if (!equals)
                    {
                        this.gameRepository.Update(dbGame);
                        await this.gameRepository.SaveChangesAsync();
                    }
                }
                else
                {
                    await this.gameRepository.AddAsync(game);
                    await this.gameRepository.SaveChangesAsync();
                }
            }
        }
        catch (Exception ex)
        {
            this.Logger.LogError(ex, $"Failed to process group: {group.Identifier}");
        }
    }

    private string SetOfficialName(string hostCity, int year)
    {
        if (hostCity == "Rio de Janeiro" && year == 2016)
        {
            hostCity = "Rio";
        }
        else if (hostCity == "Los Angeles" && year == 2028)
        {
            hostCity = "LA";
        }
        else if (hostCity == "Milano-Cortina d'Ampezzo" && year == 2026)
        {
            hostCity = "Milano Cortina";
        }

        return $"{hostCity} {year}";
    }
}