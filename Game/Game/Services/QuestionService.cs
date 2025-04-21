using Game.App.Services.Interfaces;
using Game.Client.Services;
using Game.Core.Models;

namespace Game.Services
{
  public class QuestionService : IQuestionService
  {
    private readonly IQuestionExecutor _executor;

    public QuestionService(IQuestionExecutor executor)
    {
      _executor = executor;
    }
    public async Task Create(Question question)
    {
      await _executor.Create(question);
    }

    public async Task Delete(Guid id)
    {
      await _executor.Delete(id);
    }

    public async Task<Question> Get(Guid id)
    {
      return await _executor.Get(id);
    }

    public async Task<List<Question>> GetAll()
    {
      return await _executor.GetAll();
    }

    public async Task Update(Question question)
    {
      await _executor.Update(question);
    }
  }
}
