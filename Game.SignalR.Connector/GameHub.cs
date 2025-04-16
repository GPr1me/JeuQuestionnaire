using Game.App.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Game.SignalR.Connector
{
  public sealed class GameHub : Hub
  {
    public static readonly string HubUrl = "/gamehub";
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

      _gameService.AddPlayer(Context.ConnectionId);
      await _gameService.SendPlayerList();
      await Clients.Caller.SendAsync("YourName", _gameService.Players[Context.ConnectionId]);
    }

    #region Commands  

    public async Task SendMessage(string message)
    {
      await Clients.All.SendAsync("ReceiveMessage", message);
      _gameService.SendMessage(Context.ConnectionId, message);
      await _gameService.SendChatHistory();
    }

    public Task ChangeName(string name)
    {
      string oldName = _gameService.Players[Context.ConnectionId].Name;
      _gameService.RenamePlayerById(Context.ConnectionId, name);
      string newName = _gameService.Players[Context.ConnectionId].Name;
      return Clients.Caller.SendAsync("PlayerNameChanged", oldName, newName);
    }

    #endregion
  }
}
