using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using SignalRBlazor.Client.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRBlazor.Client.Components
{
  public partial class ViewDisplay : ComponentBase, IAsyncDisposable
  {
    [Inject]
    protected NavigationManager NavigationManager { get; set; } 
    
    private int viewCount = 0;
    private ClientViewHub viewHub;

    private async Task NotifyWatching()
    {
      await viewHub.Notify();
    }

    protected override async Task OnInitializedAsync()
    {
      viewHub = new ClientViewHub(NavigationManager, (value) => {
        viewCount = value;
        StateHasChanged();
      });

      await viewHub.Start();

      await NotifyWatching();
    }

    public async ValueTask DisposeAsync()
    {
      await viewHub.DisposeAsync();
    }
  }
}
