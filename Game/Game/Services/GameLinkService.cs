using Game.App.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Game.Services
{
  public class GameLinkService : IGameLinkService
  {
    private readonly IGameHubService _hubService;
    private readonly JsonSerializerSettings jsonSerializerSettings = new()
    {
      ContractResolver = new DefaultContractResolver
      {
        NamingStrategy = new CamelCaseNamingStrategy
        {
          OverrideSpecifiedNames = true
        }
      }
    };

    public GameLinkService(IGameHubService hubService)
    {
      _hubService = hubService;
    }

    public async Task SendChatHistory(ICollection<string> chatHistory)
    {
      var jsonData = JsonConvert.SerializeObject(chatHistory, jsonSerializerSettings);
      await _hubService.SendChatHistory(jsonData);
    }

    public async Task SendPlayerList(ICollection<string> playerList)
    {
      var jsonData = JsonConvert.SerializeObject(playerList, jsonSerializerSettings);
      await _hubService.SendPlayerList(jsonData);
    }

    public async Task SendGetPreparedSignal(int delay)
    {
      var jsonData = JsonConvert.SerializeObject(new { Args = delay }, jsonSerializerSettings);
      await _hubService.SendChatHistory(jsonData);
    }

    public async Task SendGoSignal()
    {
      await _hubService.SendGoSignal();
    }

    public async Task SendStopSignal()
    {
      await _hubService.SendStopSignal();
    }

    public async Task SendScore(string playerId, int score)
    {
      var jsonData = JsonConvert.SerializeObject(new { Args = score }, jsonSerializerSettings);
      await _hubService.SendScore(playerId, jsonData);
    }
  }
}
