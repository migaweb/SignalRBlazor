using Microsoft.AspNetCore.Components;
using SignalRBlazor.Client.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRBlazor.Client.Components
{
  public partial class Time : ComponentBase, IAsyncDisposable
  {
    [Inject]
    protected NavigationManager NavigationManager { get; set; }

    private string time = "";
    private TimeHub timeHub;

    protected override async Task OnInitializedAsync()
    {
      timeHub = new TimeHub(NavigationManager, (value) => {
        time = value;
        StateHasChanged();
      });

      await timeHub.Start();
    }

    public async ValueTask DisposeAsync()
    {
      await timeHub.DisposeAsync();
    }
  }
}
