using Game.App.Services.Interfaces;
using Game.SignalR.Connector.Services.Interfaces;
using System.Collections.Concurrent;
using System.Net;

namespace Game.App.Services
{
  public class GameService : IGameService
  {
    private readonly IGameLinkService _gameLinkService;

    public List<string> Players { get; private set; } = [];
    private readonly ConcurrentDictionary<string, List<string>> _chatHistory = new();

    public GameService(IGameLinkService gameLinkService)
    {
      _gameLinkService = gameLinkService;
    }

    public void AddPlayer(IPAddress? playerIp)
    {
      if (playerIp is null) return;
      var player = ValidateAndToString(playerIp);
      Players.Add(player);
      _chatHistory.TryAdd(player, []);
    }

    public void RemovePlayer(IPAddress? playerIp)
    {
      if (playerIp is null) return;
      var player = ValidateAndToString(playerIp);
      Players.Remove(player);
      _chatHistory.TryRemove(player, out _);
    }

    public void SendMessage(IPAddress? playerIp, string message)
    {
      if (playerIp is null) return;
      var player = ValidateAndToString(playerIp);
      if (_chatHistory.TryGetValue(player, out List<string>? value))
      {
        value.Add(message);
      }
    }

    public async Task SendPlayerList() => await _gameLinkService.SendPlayerList(Players.AsReadOnly());

    public async Task SendChatHistory() => await _gameLinkService.SendChatHistory([.. _chatHistory.Values.SelectMany(x => x)]);

    private static string ValidateAndToString(IPAddress? playerIp)
    {
      if (playerIp is null) throw new InvalidDataException("Sender IP not found");
      return playerIp.ToString();
    }
  }
}
