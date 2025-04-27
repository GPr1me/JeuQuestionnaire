using Game.App.Services.Interfaces;
using Game.Core.Models;
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

    public async Task SendPlayerList(string jsonData) => await _hub.Clients.All.SendAsync(HubEnpoints.PlayerListUpdated, jsonData);
    public async Task SendChatHistory(string jsonData) => await _hub.Clients.All.SendAsync(HubEnpoints.ChatHistoryUpdated, jsonData);
    public async Task SendGetPreparedSignal(int delay) => await _hub.Clients.All.SendAsync(HubEnpoints.GetPrepared, delay);
    public async Task SendGoSignal() => await _hub.Clients.All.SendAsync(HubEnpoints.Go);
    public async Task SendStopSignal() => await _hub.Clients.All.SendAsync(HubEnpoints.Stop);
    public async Task SendScore(string playerId, string jsonData) => await _hub.Clients.Client(playerId).SendAsync(HubEnpoints.ScoreUpdated, jsonData);
    public async Task SendState(string jsonData) => await _hub.Clients.All.SendAsync(HubEnpoints.StateUpdated, jsonData);
  }
}
