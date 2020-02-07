using System;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.Start();
            Console.WriteLine("Listening on port 8080");
            Console.WriteLine("Press any key to stop...");
            Console.ReadKey();
            Console.WriteLine("Stopping!");
            server.Stop();
        }
    }
}
