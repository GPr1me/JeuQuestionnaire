using Game.App.Enum;
using Game.Core.Models;

namespace Game.App.Services.Interfaces
{
  public interface IGameLinkService
  {
    Task SendChatHistory(ICollection<string> chatHistory);
    Task SendGetPreparedSignal(int delay);
    Task SendGoSignal();
    Task SendPlayerList(ICollection<Player> playerList);
    Task SendScore(string playerId, int score);
    Task SendState(GameState state);
    Task SendStopSignal();
  }
}