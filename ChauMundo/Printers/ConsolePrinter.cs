using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ChauMundo.Printers
{
    internal class ConsolePrinter : IPrinter
    {
        public void Print(string Message)
        {
            Console.WriteLine(Message);
        }
    }
}
