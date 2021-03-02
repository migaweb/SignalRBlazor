using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace SignalRBlazor.Client.Hubs
{
  public class TimeHub : IAsyncDisposable
  {
    private readonly NavigationManager _navigationManager;
    private HubConnection _hubConnection;

    public TimeHub(NavigationManager navigationManager, Action<string> timeUpdated)
    {
      _navigationManager = navigationManager;

      initHubConnection(timeUpdated);
    }

    public async Task Start()
    {
      await _hubConnection.StartAsync();
      Console.WriteLine("Hub is started");
    }

    private Task hubConnectionReconnected(string arg)
    {
      Console.WriteLine("Hub was reconnected");

      return Task.CompletedTask;
    }

    private Task hubConnectionReconnecting(Exception arg)
    {
      Console.WriteLine("Hub is reconnecting");
      return Task.CompletedTask;
    }

    private Task hubConnectionClosed(Exception arg)
    {
      Console.WriteLine("Hub is closed");
      return Task.CompletedTask;
    }

    private void initHubConnection(Action<string> timeUpdated)
    {
      _hubConnection = new HubConnectionBuilder()
            .WithUrl(_navigationManager.ToAbsoluteUri("/timehub"))
            .Build();

      _hubConnection.Reconnected += hubConnectionReconnected;
      _hubConnection.Reconnecting += hubConnectionReconnecting;
      _hubConnection.Closed += hubConnectionClosed;

      _hubConnection.On<string>("updateCurrentTime", timeUpdated);
    }

    public async ValueTask DisposeAsync()
    {
      await _hubConnection.DisposeAsync();
    }
  }
}
