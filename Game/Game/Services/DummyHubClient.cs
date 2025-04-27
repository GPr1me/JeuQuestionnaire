using Game.App.Services.Interfaces;

namespace Game.Services
{
  public class DummyHubClient : IHubClient
  {
    public bool IsConnected => false;

    public IDisposable On<T>(string methodName, Action<T> handler)
    {
      return new DummyDisposable();
    }

    public Task OpenConnection()
    {
      return Task.CompletedTask;
    }

    public void Send(string methodName, object arg) { }
  }
  public class DummyDisposable : IDisposable
  {
    public void Dispose()
    {
      // No operation, dummy implementation
    }
  }
}
