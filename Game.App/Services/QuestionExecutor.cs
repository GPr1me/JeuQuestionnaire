using Game.App.Repos;
using Game.App.Services.Interfaces;
using Game.App.Validators;
using Game.Core.Models;

namespace Game.App.Services
{
  public class QuestionExecutor : IQuestionExecutor
  {
    private readonly IQuestionRepository _repository;
    private readonly IQuestionValidator _validator;

    public QuestionExecutor(IQuestionRepository repository, IQuestionValidator validator)
    {
      _repository = repository;
      _validator = validator;
    }

    public async Task<Question> Get(Guid id)
    {
      var question = await _repository.Get(id);

      _validator.Validate(question);

      return question;
    }
    public async Task<List<Question>> GetAll()
    {
      return await _repository.GetAll();
    }

    public async Task Create(Question question)
    {
      _validator.Validate(question);
      await _repository.Create(question);
    }
    public async Task Update(Question question)
    {
      _validator.Validate(question);
      await _repository.Update(question);
    }
    public async Task Delete(Guid id)
    {
      var question = await _repository.Get(id);

      _validator.Validate(question);

      await _repository.Delete(id);
    }
  }
}
