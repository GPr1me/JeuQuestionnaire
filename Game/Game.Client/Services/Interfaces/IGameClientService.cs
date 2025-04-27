using Game.Core.Models;

namespace Game.Client.Services.Interfaces
{
  public interface IGameClientService
  {
    Task<Question?> GetCurrentQuestion();
    Task<List<Question>> GetQuestions();
    Task NextQuestion();
    Task StartGame(List<Question> questions);
    Task EndGame();
    Task ResetGame();
    Task ShowGameStats();
  }
}