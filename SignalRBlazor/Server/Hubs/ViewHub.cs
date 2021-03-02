using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRBlazor.Server.Hubs
{
  public class ViewHub : Hub
  {
    public static int ViewCount { get; set; } = 0;

    public async Task NotifyWatching()
    {
      ViewCount++;

      await this.Clients.All.SendAsync("viewCountUpdate", ViewCount);
    }
  }
}
