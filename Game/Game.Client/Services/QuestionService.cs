using Game.Core.Models;
using System.Net.Http.Json;

namespace Game.Client.Services
{
  public class QuestionService : IQuestionService
  {
    private readonly HttpClient _httpClient;

    public QuestionService(HttpClient httpClient)
    {
      _httpClient = httpClient;
      //_httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("ServerUrl")!);
    }

    public async Task<List<Question>> GetAll()
    {
      return await _httpClient.GetFromJsonAsync<List<Question>>("api/questions");
    }

    public async Task<Question> Get(Guid id)
    {
      return await _httpClient.GetFromJsonAsync<Question>($"api/questions/{id}");
    }

    public async Task Create(Question question)
    {
      await _httpClient.PostAsJsonAsync("api/questions", question);
    }

    public async Task Update(Question question)
    {
      await _httpClient.PutAsJsonAsync($"api/questions/{question.Id}", question);
    }

    public async Task Delete(Guid id)
    {
      await _httpClient.DeleteAsync($"api/questions/{id}");
    }
  }
}