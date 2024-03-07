﻿namespace SportData.Converters.OlympicGames;

using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using SportData.Data.Models.Entities.Crawlers;
using SportData.Services.Data.CrawlerStorageDb.Interfaces;
using SportData.Services.Interfaces;

public class ParticipantConverter : BaseOlympediaConverter
{
    public ParticipantConverter(ILogger<BaseConverter> logger, ICrawlersService crawlersService, ILogsService logsService, IGroupsService groupsService, IZipService zipService,
        IRegExpService regExpService, INormalizeService normalizeService, IOlympediaService olympediaService)
        : base(logger, crawlersService, logsService, groupsService, zipService, regExpService, normalizeService, olympediaService)
    {
    }

    protected override async Task ProcessGroupAsync(Group group)
    {
        try
        {
            var document = this.CreateHtmlDocument(group.Documents.Single(x => x.Order == 1));
            var originalEventName = document.DocumentNode.SelectSingleNode("//ol[@class='breadcrumb']/li[@class='active']").InnerText;
            var gameCacheModel = this.FindGame(document);
            var discipline = this.FindDiscipline(document);
            var eventModel = this.CreateEventModel(originalEventName, gameCacheModel, discipline);
            if (eventModel != null)
            {
                //var eventCacheModel = this.DataCacheService
                //    .EventCacheModels
                //    .FirstOrDefault(x => x.OriginalName == eventModel.OriginalName && x.GameId == eventModel.GameId && x.DisciplineId == eventModel.DisciplineId);

                //if (eventCacheModel != null)
                //{
                //    var trRows = document.DocumentNode.SelectSingleNode("//table[@class='table table-striped']").Elements("tr");
                //    var indexes = this.GetHeaderIndexes(document);
                //    if (eventCacheModel.IsTeamEvent)
                //    {
                //        Team team = null;
                //        foreach (var trRow in trRows)
                //        {
                //            var tdNodes = trRow.Elements("td").ToList();
                //            var countryCode = this.OlympediaService.FindNOCCode(trRow.OuterHtml);
                //            if (countryCode != null && !trRow.InnerHtml.ToLower().Contains("coach"))
                //            {
                //                var nocCacheModel = this.DataCacheService.NOCCacheModels.FirstOrDefault(x => x.Code == countryCode);
                //                team = new Team
                //                {
                //                    NOCId = nocCacheModel.Id,
                //                    EventId = eventCacheModel.Id,
                //                    Name = tdNodes[indexes[ConverterConstants.INDEX_NAME]].InnerText.Trim(),
                //                    Medal = this.OlympediaService.FindMedal(trRow.OuterHtml),
                //                    FinishStatus = this.OlympediaService.FindStatus(trRow.OuterHtml)
                //                };

                //                team = await this.teamsService.AddOrUpdateAsync(team);
                //            }

                //            if (trRow.InnerHtml.ToLower().Contains("coach"))
                //            {
                //                var athleteModel = this.OlympediaService.FindAthlete(trRow.OuterHtml);
                //                var coach = await this.athletesService.GetAsync(athleteModel.Code);
                //                if (coach != null)
                //                {
                //                    team.CoachId = coach.Id;
                //                    team = await this.teamsService.AddOrUpdateAsync(team);
                //                }
                //            }
                //            else
                //            {
                //                var athleteNumbers = this.OlympediaService.FindAthletes(trRow.OuterHtml);
                //                var nocCode = this.DataCacheService.NOCCacheModels.FirstOrDefault(x => x.Id == team.NOCId);
                //                foreach (var athleteModel in athleteNumbers)
                //                {
                //                    var participant = await this.CreateParticipantAsync(athleteModel.Code, nocCode.Code, team.Medal, team.FinishStatus, eventCacheModel, gameCacheModel);

                //                    if (participant != null)
                //                    {
                //                        await this.squadsService.AddOrUpdateAsync(new Squad { ParticipantId = participant.Id, TeamId = team.Id });
                //                    }
                //                }
                //            }
                //        }
                //    }
                //    else
                //    {
                //        foreach (var trRow in trRows)
                //        {
                //            var athleteModel = this.OlympediaService.FindAthlete(trRow.OuterHtml);
                //            var countryCode = this.OlympediaService.FindNOCCode(trRow.OuterHtml);
                //            var medalType = this.OlympediaService.FindMedal(trRow.OuterHtml);
                //            var finishStatus = this.OlympediaService.FindStatus(trRow.OuterHtml);
                //            if (athleteModel != null && countryCode != null)
                //            {
                //                await this.CreateParticipantAsync(athleteModel.Code, countryCode, medalType, finishStatus, eventCacheModel, gameCacheModel);
                //            }
                //        }
                //    }
                //}
            }
        }
        catch (Exception ex)
        {
            this.Logger.LogError(ex, $"Failed to process group: {group.Identifier}");
        }
    }

    //private async Task<Participant> CreateParticipantAsync(int athleteNumber, string countryCode, MedalType medalType, FinishStatus finishStatus, EventCacheModel eventCacheModel, GameCacheModel gameCacheModel)
    //{
    //    //var athlete = await this.athletesService.GetAsync(athleteNumber);
    //    //if (athlete != null)
    //    //{
    //    //    var nocCacheModel = this.DataCacheService.NOCCacheModels.FirstOrDefault(c => c.Code == countryCode);
    //    //    if (nocCacheModel != null)
    //    //    {
    //    //        var participant = new Participant
    //    //        {
    //    //            AthleteId = athlete.Id,
    //    //            EventId = eventCacheModel.Id,
    //    //            NOCId = nocCacheModel.Id,
    //    //            Number = athleteNumber,
    //    //            Medal = medalType,
    //    //            FinishStatus = finishStatus,
    //    //        };

    //    //        if (athlete.BirthDate.HasValue)
    //    //        {
    //    //            this.CalculateAge(gameCacheModel.OpenDate ?? gameCacheModel.StartCompetitionDate.Value, athlete.BirthDate.Value, participant);
    //    //        }

    //    //        participant = await this.participantsService.AddOrUpdateAsync(participant);

    //    //        return participant;
    //    //    }
    //    //}

    //    return null;
    //}

    //private void CalculateAge(DateTime startDate, DateTime endDate, Participant participant)
    //{
    //    var totalDays = (startDate - endDate).TotalDays;
    //    var year = (int)Math.Floor(totalDays / 365.25);
    //    var newYear = endDate.AddYears(year);
    //    var days = (startDate - newYear).TotalDays;

    //    participant.AgeYears = year;
    //    participant.AgeDays = (int)days;
    //}
}