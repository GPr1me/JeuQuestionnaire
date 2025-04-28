namespace Game.App.Services.Interfaces
{
  public interface IHubClient
  {
    bool IsConnected { get; }

    IDisposable On<T>(string methodName, Action<T> handler);
    Task OpenConnection(Func<Exception?, Task>? onClose, Func<string?, Task>? onRecconnected, Func<Exception?, Task>? onReconnecting);
    void Send(string methodName, object arg);
  }
}