using System.Net;

namespace Game.App.Services.Interfaces
{
  public interface IGameService
  {
    List<string> Players { get; }

    void AddPlayer(IPAddress? playerIp);
    void RemovePlayer(IPAddress? playerIp);
    Task SendChatHistory();
    void SendMessage(IPAddress? playerIp, string message);
    Task SendPlayerList();
  }
}