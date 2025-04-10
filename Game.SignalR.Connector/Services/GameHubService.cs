using Game.SignalR.Connector.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Game.SignalR.Connector.Services
{
  public class GameHubService : IGameHubService
  {
    private readonly IHubContext<GameHub> _hub;

    public GameHubService(IHubContext<GameHub> hub)
    {
      _hub = hub;
    }

    public async Task SendPlayerList(string jsonData) => await _hub.Clients.All.SendAsync("PlayerListUpdated", jsonData);
    public async Task SendChatHistory(string jsonData) => await _hub.Clients.All.SendAsync("ChatHistoryUpdated", jsonData);
  }
}
