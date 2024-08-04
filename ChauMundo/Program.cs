using System.Configuration;
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

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/hello", () =>
                {
                    string? message = System.Configuration.ConfigurationManager.AppSettings["Message"];
                    Program.PrintMessage(new DebugPrinter(), message ?? "Default message");
                    return Results.Ok("Message printed to debug output");
                });
            });
        }
    }
}