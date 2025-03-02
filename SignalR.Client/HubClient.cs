using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SignalR.Client
{
  public class HubClient
  {
    private readonly HubConnection _hubConnection;
    private readonly ILogger<HubClient> _logger;

    public bool IsConnected { get => _hubConnection.State == HubConnectionState.Connected; }

    public HubClient(HubConnectionSettings settings, ILogger<HubClient> logger)
    {
      _hubConnection = new HubConnectionBuilder().WithUrl(settings.Url)
                                                 .WithAutomaticReconnect(settings.RetryPolicy)
                                                 .Build();
      _logger = logger;
    }

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
      if (e != null) _logger.LogError("SignalR hub reconnection error: {exception}", e);
      else _logger.LogInformation("SignalR hub reconnection in progress.");

      return Task.CompletedTask;
    }

    private Task OnReconnected(string? connectionId)
    {


      return Task.CompletedTask;
    }

    private Task OnReconnecting(Exception? e)
    {
      if (e != null) _logger.LogError("SignalR hub reconnection error: {exception}", e);
      else _logger.LogInformation("SignalR hub reconnection in progress.");

      return Task.CompletedTask;
    }

    #endregion

    public IDisposable On<T>(string methodName, Action<T> handler) => _hubConnection.On(methodName, (string arg) =>
    {
      var convertedArg = (T) JsonConvert.DeserializeObject(arg, typeof(T))!;
      handler(convertedArg);
    });
  }
}
