namespace Game.App.Services.Interfaces
{
  public interface IHubClient
  {
    bool IsConnected { get; }

    IDisposable On<T>(string methodName, Action<T> handler);
    Task OpenConnection();
    void Send(string methodName, object arg);
  }
}