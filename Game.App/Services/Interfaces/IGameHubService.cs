namespace Game.SignalR.Connector.Services.Interfaces
{
  public interface IGameHubService
  {
    Task SendChatHistory(string jsonData);
    Task SendPlayerList(string jsonData);
  }
}