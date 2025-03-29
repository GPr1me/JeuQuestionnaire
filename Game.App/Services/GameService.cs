using Game.App.Services.Interfaces;
using System.Collections.Concurrent;
using System.Net;

namespace Game.App.Services
{
  public class GameService : IGameService
  {
    public List<string> Players { get; private set; } = [];
    private readonly ConcurrentDictionary<string, List<string>> _chatHistory = new();

    public void AddPlayer(IPAddress? playerIp)
    {
      var player = ValidateAndToString(playerIp);
      Players.Add(player);
      _chatHistory.TryAdd(player, []);
    }

    public void RemovePlayer(IPAddress? playerIp)
    {
      var player = ValidateAndToString(playerIp);
      Players.Remove(player);
      _chatHistory.TryRemove(player, out _);
    }

    public void SendMessage(IPAddress? playerIp, string message)
    {
      var player = ValidateAndToString(playerIp);
      if (_chatHistory.TryGetValue(player, out List<string>? value))
      {
        value.Add(message);
      }
    }

    public List<string> GetChatHistory()
    {
      return [.. _chatHistory.Values.SelectMany(x => x)];
    }

    private static string ValidateAndToString(IPAddress? playerIp)
    {
      if (playerIp is null) throw new InvalidDataException("Sender IP not found");
      return playerIp.ToString();
    }
  }
}
