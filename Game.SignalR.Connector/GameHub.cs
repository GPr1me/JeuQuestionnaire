using Game.App.Services.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;
using System.Net;

namespace Game.SignalR.Connector
{
  public class GameHub : Hub
  {
    public static readonly string HubUrl = "/gamehub";
    private readonly IGameService _gameService;

    public GameHub(IGameService gameService)
    {
      _gameService = gameService;
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
      _gameService.RemovePlayer(GetClientIp());
      await base.OnDisconnectedAsync(exception);
    }

    public override async Task OnConnectedAsync()
    {
      _gameService.AddPlayer(GetClientIp());
      await base.OnConnectedAsync();
    }

    #region Commands

    public void SendMessage(string message)
    {
      _gameService.SendMessage(GetClientIp(), message);
    }

    public void ReceiveMessage(string message)
    {
      Clients.All.SendAsync("ReceiveMessage", message);
    }

    #endregion

    #region Private functions

    private IPAddress? GetClientIp()
    {
      return Context.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress?.MapToIPv4();
    }

    #endregion
  }
}
