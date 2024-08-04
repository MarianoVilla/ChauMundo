using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChauMundo.Printers
{
    public class DebugPrinter : IPrinter
    {
        OutputCapture outputCapture;
        public DebugPrinter()
        {
            outputCapture = new OutputCapture();
        }
        ~DebugPrinter()
        {
            outputCapture.Dispose();
        }
        public void Print(string Message)
        {
            outputCapture.WriteLine(Message);
        }


    }
}
