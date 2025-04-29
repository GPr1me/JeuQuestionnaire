using Game.App.Services.Interfaces;
using Game.Core.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Game.SignalR.Connector
{
  public sealed class GameHub : Hub
  {
    public static readonly string HubUrl = "gamehub";
    private readonly IGameService _gameService;

    public GameHub(IGameService gameService)
    {
      _gameService = gameService;
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
      await base.OnDisconnectedAsync(exception);

      _gameService.RemovePlayer(Context.ConnectionId);
      await _gameService.SendPlayerList();
    }

    public override async Task OnConnectedAsync()
    {
      await base.OnConnectedAsync();
      await RegisterPlayer();
    }

    #region Commands

    public async Task RegisterPlayer()
    {
      _gameService.AddPlayer(Context.ConnectionId);
      await _gameService.SendPlayerList();
      await Clients.Caller.SendAsync("YourName", _gameService.Players[Context.ConnectionId]);
    }

    public async Task UnregisterPlayer(string arg)
    {
      _gameService.RemovePlayer(Context.ConnectionId);
      await _gameService.SendPlayerList();
    }

    public async Task SendMessage(string arg)
    {
      string message = JsonConvert.DeserializeObject<string>(arg)!;
      await Clients.All.SendAsync("ReceiveMessage", message);
      await _gameService.SendMessage(Context.ConnectionId, message);
    }

    public Task ChangeName(string name)
    {
      string oldName = _gameService.Players[Context.ConnectionId].Name;
      _gameService.RenamePlayerById(Context.ConnectionId, name);
      string newName = _gameService.Players[Context.ConnectionId].Name;
      return Clients.Caller.SendAsync("PlayerNameChanged", oldName, newName);
    }

    public async Task CurrentQuestionUpdated(string jsonData)
    {
      await Clients.All.SendAsync(HubEnpoints.CurrentQuestionUpdated, jsonData);
    }

    public async Task GamePlayerUpdated(string jsonData)
    {
      await Clients.All.SendAsync(HubEnpoints.GamePlayerUpdated, jsonData);
    }

    #endregion
  }
}
