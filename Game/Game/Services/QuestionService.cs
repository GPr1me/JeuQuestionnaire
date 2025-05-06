using Game.App.Services.Interfaces;
using Game.Client.Services.Interfaces;
using Game.Core.Models;
using Microsoft.AspNetCore.Components.Forms;

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

    public Task<string> UploadContent(IBrowserFile file)
    {
      // This method is not implemented in the executor, so it will throw a NotImplementedException
      throw new NotImplementedException("UploadContent is not implemented in the executor.");
    }
  }
}
