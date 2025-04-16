
using Game.Core.Models;

namespace Game.App.Repos
{
  public interface IQuestionRepository
  {
    Task<Question> Get(Guid id);
    Task<List<Question>> GetAll();
    Task Create(Question question);
    Task Update(Question question);
    Task Delete(Guid id);
  }
}
