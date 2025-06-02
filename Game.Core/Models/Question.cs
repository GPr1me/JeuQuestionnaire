using Game.Core.Models.Interfaces;

namespace Game.Core.Models
{
  public class Question : IMarkupText
  {
    public required Guid Id { get; init; }
    public required string Text { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public List<Answer> Options { get; set; } = [];
    public Answer? CorrectAnswer { get => Options.FirstOrDefault(o => o.IsCorrect); }
    public string CorrectAnswerAlphanumeric
    {
      get
      {
        for (int i = 0; i < Options.Count; i++)
        {
          if (Options[i].IsCorrect)
            return responses[i];
        }
        return string.Empty;
      }
    }
    private static readonly string[] responses = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
  }
}