using Game.App.Services.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

namespace SignalR.Client
{
  public class HubClient(string url) : IHubClient, IAsyncDisposable
  {
    private readonly HubConnection _hubConnection = new HubConnectionBuilder().WithUrl(url).Build();

    public bool IsConnected { get => _hubConnection.State == HubConnectionState.Connected; }

    public async Task OpenConnection(Func<Exception?, Task>? onClose,
                                     Func<string?, Task>? onReconnected,
                                     Func<Exception?, Task>? onReconnecting)
    {
      if (!IsConnected)
      {
        _hubConnection.Closed += onClose;
        _hubConnection.Reconnecting += onReconnecting;
        _hubConnection.Reconnected += onReconnected;

        await _hubConnection.StartAsync();
      }
    }

    public IDisposable On<T>(string methodName, Action<T> handler) => _hubConnection.On(methodName, (string arg) =>
    {
      var convertedArg = (T) JsonConvert.DeserializeObject(arg, typeof(T))!;
      handler(convertedArg);
    });

    public void Send(string methodName, object? arg)
    {
      var serializedArg = arg is string stringArg ? stringArg : JsonConvert.SerializeObject(arg);
      _hubConnection.SendAsync(methodName, serializedArg);
    }

    public ValueTask DisposeAsync()
    {
      GC.SuppressFinalize(this);
      return ((IAsyncDisposable) _hubConnection).DisposeAsync();
    }
  }
}
