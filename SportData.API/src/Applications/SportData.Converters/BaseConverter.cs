﻿namespace SportData.Converters;

using System.Text;

using Dasync.Collections;

using HtmlAgilityPack;

using Microsoft.Extensions.Logging;

using SportData.Common.Extensions;
using SportData.Data.Models.Entities.Crawlers;
using SportData.Services.Data.CrawlerStorageDb.Interfaces;
using SportData.Services.Interfaces;

public abstract class BaseConverter
{
    private readonly ICrawlersService crawlersService;
    private readonly ILogsService logsService;
    private readonly IGroupsService groupsService;
    private readonly IZipService zipService;

    public BaseConverter(ILogger<BaseConverter> logger, ICrawlersService crawlersService, ILogsService logsService, IGroupsService groupsService, IZipService zipService)
    {
        this.Logger = logger;
        this.crawlersService = crawlersService;
        this.logsService = logsService;
        this.groupsService = groupsService;
        this.zipService = zipService;
    }

    protected ILogger<BaseConverter> Logger { get; }

    protected abstract Task ProcessGroupAsync(Group group);

    public async Task ConvertAsync(string crawlerName)
    {
        this.Logger.LogInformation($"Converter: {crawlerName} start.");

        try
        {
            var crawlerId = await this.crawlersService.GetCrawlerIdAsync(crawlerName);
            var identifiers = await this.logsService.GetLogIdentifiersAsync(crawlerId);

            //identifiers = new List<Guid>
            //{
            //};

            await identifiers.ParallelForEachAsync(async identifier =>
            {
                try
                {
                    var group = await this.groupsService.GetGroupAsync(identifier);
                    var zipModels = this.zipService.UnzipGroup(group.Content);
                    foreach (var document in group.Documents)
                    {
                        var zipModel = zipModels.First(z => z.Name == document.Name);
                        document.Content = zipModel.Content;
                    }

                    await this.ProcessGroupAsync(group);
                }
                catch (Exception ex)
                {
                    this.Logger.LogError(ex, $"Group was not process: {identifier};");
                }
            }, maxDegreeOfParallelism: 1);
        }
        catch (Exception ex)
        {
            this.Logger.LogError(ex, $"Failed to process documents from converter: {crawlerName};");
        }

        this.Logger.LogInformation($"Converter: {crawlerName} end.");
    }

    protected HtmlDocument CreateHtmlDocument(Document document)
    {
        var encoding = Encoding.GetEncoding(document.Encoding);
        var html = encoding.GetString(document.Content).Decode();
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);

        return htmlDocument;
    }
}