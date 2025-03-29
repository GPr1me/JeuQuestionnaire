using System.Net;

namespace Game.App.Services.Interfaces
{
  public interface IGameService
  {
    List<string> Players { get; }

    void AddPlayer(IPAddress? playerIp);
    List<string> GetChatHistory();
    void RemovePlayer(IPAddress? playerIp);
    void SendMessage(IPAddress? playerIp, string message);
  }
}