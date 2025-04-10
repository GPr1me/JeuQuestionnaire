namespace Game.SignalR.Connector.Services.Interfaces
{
  public interface IGameLinkService
  {
    Task SendChatHistory(ICollection<string> chatHistory);
    Task SendPlayerList(ICollection<string> playerList);
  }
}