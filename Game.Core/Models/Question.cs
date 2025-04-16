namespace Game.Core.Models
{
  public class Question
  {
    public required Guid Id { get; init; }
    public required string Text { get; set; }
    public List<Answer> Options { get; set; } = [];
    public required Answer CorrectAnswer { get; set; }
  }
}