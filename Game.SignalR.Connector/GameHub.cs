using Game.App.Services.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;
using System.Net;

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

      _gameService.RemovePlayer(GetClientIp());
      await _gameService.SendPlayerList();
    }

    public override async Task OnConnectedAsync()
    {
      await base.OnConnectedAsync();

      _gameService.AddPlayer(GetClientIp());
      await _gameService.SendPlayerList();
    }

    #region Commands

    public async Task SendMessage(string message)
    {
      _gameService.SendMessage(GetClientIp(), message);
      await _gameService.SendChatHistory();
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
