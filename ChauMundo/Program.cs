using System.Configuration;
using ChauMundo.Printers;

namespace ChauMundo
{
    internal class Program
    {
        static string? Message = null;
        static void Main(string[] args)
        {
            Message = ConfigurationManager.AppSettings[nameof(Message)];
            PrintMessage(new DebugPrinter(), Message ?? "Default message");
            Console.ReadLine();
        }
        static void PrintMessage(IPrinter Printer, 
            string Message)
        {
            Printer.Print(Message);
        }
    }
}
