using Game.Core.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace Game.Client.Services.Interfaces
{
  public interface IQuestionService
  {
    Task Create(Question question);
    Task Delete(Guid id);
    Task<Question> Get(Guid id);
    Task<List<Question>> GetAll();
    Task Update(Question question);
    Task<string> UploadContent(IBrowserFile file);
  }
}