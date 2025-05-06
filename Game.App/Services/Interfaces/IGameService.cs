using Game.Core.Models;
using System.Collections.Concurrent;

namespace Game.App.Services.Interfaces
{
  public interface IGameService
  {
    ConcurrentDictionary<string, Player> Players { get; }

    void AddPlayer(string playerId);
    void RemovePlayer(string playerId);
    void RenamePlayerById(string playerId, string newName);
    void RenamePlayerByValue(string oldName, string newName);
    Task SendMessage(string playerId, string message);
    Task SendPlayerList();


    void AddQuestion(Question question);
    Task<Question?> GetCurrentQuestion();
    Task StartGame(List<Question> questions);
    Task NextQuestion();
    Task EndGame();
    Task SubmitAnswer(string playerId, string answer);
    Task ResetGame();
    Task SendScores();
    Task SendDelayGoSignal(int delaySec);
    void RegisterGameMaster(string playerId);
  }
}