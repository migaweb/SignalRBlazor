using Microsoft.AspNetCore.Components;
using SignalRBlazor.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRBlazor.Client.Components
{
  public partial class CompHub : ComponentBase 
  {
    [Parameter]
    public RenderFragment ChildContent { get; set; }

    [Inject]
    public Hubs.CompHub CompHubManager { get; set; }

    public List<Report> Reports { get; set; } = new List<Report>();

    protected override async Task OnInitializedAsync()
    {
      CompHubManager.Init(OnReportAdded, OnReceiveAllReports, onColorUpdated);
      await CompHubManager.Start();
      await CompHubManager.GetAllReports();
      CompHubManager.OnReportsStateChanged(new EventArgs());
    }

    private void onColorUpdated(Color color)
    {
      CompHubManager.OnColorChanged(new ColorEventArgs(color));
    }

    public void OnReceiveAllReports(List<Report> reports)
    {
      Reports.AddRange(reports);
      CompHubManager.OnReportsStateChanged(new EventArgs());
    }

    public void OnReportAdded (Report report)
    {
      Reports.Add(report);
      CompHubManager.OnReportsStateChanged(new EventArgs());
    }
  }
}
