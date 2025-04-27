using Game.App.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Game.Client.Pages
{
  public class SignalRClientComponent : ComponentBase, IDisposable
  {
    private IDisposable? Subscription;
    private bool disposedValue;

    [Inject]
    public required IHubClient Hub { get; set; }

    protected async Task InitializeAsync()
    {
      await Hub.OpenConnection();
    }

    protected void AddOnEventListener<T>(string methodName, Action<T> handler)
    {
      Subscription = Hub.On<T>(methodName, (arg) =>
      {
        handler(arg);
        StateHasChanged();
      });
    }

    protected void Send(string methodName, object? arg) => Hub.Send(methodName, arg);

    #region Dispose implementation

    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
          Subscription?.Dispose();

        disposedValue = true;
      }
    }

    public void Dispose()
    {
      Dispose(disposing: true);
      GC.SuppressFinalize(this);
    }

    #endregion
  }
}
