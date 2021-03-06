using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRBlazor.Shared
{
  public class ColorEventArgs : EventArgs
  {
    public Color NewColor { get; set; }

    public ColorEventArgs(Color color)
    {
      NewColor = color;
    }
  }
}
