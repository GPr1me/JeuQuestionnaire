namespace Game.App.Services.Interfaces
{
  public interface IGameHubService
  {
    Task SendChatHistory(string jsonData);
    Task SendGetPreparedSignal(int delay);
    Task SendGoSignal();
    Task SendPlayerList(string jsonData);
    Task SendScore(string playerId, string jsonData);
    Task SendStopSignal();
  }
}