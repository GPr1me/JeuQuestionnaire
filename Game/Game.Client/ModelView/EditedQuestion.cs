using Game.Core.Models;

namespace Game.Client.ModelView
{
  public record EditedQuestion
  {
    public Question? Question { get; set; }
    public bool IsNew { get; set; } = true;
  }
}
