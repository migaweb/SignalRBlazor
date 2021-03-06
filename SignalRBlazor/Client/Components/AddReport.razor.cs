using Microsoft.AspNetCore.Components;
using SignalRBlazor.Client.Hubs;
using SignalRBlazor.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRBlazor.Client.Components
{
  public partial class AddReport : ComponentBase
  {
    [CascadingParameter]
    public Hubs.CompHub CompHub { get; set; }

    public Report Report { get; set; } = new Report { Status = Status.Online };    

    private async Task OnReportSubmit()
    {
      var report = Report;
      await CompHub.SendReport(report);
    }
  }
}
