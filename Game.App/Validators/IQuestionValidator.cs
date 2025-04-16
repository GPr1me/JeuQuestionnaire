using Game.Core.Models;

namespace Game.App.Validators
{
  public interface IQuestionValidator
  {
    void Validate(Question? question);
  }
}