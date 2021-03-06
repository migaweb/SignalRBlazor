using Microsoft.AspNetCore.Components;
using SignalRBlazor.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRBlazor.Client.Components
{
  public partial class ReportsList : ComponentBase, IDisposable
  {
    [CascadingParameter]
    public Hubs.CompHub CompHub { get; set; }

    [CascadingParameter]
    public List<Report> Reports { get; set; }

    protected override void OnInitialized()
    {
      base.OnInitialized();
      CompHub.ReportsStateChanged += CompHub_ReportsStateChanged;
    }

    private void CompHub_ReportsStateChanged(object sender, EventArgs e)
    {
      StateHasChanged();
    }

    public void Dispose()
    {
      CompHub.ReportsStateChanged -= CompHub_ReportsStateChanged;
    }
  }
}
