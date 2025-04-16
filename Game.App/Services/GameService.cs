using Game.App.Services.Interfaces;
using Game.Core.Models;
using System.Collections.Concurrent;

namespace Game.App.Services
{
  public class GameService : IGameService
  {
    private readonly IGameLinkService _gameLinkService;

    public ConcurrentDictionary<string, Player> Players { get; private set; } = [];
    private readonly ConcurrentDictionary<string, List<string>> _chatHistory = new();
    private readonly List<Question> _questions = [];
    private int _currentQuestionIndex = -1;

    public GameService(IGameLinkService gameLinkService)
    {
      _gameLinkService = gameLinkService;
    }

    public void AddPlayer(string playerId)
    {
      Players[playerId] = new Player(playerId);
      _chatHistory.TryAdd(playerId, []);
    }

    public void RemovePlayer(string playerId)
    {
      Players.TryRemove(playerId, out _);
      _chatHistory.TryRemove(playerId, out _);
    }

    public void SendMessage(string playerId, string message)
    {
      if (_chatHistory.TryGetValue(playerId, out List<string>? value))
      {
        value.Add(message);
      }
    }

    public void RenamePlayerByValue(string oldName, string newName)
    {
      foreach (var player in Players)
      {
        if (player.Value.Name == oldName)
        {
          Players[player.Key].Name = newName;
          break;
        }
      }
    }

    public void RenamePlayerById(string playerId, string newName) => Players[playerId].Name = newName;

    public async Task SendPlayerList() => await _gameLinkService.SendPlayerList(Players.Values.Select(p => p.Name).ToList().AsReadOnly());

    public async Task SendChatHistory() => await _gameLinkService.SendChatHistory([.. _chatHistory.Values.SelectMany(x => x)]);

    public void AddQuestion(Question question) => _questions.Add(question);

    public Question? GetCurrentQuestion() =>
        _currentQuestionIndex >= 0 && _currentQuestionIndex < _questions.Count
            ? _questions[_currentQuestionIndex]
            : null;

    public void StartGame()
    {
      _currentQuestionIndex = 0;
      foreach (var player in Players)
      {
        player.Value.ClearLastMessage();
      }
    }

    public void NextQuestion()
    {
      if (_currentQuestionIndex < _questions.Count - 1)
      {
        _currentQuestionIndex++;
        foreach (var player in Players)
        {
          player.Value.ClearLastMessage();
        }
      }
    }

    public void EndGame() => _currentQuestionIndex = -1;

    public void SubmitAnswer(string playerId, string answer)
    {
      Players[playerId].LastMessage = answer;
      Players[playerId].LastMessageDate = DateTime.Now;
    }

    public async Task SendGoSignal()
    {
      var question = GetCurrentQuestion();
      if (question != null)
      {
        await _gameLinkService.SendGoSignal();
      }
    }

    public async Task SendDelayGoSignal(int delaySec)
    {
      var question = GetCurrentQuestion();
      if (question != null)
      {
        await _gameLinkService.SendGetPreparedSignal(delaySec);
        await Task.Delay(delaySec * 1000); // Simulate a delay
        await _gameLinkService.SendGoSignal();
      }
    }

    public async Task SendScores()
    {
      foreach (var player in Players)
      {
        await _gameLinkService.SendScore(player.Key, player.Value.Score);
      }
    }

  }
}
