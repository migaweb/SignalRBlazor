using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRBlazor.Shared
{
  public class ReportStateQueue
  {
    public FixedSizedQueue<Report> Queue { get; set; }

    public ReportStateQueue()
    {
      Queue = new FixedSizedQueue<Report>(10);
    }
  }
}
