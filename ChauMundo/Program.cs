using ChauMundo.Printers;

namespace ChauMundo
{
    internal class Program
    {
        //static string? Message = null;
        public static void Main(string[] args)
        {
            //Message = ConfigurationManager.AppSettings[nameof(Message)];
            //PrintMessage(new DebugPrinter(), Message ?? "Default message");

            // Start the web application
            CreateHostBuilder(args).Build().Run();
        }

        public static void PrintMessage(IPrinter Printer, string Message)
        {
            Printer.Print(Message);
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}