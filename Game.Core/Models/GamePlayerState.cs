namespace Game.Core.Models
{
  public record GamePlayerState
  {
    public bool ShowAnswer { get; set; } = false;
    public bool ShowQuestion { get; set; } = false;
    public bool ShowScore { get; set; } = false;
    public bool ShowChat { get; set; } = false;
    public bool ShowPlayerList { get; set; } = false;
  }
}
