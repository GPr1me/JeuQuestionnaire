using Game.Core.Models;

namespace Game.Client.Services.Interfaces
{
  public interface IQuestionService
  {
    Task Create(Question question);
    Task Delete(Guid id);
    Task<Question> Get(Guid id);
    Task<List<Question>> GetAll();
    Task Update(Question question);
  }
}