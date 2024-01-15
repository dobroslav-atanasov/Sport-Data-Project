namespace SportData.Services.Data.OlympicGamesDb.Interfaces;

using SportData.Data.Models.Entities.OlympicGames;

public interface IParticipantsService
{
    Task<Participant> AddOrUpdateAsync(Participant participant);

    Task<Participant> GetAsync(int number, int eventId);

    Task<Participant> GetAsync(int number, int eventId, int nocId);
}