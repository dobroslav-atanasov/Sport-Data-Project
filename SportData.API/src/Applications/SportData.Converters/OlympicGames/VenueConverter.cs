﻿namespace SportData.Converters.OlympicGames;

using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using SportData.Data.Models.Entities.Crawlers;
using SportData.Services.Data.CrawlerStorageDb.Interfaces;
using SportData.Services.Interfaces;

public class VenueConverter : BaseOlympediaConverter
{
    public VenueConverter(ILogger<BaseConverter> logger, ICrawlersService crawlersService, ILogsService logsService, IGroupsService groupsService, IZipService zipService,
        IRegExpService regExpService, INormalizeService normalizeService, IOlympediaService olympediaService)
        : base(logger, crawlersService, logsService, groupsService, zipService, regExpService, normalizeService, olympediaService)
    {
    }

    protected override async Task ProcessGroupAsync(Group group)
    {
        //try
        //{
        //    var document = this.CreateHtmlDocument(group.Documents.Single());
        //    var number = int.Parse(new Uri(group.Documents.Single().Url).Segments.Last());
        //    var fullName = document.DocumentNode.SelectSingleNode("//h1").InnerText.Decode();
        //    var name = this.RegExpService.MatchFirstGroup(document.DocumentNode.OuterHtml, @"<tr>\s*<th>Name<\/th>\s*<td>(.*?)<\/td>\s*<\/tr>")?.Decode();
        //    var cityName = this.RegExpService.MatchFirstGroup(document.DocumentNode.OuterHtml, @"<tr>\s*<th>Place<\/th>\s*<td>(.*?)<\/td>\s*<\/tr>");
        //    var englishName = this.RegExpService.MatchFirstGroup(document.DocumentNode.OuterHtml, @"<tr>\s*<th>English name<\/th>\s*<td>(.*?)<\/td>\s*<\/tr>")?.Decode();
        //    var opened = this.RegExpService.MatchFirstGroup(document.DocumentNode.OuterHtml, @"<tr>\s*<th>Opened<\/th>\s*<td>([\d]+)<\/td>\s*<\/tr>");
        //    var demolished = this.RegExpService.MatchFirstGroup(document.DocumentNode.OuterHtml, @"<tr>\s*<th>Demolished<\/th>\s*<td>([\d]+)<\/td>\s*<\/tr>");
        //    var capacity = this.RegExpService.MatchFirstGroup(document.DocumentNode.OuterHtml, @"<tr>\s*<th>Games Capacity<\/th>\s*<td>([\d]+)<\/td>\s*<\/tr>");
        //    var coordinatesMatch = this.RegExpService.Match(document.DocumentNode.OuterHtml, @"<tr>\s*<th>Coordinates<\/th>\s*<td>([\d\.\-]+),\s*([\d\.\-]+)");

        //    var venue = new Venue
        //    {
        //        CreatedOn = DateTime.UtcNow,
        //        Name = name,
        //        CityName = this.NormalizeService.NormalizeHostCityName(cityName),
        //        Number = number,
        //        EnglishName = englishName,
        //        FullName = fullName,
        //        OpenedYear = opened != null ? int.Parse(opened) : null,
        //        DemolishedYear = demolished != null ? int.Parse(demolished) : null,
        //        Capacity = capacity,
        //        LatitudeCoordinate = coordinatesMatch != null ? double.Parse(coordinatesMatch.Groups[1].Value.Trim().Replace(".", ",")) : null,
        //        LongitudeCoordinate = coordinatesMatch != null ? double.Parse(coordinatesMatch.Groups[2].Value.Trim().Replace(".", ",")) : null,
        //    };

        //    //var sport = await this.sportsService.GetAsync("Basketball");

        //    //var asd = sport.Disciplines;

        //    //await this.venuesService.AddOrUpdateAsync(venue);
        //}
        //catch (Exception ex)
        //{
        //    this.Logger.LogError(ex, $"Failed to process group: {group.Identifier}");
        //}
    }
}