using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRBlazor.Shared
{
  public class Report
  {
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string Username { get; set; }
    public string Message { get; set; }
    public string Page { get; set; }
    public Status Status { get; set; }
  }

  public enum Status
  {
    Online,
    Idle,
    Error
  }
}
