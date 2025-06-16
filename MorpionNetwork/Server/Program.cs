using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lancement du serveur de morpion...");
            var server = new Server();
            server.Start();
        }
    }
}