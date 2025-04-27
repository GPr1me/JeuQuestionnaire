using Game.App.Services.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

namespace SignalR.Client
{
  public class HubClient(string url) : IHubClient, IAsyncDisposable
  {
    private readonly HubConnection _hubConnection = new HubConnectionBuilder().WithUrl(url).Build();

    public bool IsConnected { get => _hubConnection.State == HubConnectionState.Connected; }

    public async Task OpenConnection()
    {
      if (!IsConnected)
      {
        _hubConnection.Closed += OnClose;
        _hubConnection.Reconnecting += OnReconnecting;
        _hubConnection.Reconnected += OnReconnected;

        await _hubConnection.StartAsync();
      }
    }

    #region HubConnection Events

    private Task OnClose(Exception? e)
    {
      return Task.CompletedTask;
    }

    private Task OnReconnected(string? connectionId)
    {
      return Task.CompletedTask;
    }

    private Task OnReconnecting(Exception? e)
    {
      return Task.CompletedTask;
    }

    #endregion

    public IDisposable On<T>(string methodName, Action<T> handler) => _hubConnection.On(methodName, (string arg) =>
    {
      var convertedArg = (T) JsonConvert.DeserializeObject(arg, typeof(T))!;
      handler(convertedArg);
    });

    public void Send(string methodName, object arg)
    {
      var serializedArg = JsonConvert.SerializeObject(arg);
      _hubConnection.SendAsync(methodName, serializedArg);
    }

    public ValueTask DisposeAsync()
    {
      GC.SuppressFinalize(this);
      return ((IAsyncDisposable) _hubConnection).DisposeAsync();
    }
  }
}
