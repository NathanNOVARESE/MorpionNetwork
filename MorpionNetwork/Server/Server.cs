using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using Shared;

namespace Server
{
    public class Server
    {
        private TcpListener _listener;
        private TcpClient[] _players;
        private bool _gameRunning;
        
        public Server()
        {
            _players = new TcpClient[2];
            _listener = new TcpListener(IPAddress.Any, GameProtocol.ServerPort);
        }

        public void Start()
        {
            _listener.Start();
            Console.WriteLine("Serveur démarré. En attente de joueurs...");

            for (int i = 0; i < 2; i++)
            {
                _players[i] = _listener.AcceptTcpClient();
                Console.WriteLine($"Joueur {i + 1} connecté.");

                var connectMsg = new GameProtocol.GameMessage
                {
                    Type = GameProtocol.MessageType.GameStart,
                    PlayerSymbol = i == 0 ? 'X' : 'O'
                };

                SendMessage(_players[i], connectMsg);
            }

            _gameRunning = true;
            Console.WriteLine("La partie commence!");

            HandleGame();
        }

        private void HandleGame()
        {
            int currentPlayer = 0;

            while (_gameRunning)
            {
                var message = ReceiveMessage(_players[currentPlayer]);
                
                if (message.Type == GameProtocol.MessageType.Move)
                {
                    Console.WriteLine($"Joueur {currentPlayer + 1} a joué en {message.X},{message.Y}");
                    
                    // Envoyer le mouvement à l'autre joueur
                    SendMessage(_players[(currentPlayer + 1) % 2], message);
                    
                    // Changer de joueur
                    currentPlayer = (currentPlayer + 1) % 2;
                }
                else if (message.Type == GameProtocol.MessageType.Disconnect)
                {
                    Console.WriteLine($"Joueur {currentPlayer + 1} s'est déconnecté");
                    _gameRunning = false;
                }
            }

            // Fermer les connexions
            foreach (var player in _players)
            {
                player?.Close();
            }
            _listener.Stop();
        }

        private GameProtocol.GameMessage ReceiveMessage(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string messageJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            return JsonSerializer.Deserialize<GameProtocol.GameMessage>(messageJson);
        }

        private void SendMessage(TcpClient client, GameProtocol.GameMessage message)
        {
            string messageJson = JsonSerializer.Serialize(message);
            byte[] buffer = Encoding.UTF8.GetBytes(messageJson);
            NetworkStream stream = client.GetStream();
            stream.Write(buffer, 0, buffer.Length);
        }
    }
}