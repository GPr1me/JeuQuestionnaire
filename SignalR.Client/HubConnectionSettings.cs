namespace SignalR.Client
{
  public class HubConnectionSettings
  {
    public required string Url { get; set; }
    public ReconnectionRetryPolicy RetryPolicy { get; set; } = new();
  }
}
