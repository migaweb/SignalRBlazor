using Microsoft.AspNetCore.SignalR;
using SignalRBlazor.Shared;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRBlazor.Server.Hubs
{
  public class CompHub : Hub
  {
    private readonly ReportStateQueue _reportQueue;

    public CompHub(ReportStateQueue reportQueue)
    {
      _reportQueue = reportQueue;
    }

    public async Task ChangeColor(Color color)
    {
      await this.Clients.All.SendAsync("colorUpdated", color);
    }

    public async Task GetAllReports()
    {
      if (_reportQueue.Queue.Count > 0)  
        await this.Clients.Caller.SendAsync("getCurrentReports", _reportQueue.Queue.ToList());
    }

    public async Task NotifyReport(Report report)
    {
      if (_reportQueue.Queue.Count <= 0) 
        report.Id = 1;
      else
        report.Id = _reportQueue.Queue.Max(r => r.Id) + 1;

      _reportQueue.Queue.Enqueue(report);
      await this.Clients.All.SendAsync("reportUpdated", report);
    }
  }
}
