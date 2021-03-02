using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using SignalRBlazor.Server.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRBlazor.Server.HostedServices
{
  public class TimeService : IHostedService, IDisposable
  {
    private readonly IHubContext<TimeHub> _timeHub;
    private Timer _timer;

    public TimeService(IHubContext<TimeHub> timeHub)
    {
      _timeHub = timeHub;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
      _timer = new Timer(Tick, null, 0, 500);

      return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      _timer?.Change(Timeout.Infinite, 0);
      return Task.CompletedTask;
    }

    private void Tick(object state)
    {
      var currentTime = DateTime.UtcNow.ToString("F");
      _timeHub.Clients.All.SendAsync("updateCurrentTime", currentTime);
    }

    public void Dispose()
    {
      _timer?.Dispose();
    }
  }
}
