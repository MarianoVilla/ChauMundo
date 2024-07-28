namespace ChauMundo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PrintMessage(new ConsoleCommunicator(), "Hello World!");
            Console.ReadLine();
        }
        static void PrintMessage(ICommunicator Communicator, 
            string Message)
        {
            Communicator.Communicate(Message);
        }
    }
}
