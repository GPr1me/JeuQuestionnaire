namespace Game.Core.Models
{
  public class Answer
  {
    public required Guid Id { get; init; }
    public required string Text { get; set; }
    public required bool IsCorrect { get; set; }
  }
}
