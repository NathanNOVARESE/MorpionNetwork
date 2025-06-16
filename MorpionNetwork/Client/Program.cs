using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Client de Morpion");
            Console.Write("Entrez l'adresse IP du serveur: ");
            string serverIp = Console.ReadLine();

            var client = new GameClient(serverIp);
            client.Start();
        }
    }
}