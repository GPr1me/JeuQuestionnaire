using Game.Core.Models.Interfaces;

namespace Game.Core.Models
{
  public class Answer : IMarkupText
  {
    public required Guid Id { get; init; }
    public required string Text { get; set; }
    public required bool IsCorrect { get; set; }
  }
}
