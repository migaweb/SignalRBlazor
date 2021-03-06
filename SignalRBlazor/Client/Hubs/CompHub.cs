using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using SignalRBlazor.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRBlazor.Client.Hubs
{

  public class CompHub : IAsyncDisposable
  {
    private readonly NavigationManager _navigationManager;
    private HubConnection _hubConnection;
    public event EventHandler<EventArgs> ReportsStateChanged;
    public event EventHandler<ColorEventArgs> ColorChanged;

    public CompHub(NavigationManager navigationManager)
    {
      _navigationManager = navigationManager;
    }

    public void OnColorChanged(ColorEventArgs e)
    {
      var handler = ColorChanged;

      if (handler != null)
      {
        handler(this, e);
      }
    }

    public void OnReportsStateChanged(EventArgs e)
    {
      var handler = ReportsStateChanged;

      if (handler != null)
      {
        handler(this, e);
      }
    }

    public void Init(Action<Report> reportsUpdated, Action<List<Report>> onReceiveAllReports, Action<Color> onColorUpdated)
    {
      InitHubConnection(reportsUpdated, onReceiveAllReports, onColorUpdated);
    }

    public async Task Start()
    {
      await _hubConnection.StartAsync();
      Console.WriteLine("Comp Hub is started");
    }

    private Task hubConnectionReconnected(string arg)
    {
      Console.WriteLine("Comp Hub was reconnected");

      return Task.CompletedTask;
    }

    private Task hubConnectionReconnecting(Exception arg)
    {
      Console.WriteLine("Comp Hub is reconnecting");
      return Task.CompletedTask;
    }

    private Task hubConnectionClosed(Exception arg)
    {
      Console.WriteLine("Hub is closed");
      return Task.CompletedTask;
    }

    public async Task ChangeColor(Color color)
    {
      await _hubConnection.SendAsync("ChangeColor", color);
    }

    public async Task GetAllReports()
    {
      await _hubConnection.SendAsync("getAllReports");
    }

    public async Task SendReport(Report report)
    {
      await _hubConnection.SendAsync("notifyReport", report);
    }

    private void InitHubConnection(Action<Report> onReportsUpdated, 
      Action<List<Report>> onReceiveAllReports, Action<Color> onColorUpdated)
    {
      _hubConnection = new HubConnectionBuilder()
            .WithUrl(_navigationManager.ToAbsoluteUri("/comphub"))
            .WithAutomaticReconnect()
            .Build();
      _hubConnection.Reconnected += hubConnectionReconnected;
      _hubConnection.Reconnecting += hubConnectionReconnecting;
      _hubConnection.Closed += hubConnectionClosed;
      _hubConnection.On<Report>("reportUpdated", onReportsUpdated);
      _hubConnection.On<List<Report>>("getCurrentReports", onReceiveAllReports);
      _hubConnection.On<Color>("colorUpdated", onColorUpdated);
    }

    public async ValueTask DisposeAsync()
    {
      await _hubConnection.DisposeAsync();
    }
  }
}
