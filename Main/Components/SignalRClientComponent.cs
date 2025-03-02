using Microsoft.AspNetCore.Components;
using SignalR.Client;

namespace Main.Components
{
  public class SignalRClientComponent : ComponentBase, IDisposable
  {
    private IDisposable? Subscription;
    private bool disposedValue;

    [Inject]
    public required HubClient Hub { get; set; }


    protected void AddOnEventListener<T>(string methodName, Action<T> handler)
    {
      Subscription = Hub.On<T>(methodName, (arg) =>
      {
        handler(arg);
        StateHasChanged();
      });
    }

    #region Dispose implementation

    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
        {
          // TODO: dispose managed state (managed objects)
          Subscription?.Dispose();
        }

        // TODO: free unmanaged resources (unmanaged objects) and override finalizer
        // TODO: set large fields to null
        disposedValue = true;
      }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~SignalRClientInstance()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
      // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
      Dispose(disposing: true);
      GC.SuppressFinalize(this);
    }

    #endregion
  }
}
