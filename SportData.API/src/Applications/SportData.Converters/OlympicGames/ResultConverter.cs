namespace SportData.Converters.OlympicGames;

using System;
using System.Threading.Tasks;

using HtmlAgilityPack;

using Microsoft.Extensions.Logging;

using SportData.Common.Constants;
using SportData.Converters.OlympicGames.SportConverters;
using SportData.Data.Models.Cache;
using SportData.Data.Models.Converters;
using SportData.Data.Models.Entities.Crawlers;
using SportData.Services.Data.CrawlerStorageDb.Interfaces;
using SportData.Services.Data.OlympicGamesDb.Interfaces;
using SportData.Services.Interfaces;

public class ResultConverter : BaseOlympediaConverter
{
    private readonly IDateService dateService;
    private readonly BasketballConverter basketballConverter;
    private readonly SkiingConverter skiingConverter;
    private readonly ArcheryConverter archeryConverter;
    private readonly GymnasticsConverter gymnasticsConverter;
    private readonly AthleticsConverter athleticsConverter;
    private readonly AquaticsConverter aquaticsConverter;

    public ResultConverter(ILogger<BaseConverter> logger, ICrawlersService crawlersService, ILogsService logsService, IGroupsService groupsService, IZipService zipService,
        IRegExpService regExpService, INormalizeService normalizeService, IOlympediaService olympediaService, IDataCacheService dataCacheService,
        IDateService dateService, BasketballConverter basketballConverter, SkiingConverter skiingConverter, ArcheryConverter archeryConverter, GymnasticsConverter gymnasticsConverter,
        AthleticsConverter athleticsConverter, AquaticsConverter aquaticsConverter)
        : base(logger, crawlersService, logsService, groupsService, zipService, regExpService, normalizeService, olympediaService, dataCacheService)
    {
        this.dateService = dateService;
        this.basketballConverter = basketballConverter;
        this.skiingConverter = skiingConverter;
        this.archeryConverter = archeryConverter;
        this.gymnasticsConverter = gymnasticsConverter;
        this.athleticsConverter = athleticsConverter;
        this.aquaticsConverter = aquaticsConverter;
    }

