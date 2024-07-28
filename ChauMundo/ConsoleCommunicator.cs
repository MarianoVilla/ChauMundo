using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ChauMundo
{
    internal class ConsoleCommunicator : ICommunicator
    {
        public void Communicate(string Message)
        {
            Console.WriteLine(Message);
        }
    }
}
