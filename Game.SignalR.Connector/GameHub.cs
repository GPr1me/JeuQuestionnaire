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
      try
      {
        _gameService.RemovePlayer(GetClientIp());
        await base.OnDisconnectedAsync(exception);
      }
      catch (Exception ex)
      {
        // Log the exception
        Console.WriteLine($"Error in OnDisconnectedAsync: {ex.Message}");
        throw;
      }
    }

    public override async Task OnConnectedAsync()
    {
      try
      {
        _gameService.AddPlayer(GetClientIp());
        await base.OnConnectedAsync();
      }
      catch (Exception ex)
      {
        // Log the exception
        Console.WriteLine($"Error in OnConnectedAsync: {ex.Message}");
        throw;
      }
    }

    #region Commands

    public void SendMessage(string message)
    {
      try
      {
        _gameService.SendMessage(GetClientIp(), message);
      }
      catch (Exception ex)
      {
        // Log the exception
        Console.WriteLine($"Error in SendMessage: {ex.Message}");
        throw;
      }
    }

    public void ReceiveMessage(string message)
    {
      try
      {
        Clients.All.SendAsync("ReceiveMessage", message);
      }
      catch (Exception ex)
      {
        // Log the exception
        Console.WriteLine($"Error in ReceiveMessage: {ex.Message}");
        throw;
      }
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