    protected override async Task ProcessGroupAsync(Group group)
    {
        try
        {
            var htmlDocument = this.CreateHtmlDocument(group.Documents.Single(x => x.Order == 1));
            var documents = group.Documents.Where(x => x.Order != 1).OrderBy(x => x.Order);
            var originalEventName = htmlDocument.DocumentNode.SelectSingleNode("//ol[@class='breadcrumb']/li[@class='active']").InnerText;
            var gameCache = this.GetGameFromDatabase(htmlDocument);
            var disciplineCache = this.GetDisciplineFromDatabase(htmlDocument);
            var eventModel = this.CreateEventModel(originalEventName, gameCache, disciplineCache);
            if (eventModel != null)
            {
                var eventCache = this.DataCacheService
                    .Events
                    .FirstOrDefault(x => x.OriginalName == eventModel.OriginalName && x.GameId == eventModel.GameId && x.DisciplineId == eventModel.DisciplineId);

                if (eventCache != null)
                {
                    var tables = this.ExtractTables(htmlDocument, eventCache, disciplineCache, gameCache);
                    var documentModels = this.ExtractDocuments(documents, eventCache);

                    var options = new ConvertOptions
                    {
                        Discipline = disciplineCache,
                        Event = eventCache,
                        Game = gameCache,
                        HtmlDocument = htmlDocument,
                        Tables = tables,
                        Documents = documentModels
                    };

                    switch (disciplineCache.Name)
                    {
                        case DisciplineConstants.BASKETBALL_3X3:
                            await this.basketballConverter.ProcessAsync(options);
                            break;
                        case DisciplineConstants.ALPINE_SKIING:
                            await this.skiingConverter.ProcessAsync(options);
                            break;
                        case DisciplineConstants.ARCHERY:
                            await this.archeryConverter.ProcessAsync(options);
                            break;
                        case DisciplineConstants.ARTISTIC_GYMNASTICS:
                            // TODO: Judges
                            await this.gymnasticsConverter.ProcessAsync(options);
                            break;
                        case DisciplineConstants.ARTISTIC_SWIMMING:
                            await this.aquaticsConverter.ProcessAsync(options);
                            break;
                        case DisciplineConstants.ATHLETICS:
                            // TODO: Splits
                            await this.athleticsConverter.ProcessAsync(options);
                            break;
                            //case DisciplineConstants.BADMINTON:
                            //    await this.ProcessBadmintonAsync(options);
                            //    break;
                            //case DisciplineConstants.BASEBALL:
                            //    await this.ProcessBaseballAsync(options);
                            //    break;
                            //case DisciplineConstants.BASKETBALL:
                            //    await this.ProcessBasketballAsync(options);
                            //    break;
                            //case DisciplineConstants.BASQUE_PELOTA:
                            //    await this.ProcessBasquePelotaAsync(options);
                            //    break;
                            //case DisciplineConstants.BEACH_VOLLEYBALL:
                            //    await this.ProcessBeachVolleyballAsync(options);
                            //    break;
                            //case DisciplineConstants.BIATHLON:
                            //    await this.ProcessBiathlonAsync(options);
                            //    break;
                            //case DisciplineConstants.BOBSLEIGH:
                            //    await this.ProcessBobsleighAsync(options);
                            //    break;
                            //case DisciplineConstants.BOXING:
                            //    await this.ProcessBoxingAsync(options);
                            //    break;
                            //case DisciplineConstants.CANOE_SLALOM:
                            //    await this.ProcessCanoeSlalomAsync(options);
                            //    break;
                            //case DisciplineConstants.CANOE_SPRINT:
                            //    await this.ProcessCanoeSprintAsync(options);
                            //    break;
                            //case DisciplineConstants.CRICKET:
                            //    await this.ProcessCricketAsync(options);
                            //    break;
                            //case DisciplineConstants.CROSS_COUNTRY_SKIING:
                            //    await this.ProcessCrossCountrySkiing(options);
                            //    break;
                            //case DisciplineConstants.CURLING:
                            //    await this.ProcessCurlingSkiing(options);
                            //    break;
                            //case DisciplineConstants.CYCLING_BMX_FREESTYLE:
                            //    await this.ProcessCyclingBMXFreestyleAsync(options);
                            //    break;
                            //case DisciplineConstants.CYCLING_BMX_RACING:
                            //    await this.ProcessCyclingBMXRacingAsync(options);
                            //    break;
                            //case DisciplineConstants.CYCLING_MOUNTAIN_BIKE:
                            //    await this.ProcessCyclingMountainBikeAsync(options);
                            //    break;
                            //case DisciplineConstants.CYCLING_ROAD:
                            //    await this.ProcessCyclingRoadAsync(options);
                            //    break;
                            //case DisciplineConstants.CYCLING_TRACK:
                            //    await this.ProcessCyclingTrackAsync(options);
                            //    break;
                            //case DisciplineConstants.DIVING:
                            //    await this.ProcessDivingAsync(options);
                            //    break;
                            //case DisciplineConstants.EQUESTRIAN_DRESSAGE:
                            //    TP comment in INDEX_POINTS
                            //    await this.ProcessEquestrianDressage(options);
                            //    break;
                            //case DisciplineConstants.EQUESTRIAN_DRIVING:
                            //    await this.ProcessEquestrianDriving(options);
                            //    break;
                            //case DisciplineConstants.EQUESTRIAN_EVENTING:
                            //    // TODO: Documents are not processed.
                            //    await this.ProcessEquestrianEventing(options);
                            //    break;
                            //case DisciplineConstants.EQUESTRIAN_JUMPING:
                            //    await this.ProcessEquestrianJumping(options);
                            //    break;
                            //case DisciplineConstants.EQUESTRIAN_VAULTING:
                            //    await this.ProcessEquestrianVaulting(options);
                            //    break;
                            //case DisciplineConstants.FENCING:
                            //    await this.ProcessFencingAsync(options);
                            //    break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            this.Logger.LogError(ex, $"Failed to process group: {group.Identifier}");
        }
    }

    private List<TableModel> ExtractTables(HtmlDocument htmlDocument, EventCache eventCache, DisciplineCache disciplineCache, GameCache gameCache)
    {
        var standingTableHtml = htmlDocument.DocumentNode.SelectSingleNode("//table[@class='table table-striped']")?.OuterHtml;
        var format = this.RegExpService.MatchFirstGroup(htmlDocument.DocumentNode.OuterHtml, @"<th>Format<\/th>\s*<td(?:.*?)>(.*?)<\/td>");
        var dateString = this.RegExpService.MatchFirstGroup(htmlDocument.DocumentNode.OuterHtml, @"<th>\s*Date\s*<\/th>\s*<td>(.*?)<\/td>");
        var dateModel = this.dateService.ParseDate(dateString);

        var tables = new List<TableModel>();
        var order = 1;
        var roundTableModel = new TableModel
        {
            OriginalHtml = standingTableHtml,
            Html = standingTableHtml,
            EventId = eventCache.Id,
            Order = order++,
            Title = "Standing",
            Format = format,
            From = dateModel.From,
            To = dateModel.To
        };

        this.ExtractRows(roundTableModel);
        tables.Add(roundTableModel);

        var html = htmlDocument.ParsedText;
        html = html.Replace("<table class=\"biodata\"></table>", string.Empty);

        var rounds = this.RegExpService.Matches(html, @"<h2>(.*?)<\/h2>");
        if (rounds.Any())
        {
            var matches = this.RegExpService.Matches(html, @"<h2>(.*?)<\/h2>");
            if (matches.Any())
            {
                for (int i = 0; i < matches.Count; i++)
                {
                    var pattern = $@"{matches[i].Value}#$%";
                    if (i + 1 != matches.Count)
                    {
                        pattern = $@"{matches[i].Value}@$#$@{matches[i + 1].Value}";
                    }

                    pattern = pattern.Replace("(", @"\(").Replace(")", @"\)").Replace("/", @"\/").Replace("#$%", "(.*)").Replace("@$#$@", "(.*?)");
                    var match = this.RegExpService.Match(html, pattern);
                    if (match.Success)
                    {
                        var title = this.RegExpService.MatchFirstGroup(match.Groups[0].Value, "<h2>(.*?)</h2>");
                        title = this.RegExpService.CutHtml(title);
                        dateString = this.RegExpService.MatchFirstGroup(title, @"\((.*)\)");
                        var date = this.dateService.ParseDate(dateString, gameCache.Year);
                        format = this.RegExpService.MatchFirstGroup(match.Groups[0].Value, @"<i>(.*?)<\/i>");
                        title = this.RegExpService.CutHtml(title, @"\((.*)\)")?.Trim();
                        var originalHtml = match.Groups[0].Value;

                        var table = new TableModel
                        {
                            Order = order++,
                            EventId = eventCache.Id,
                            OriginalHtml = originalHtml,
                            From = date.From,
                            To = date.To,
                            Title = title,
                            Format = format,
                            Html = $"<table>{match.Groups[2].Value}</table>",
                            Round = this.NormalizeService.MapRound(title),
                        };

                        var groupMatches = this.RegExpService.Matches(table.OriginalHtml, @"<h3>(.*?)<table class=""table table-striped"">(.*?)<\/table>(?:(?:.*?)(?:Splits<\/h4>|Split Times<\/h2>)\s*<table class=""table table-striped"">(.*?)<\/table>)?");
                        if (groupMatches.Any())
                        {
                            groupMatches.ToList().ForEach(x =>
                            {
                                var groupTitle = this.RegExpService.MatchFirstGroup(x.Groups[0].Value, "<h3>(.*?)<");
                                var groupHtml = x.Groups[0].Value;
                                var group = this.NormalizeService.MapGroup(groupTitle, groupHtml);

                                if (!string.IsNullOrEmpty(x.Groups[3].Value))
                                {
                                    group.Html = groupHtml.Replace(x.Groups[3].Value, string.Empty);
                                    group.SplitHtml = x.Groups[3].Value;
                                }

                                table.Groups.Add(group);
                            });
                        }

                        var splitMatch = this.RegExpService.Match(table.OriginalHtml, @"(?:Splits<\/h4>|Split Times<\/h2>)\s*<table class=""table table-striped"">(.*?)<\/table>");
                        if (splitMatch != null)
                        {
                            table.SplitHtml = splitMatch.Groups[0].Value;
                            table.OriginalHtml = html.Replace(splitMatch.Groups[0].Value, string.Empty);
                        }

                        if (table.Round != null)
                        {
                            this.ExtractRows(table);
                            tables.Add(table);
                        }
                    }
                }
            }
        }

        return tables;
    }

    private void ExtractRows(TableModel table)
    {
        var rows = table.HtmlDocument.DocumentNode.SelectNodes("//table[@class='table table-striped']//tr");
        if (table.Groups.Any())
        {
            foreach (var group in table.Groups)
            {
                rows = group.HtmlDocument.DocumentNode.SelectNodes("//table[@class='table table-striped']//tr");
                var headers = rows.First().Elements("th").Where(x => this.OlympediaService.FindAthlete(x.OuterHtml) == null).Select(x => x.InnerText).ToList();
                group.Headers = headers;
                group.Rows = rows;
                group.Indexes = this.OlympediaService.GetIndexes(headers);
            }
        }
        else
        {
            if (rows == null)
            {
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(table.OriginalHtml);
                rows = htmlDocument.DocumentNode.SelectNodes("//table[@class='table table-striped']//tr");
            }

            if (rows == null)
            {
                return;
            }

            var headers = rows.First().Elements("th").Where(x => this.OlympediaService.FindAthlete(x.OuterHtml) == null).Select(x => x.InnerText).ToList();
            table.Headers = headers;
            table.Rows = rows;
            table.Indexes = this.OlympediaService.GetIndexes(headers);
        }
    }

    private List<DocumentModel> ExtractDocuments(IOrderedEnumerable<Document> documents, EventCache eventCache)
    {
        var result = new List<DocumentModel>();
        foreach (var document in documents)
        {
            var htmlDocument = this.CreateHtmlDocument(document);
            var title = htmlDocument.DocumentNode.SelectSingleNode("//h1").InnerText;
            title = title.Replace(eventCache.OriginalName, string.Empty).Replace("–", string.Empty).Trim();
            var dateString = this.RegExpService.MatchFirstGroup(htmlDocument.DocumentNode.OuterHtml, @"<th>\s*Date\s*<\/th>\s*<td>(.*?)<\/td>");
            var dateModel = this.dateService.ParseDate(dateString);
            var format = this.RegExpService.MatchFirstGroup(htmlDocument.DocumentNode.OuterHtml, @"<th>Format<\/th>\s*<td(?:.*?)>(.*?)<\/td>");
            var id = int.Parse(new Uri(document.Url).Segments.Last());

            var documentModel = new DocumentModel
            {
                Id = id,
                Title = title,
                Html = htmlDocument.ParsedText,
                HtmlDocument = htmlDocument,
                From = dateModel.From,
                To = dateModel.To,
                Round = this.NormalizeService.MapRound(title),
                Format = format
            };

            var tables = htmlDocument.DocumentNode.SelectNodes("//table[@class='table table-striped']");
            if (tables != null)
            {
                var order = 1;
                foreach (var table in tables)
                {
                    var tableModel = new TableModel
                    {
                        Html = table.OuterHtml,
                        Order = order++,
                    };

                    this.ExtractRows(tableModel);
                    documentModel.Tables.Add(tableModel);
                }
            }

            result.Add(documentModel);
        }

        return result;
    }
}