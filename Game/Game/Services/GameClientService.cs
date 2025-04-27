using Game.App.Services.Interfaces;
using Game.Client.Services.Interfaces;
using Game.Core.Models;

namespace Game.Services
{
  public class GameClientService : IGameClientService
  {
    private readonly IGameService _gameService;
    private readonly IQuestionService _questionService;

    public GameClientService(IGameService gameService, IQuestionService questionService)
    {
      _gameService = gameService;
      _questionService = questionService;
    }

    public Task EndGame()
    {
      _gameService.EndGame();
      return Task.CompletedTask;
    }

    public async Task<Question?> GetCurrentQuestion()
    {
      return await _gameService.GetCurrentQuestion();
    }

    public async Task<List<Question>> GetQuestions()
    {
      return await _questionService.GetAll();
    }

    public Task NextQuestion()
    {
      _gameService.NextQuestion();
      return Task.CompletedTask;
    }

    public Task StartGame(List<Question> questions)
    {
      _gameService.StartGame(questions);
      return Task.CompletedTask;
    }

    public Task ResetGame()
    {
      _gameService.ResetGame();
      return Task.CompletedTask;
    }

    public Task ShowGameStats()
    {
      _gameService.SendScores();
      return Task.CompletedTask;
    }
  }
}
