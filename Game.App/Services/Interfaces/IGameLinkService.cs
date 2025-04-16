namespace Game.App.Services.Interfaces
{
  public interface IGameLinkService
  {
    Task SendChatHistory(ICollection<string> chatHistory);
    Task SendGetPreparedSignal(int delay);
    Task SendGoSignal();
    Task SendPlayerList(ICollection<string> playerList);
    Task SendScore(string playerId, int score);
    Task SendStopSignal();
  }
}