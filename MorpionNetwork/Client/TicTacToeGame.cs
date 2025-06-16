using System;

namespace Client
{
    public class TicTacToeGame
    {
        private char[,] _board;
        public char CurrentPlayerSymbol { get; private set; }
        public char MySymbol { get; set; }

        public TicTacToeGame()
        {
            _board = new char[3, 3];
            ResetBoard();
        }

        public void ResetBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _board[i, j] = ' ';
                }
            }
            CurrentPlayerSymbol = 'X';
        }

        public bool MakeMove(int x, int y, char player)
        {
            if (x < 0 || x > 2 || y < 0 || y > 2 || _board[x, y] != ' ' || player != CurrentPlayerSymbol)
            {
                return false;
            }

            _board[x, y] = player;
            CurrentPlayerSymbol = CurrentPlayerSymbol == 'X' ? 'O' : 'X';
            return true;
        }

        public char CheckWinner()
        {
            // Vérifier les lignes
            for (int i = 0; i < 3; i++)
            {
                if (_board[i, 0] != ' ' && _board[i, 0] == _board[i, 1] && _board[i, 1] == _board[i, 2])
                {
                    return _board[i, 0];
                }
            }

            // Vérifier les colonnes
            for (int j = 0; j < 3; j++)
            {
                if (_board[0, j] != ' ' && _board[0, j] == _board[1, j] && _board[1, j] == _board[2, j])
                {
                    return _board[0, j];
                }
            }

            // Vérifier les diagonales
            if (_board[0, 0] != ' ' && _board[0, 0] == _board[1, 1] && _board[1, 1] == _board[2, 2])
            {
                return _board[0, 0];
            }

            if (_board[0, 2] != ' ' && _board[0, 2] == _board[1, 1] && _board[1, 1] == _board[2, 0])
            {
                return _board[0, 2];
            }

            // Vérifier match nul
            bool isFull = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_board[i, j] == ' ')
                    {
                        isFull = false;
                        break;
                    }
                }
            }

            return isFull ? 'D' : ' ';
        }

        public void DisplayBoard()
        {
            Console.Clear();
            Console.WriteLine("  0 1 2");
            for (int i = 0; i < 3; i++)
            {
                Console.Write(i + " ");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(_board[i, j]);
                    if (j < 2) Console.Write("|");
                }
                Console.WriteLine();
                if (i < 2) Console.WriteLine("  -----");
            }
        }
    }
}