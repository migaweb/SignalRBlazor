using Microsoft.AspNetCore.Components;
using SignalRBlazor.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRBlazor.Client.Pages
{
  public partial class Index : ComponentBase, IDisposable
  {
    [Inject]
    public Hubs.CompHub CompHub { get; set; }

    public Color Color { get; set; }

    protected override void OnInitialized()
    {
      base.OnInitialized();
      CompHub.ColorChanged += CompHubColorChanged;
    }

    private void CompHubColorChanged(object sender, ColorEventArgs e)
    {      
      Color = e.NewColor;
      StateHasChanged();
    }

    protected async Task UpdateColor()
    {
      var ran = new Random();
      var index = ran.Next(0, 3);

      await CompHub.ChangeColor((Color)index);
    }

    public void Dispose()
    {
      CompHub.ColorChanged -= CompHubColorChanged;
    }
  }
}
