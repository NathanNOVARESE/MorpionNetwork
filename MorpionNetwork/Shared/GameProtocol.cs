namespace Shared
{
    public static class GameProtocol
    {
        public const int ServerPort = 11000;
        
        public enum MessageType
        {
            Connect,
            Disconnect,
            Move,
            GameStart,
            GameOver,
            KeepAlive
        }

        public class GameMessage
        {
            public MessageType Type { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public char PlayerSymbol { get; set; }
            public string PlayerName { get; set; }
        }
    }
}