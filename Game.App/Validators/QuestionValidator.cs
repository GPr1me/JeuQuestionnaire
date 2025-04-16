using Game.Core.Models;

namespace Game.App.Validators
{
  public class QuestionValidator : IQuestionValidator
  {
    public void Validate(Question? question)
    {
      if (question == null)
        throw new ArgumentNullException(nameof(question), "Question not found.");
      if (string.IsNullOrWhiteSpace(question.Text))
        throw new ArgumentException("Question text cannot be empty.", nameof(question.Text));
      if (question.Options == null || question.Options.Count != 4)
        throw new ArgumentException("Question must have 4 options.", nameof(question.Options));
      if (question.CorrectAnswer == null)
        throw new ArgumentException("Correct answer cannot be null.", nameof(question.CorrectAnswer));
      if (!question.Options.Contains(question.CorrectAnswer))
        throw new ArgumentException("Correct answer must be one of the options.", nameof(question.CorrectAnswer));
    }
  }
}
