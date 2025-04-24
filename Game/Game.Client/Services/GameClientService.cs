using Game.Client.Services.Interfaces;
using Game.Core.Models;
using System.Net.Http.Json;

namespace Game.Client.Services
{
  public class GameClientService : IGameClientService
  {
    private readonly HttpClient _httpClient;

    public GameClientService(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    public async Task<List<Question>> GetQuestions()
    {
      return await _httpClient.GetFromJsonAsync<List<Question>>("api/questions");
    }

    public async Task<Question?> GetCurrentQuestion()
    {
      return await _httpClient.GetFromJsonAsync<Question>($"api/game/currentQuestion");
    }

    public async Task NextQuestion() => await _httpClient.PostAsync("api/game/next", null);
    public async Task StartGame(List<Question> questions) => await _httpClient.PostAsJsonAsync("api/game/start", questions);
    public async Task EndGame() => await _httpClient.PostAsync("api/game/end", null);
  }
}
