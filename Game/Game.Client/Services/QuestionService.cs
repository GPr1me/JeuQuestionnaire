using Game.Client.Services.Interfaces;
using Game.Core.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace Game.Client.Services
{
  public class QuestionService : IQuestionService
  {
    private readonly HttpClient _httpClient;

    public QuestionService(HttpClient httpClient)
    {
      _httpClient = httpClient;
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

    public async Task<string> UploadContent(IBrowserFile file)
    {
      using var content = new MultipartFormDataContent();
      var fileContent = new StreamContent(file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024)); // 10 MB limit
      fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
      content.Add(fileContent, "file", file.Name);

      var response = await _httpClient.PostAsync("api/questions/uploadContent", content); // Replace with your API endpoint
      if (response.IsSuccessStatusCode)
      {
        return await response.Content.ReadAsStringAsync();
      }
      else
      {
        throw new Exception("File upload failed");
      }
    }
  }
}