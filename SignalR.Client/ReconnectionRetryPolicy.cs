using Microsoft.AspNetCore.SignalR.Client;

namespace SignalR.Client
{
  public class ReconnectionRetryPolicy : IRetryPolicy
  {
    public const int DefaultReconnectionRetryDelayInMilliseconds = 2000;
    public int ReconnectionRetryDelayInMilliseconds { get; set; } = DefaultReconnectionRetryDelayInMilliseconds;

    public TimeSpan? NextRetryDelay(RetryContext retryContext)
    {
      return new TimeSpan(TimeSpan.TicksPerMillisecond * ReconnectionRetryDelayInMilliseconds);
    }
  }
}
