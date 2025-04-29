namespace Game.Core.Models
{
  public static class HubEnpoints
  {
    public const string RegisterPlayer = "RegisterPlayer";
    public const string UnregisterPlayer = "UnregisterPlayer";
    public const string SendMessage = "SendMessage";
    public const string PlayerListUpdated = "PlayerListUpdated";
    public const string ChatHistoryUpdated = "ChatHistoryUpdated";
    public const string GetPrepared = "GetPrepared";
    public const string Go = "Go";
    public const string Stop = "Stop";
    public const string ScoreUpdated = "ScoreUpdated";
    public const string StateUpdated = "StateUpdated";
    public const string CurrentQuestionUpdated = "CurrentQuestionUpdated";
    public const string GamePlayerUpdated = "GamePlayerUpdated";
  }
}
