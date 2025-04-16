using Game.Core.Models;

namespace Game.App.Services.Interfaces
{
  public interface IQuestionExecutor
  {
    Task Create(Question question);
    Task Delete(Guid id);
    Task<Question> Get(Guid id);
    Task<List<Question>> GetAll();
    Task Update(Question question);
  }
}