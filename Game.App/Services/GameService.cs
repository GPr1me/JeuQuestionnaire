using Game.App.Enum;
using Game.App.Services.Interfaces;
using Game.Core.Models;
using System.Collections.Concurrent;

namespace Game.App.Services
{
  public class GameService : IGameService
  {
    private readonly IGameLinkService _gameLinkService;
    public GameState State { get; private set; } = GameState.NotStarted;

    private async Task SetState(GameState state)
    {
      State = state;
      await _gameLinkService.SendState(state);
    }

    public ConcurrentDictionary<string, Player> Players { get; private set; } = [];
    private readonly List<Question> _questions = [];
    private int _currentQuestionIndex = -1;

    public GameService(IGameLinkService gameLinkService)
    {
      _gameLinkService = gameLinkService;
    }

    public void AddPlayer(string playerId)
    {
      Players.TryAdd(playerId, new Player(playerId));
    }

    public void RemovePlayer(string playerId)
    {
      Players.TryRemove(playerId, out _);
    }

    public async Task SendMessage(string playerId, string message)
    {
      if (Players.ContainsKey(playerId))
      {
        await SubmitAnswer(playerId, message);
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

    public async Task SendPlayerList() => await _gameLinkService.SendPlayerList(Players.Values);

    public void AddQuestion(Question question) => _questions.Add(question);

    public async Task<Question?> GetCurrentQuestion()
    {
      if (_currentQuestionIndex >= 0 && _currentQuestionIndex < _questions.Count)
      {
        await SetState(GameState.InProgress);
        return (Question?) _questions[_currentQuestionIndex];
      }
      else
      {
        await SetState(GameState.NotStarted);
        return null;
      }
    }

    public async Task StartGame(List<Question> questions)
    {
      _questions.Clear();
      _currentQuestionIndex = 0;
      foreach (var player in Players)
      {
        player.Value.ClearLastMessage();
      }

      _questions.AddRange(questions);
      await SetState(GameState.InProgress);

      await SendPlayerList();
    }

    public async Task NextQuestion()
    {
      if (State != GameState.InProgress) return;

      await CompileScores();

      _currentQuestionIndex++;
      if (_currentQuestionIndex < _questions.Count - 1)
      {
        foreach (var player in Players)
        {
          player.Value.ClearLastMessage();
        }
      }

      await _gameLinkService.SendGoSignal();

      await SendPlayerList();
    }

    public async Task EndGame()
    {
      _currentQuestionIndex = -1;
      _questions.Clear();
      await SetState(GameState.Finished);
      await _gameLinkService.SendStopSignal();

      await SendPlayerList();
    }

    public async Task ResetGame()
    {
      _currentQuestionIndex = -1;
      _questions.Clear();
      await SetState(GameState.NotStarted);

      foreach (var player in Players)
      {
        player.Value.Score = 0;
        player.Value.ClearLastMessage();
      }

      await SendPlayerList();
    }

    public async Task SubmitAnswer(string playerId, string answer)
    {
      Players[playerId].LastMessage = answer;
      Players[playerId].LastMessageDate = DateTime.Now;

      await SendPlayerList();
    }

    public async Task SendDelayGoSignal(int delaySec)
    {
      if (_questions[_currentQuestionIndex] != null)
      {
        for (int i = 0; i < delaySec; i++)
        {
          await _gameLinkService.SendGetPreparedSignal(delaySec);
          await Task.Delay(1000); // Simulate a delay
        }
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

    private async Task CompileScores()
    {
      var answer = _questions[_currentQuestionIndex].CorrectAnswerAlphanumeric;

      foreach (var player in Players.Values)
      {
        if (player.LastMessage == answer)
        {
          player.Score++;
          await _gameLinkService.SendScore(player.Id, player.Score);
        }
      }
      await SendPlayerList();
    }
  }
}
