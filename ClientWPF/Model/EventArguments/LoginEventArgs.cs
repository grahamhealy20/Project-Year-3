using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWPF.Model.EventArguments
{
    class LoginEventArgs : EventArgs
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
