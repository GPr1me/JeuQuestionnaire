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
    Task SendChatHistory();
    void SendMessage(string playerId, string message);
    Task SendPlayerList();


    void AddQuestion(Question question);
    Question? GetCurrentQuestion();
    void StartGame(List<Question> questions);
    void NextQuestion();
    void EndGame();
    void SubmitAnswer(string playerId, string answer);
    Task SendGoSignal();
  }
}