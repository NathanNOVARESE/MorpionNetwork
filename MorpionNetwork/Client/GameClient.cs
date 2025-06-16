using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using Shared;

namespace Client
{
    public class GameClient
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private TicTacToeGame _game;
        private char _mySymbol;

        public GameClient(string serverIp)
        {
            _client = new TcpClient(serverIp, GameProtocol.ServerPort);
            _stream = _client.GetStream();
            _game = new TicTacToeGame();
        }

        public void Start()
        {
            // Recevoir le symbole attribué
            var initialMessage = ReceiveMessage();
            _mySymbol = initialMessage.PlayerSymbol;
            _game.MySymbol = _mySymbol;
            
            Console.WriteLine($"Vous jouez avec les {_mySymbol}");

            if (_mySymbol == 'X')
            {
                PlayTurn();
            }

            while (true)
            {
                var message = ReceiveMessage();
                
                if (message.Type == GameProtocol.MessageType.Move)
                {
                    _game.MakeMove(message.X, message.Y, message.PlayerSymbol);
                    _game.DisplayBoard();
                    
                    char winner = _game.CheckWinner();
                    if (winner != ' ')
                    {
                        if (winner == 'D')
                        {
                            Console.WriteLine("Match nul!");
                        }
                        else if (winner == _mySymbol)
                        {
                            Console.WriteLine("Vous avez gagné!");
                        }
                        else
                        {
                            Console.WriteLine("Vous avez perdu!");
                        }
                        break;
                    }
                    
                    if (message.PlayerSymbol != _mySymbol)
                    {
                        PlayTurn();
                    }
                }
                else if (message.Type == GameProtocol.MessageType.Disconnect)
                {
                    Console.WriteLine("L'autre joueur s'est déconnecté.");
                    break;
                }
            }

            _client.Close();
        }

        private void PlayTurn()
        {
            Console.WriteLine("Votre tour. Entrez les coordonnées (ligne colonne):");
            var input = Console.ReadLine().Split(' ');
            int x = int.Parse(input[0]);
            int y = int.Parse(input[1]);

            var moveMessage = new GameProtocol.GameMessage
            {
                Type = GameProtocol.MessageType.Move,
                X = x,
                Y = y,
                PlayerSymbol = _mySymbol
            };

            SendMessage(moveMessage);
            _game.MakeMove(x, y, _mySymbol);
            _game.DisplayBoard();
        }

        private GameProtocol.GameMessage ReceiveMessage()
        {
            byte[] buffer = new byte[1024];
            int bytesRead = _stream.Read(buffer, 0, buffer.Length);
            string messageJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            return JsonSerializer.Deserialize<GameProtocol.GameMessage>(messageJson);
        }

        private void SendMessage(GameProtocol.GameMessage message)
        {
            string messageJson = JsonSerializer.Serialize(message);
            byte[] buffer = Encoding.UTF8.GetBytes(messageJson);
            _stream.Write(buffer, 0, buffer.Length);
        }
    }
}