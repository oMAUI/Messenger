using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mora.Designs.Chat
{
    internal interface IMsg
    {
        void DrawMsgBox(string id, string msg);
    }
}
