using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace SignalRBlazor.Client.Hubs
{
  public class ClientViewHub : IAsyncDisposable
  {
    private readonly NavigationManager _navigationManager;
    private HubConnection _hubConnection;

    public ClientViewHub(NavigationManager navigationManager, Action<int> viewCountUpdate)
    {
      _navigationManager = navigationManager;

      initHubConnection(viewCountUpdate);
    }

    public async Task Start()
    {
      await _hubConnection.StartAsync();
      Console.WriteLine("Hub is started");
    }

    public async Task Notify()
    {
      await _hubConnection.SendAsync("notifyWatching");
    }

    private Task hubConnectionReconnected(string arg)
    {
      Console.WriteLine("Hub was reconnected");

      return Task.FromResult(true);
    }

    private Task hubConnectionReconnecting(Exception arg)
    {
      Console.WriteLine("Hub is reconnecting");
      return Task.FromResult(true);
    }

    private Task hubConnectionClosed(Exception arg)
    {
      Console.WriteLine("Hub is closed");
      return Task.FromResult(true);
    }

    private void initHubConnection(Action<int> viewCountUpdate)
    {
      _hubConnection = new HubConnectionBuilder()
            .WithUrl(_navigationManager.ToAbsoluteUri("/viewhub"))
            .Build();

      _hubConnection.Reconnected += hubConnectionReconnected;
      _hubConnection.Reconnecting += hubConnectionReconnecting;
      _hubConnection.Closed += hubConnectionClosed;

      _hubConnection.On<int>("viewCountUpdate", viewCountUpdate);
    }

    public async ValueTask DisposeAsync()
    {
      await _hubConnection.DisposeAsync();
    }
  }
}
