using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient.Model
{
  public class InfoEventArgs : EventArgs
  {
    public string message;

    public InfoEventArgs(string message)
    {
      this.message = message;
    }  
  }
}
