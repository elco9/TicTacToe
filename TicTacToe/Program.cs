using System;

namespace TicTacToe
{
    internal class Program
    {
        static char[] TTT = { '*', 'X', 'O' };
        static int currentPlayer = 1; // 1 for player X, 2 for player O
        static int[,] array =
        {
            { 0, 2, 0, 0 },{ 1, 2, 0, 0 },{ 2, 2, 0, 0 },
            { 0, 1, 0, 0 },{ 1, 1, 0, 0 },{ 2, 1, 0, 0 },
            { 0, 0, 0, 0 },{ 1, 0, 0, 0 },{ 2, 0, 0, 0 }
        };

        static void Main(string[] args)
        {
            ShowBoard();
            GetKey();
        }

        // Display the Tic Tac Toe board
        static void ShowBoard()
        {
            Console.Clear(); // Clear console before drawing the updated board

            // Iterate through the cells in the array
            for (int i = 0; i < array.GetLength(0); i++)
            {
                // Display the current player's symbol in the cell
                Console.SetCursorPosition(array[i, 0] * 3, array[i, 1]);
                Console.Write(TTT[array[i, 2]]);
            }

            Console.SetCursorPosition(0, array[0, 1] + 1);
        }

        // Get the player's move input
        static void GetKey()
        {
            ConsoleKeyInfo key = Console.ReadKey();
            int position = -1;

            // Map the numpad keys to the corresponding positions in the array
            switch (key.Key)
            {
                case ConsoleKey.NumPad7:
                    position = 6;
                    break;
                case ConsoleKey.NumPad8:
                    position = 7;
                    break;
                case ConsoleKey.NumPad9:
                    position = 8;
                    break;
                case ConsoleKey.NumPad4:
                    position = 3;
                    break;
                case ConsoleKey.NumPad5:
                    position = 4;
                    break;
                case ConsoleKey.NumPad6:
                    position = 5;
                    break;
                case ConsoleKey.NumPad1:
                    position = 0;
                    break;
                case ConsoleKey.NumPad2:
                    position = 1;
                    break;
                case ConsoleKey.NumPad3:
                    position = 2;
                    break;
            }

            // Check if the selected position is valid and not already taken
            if (position != -1 && array[position, 2] == 0)
            {
                // Set the current player's symbol in the selected position
                array[position, 2] = currentPlayer;
                ShowBoard();

                // Check for a winner or draw
                if (CheckForWinner())
                {
                    Console.SetCursorPosition(0, array[0, 1] + 3);
                    Console.WriteLine($"Player {TTT[currentPlayer]} wins!");
                    return;
                }

                if (IsBoardFull())
                {
                    Console.SetCursorPosition(0, array[0, 1] + 3);
                    Console.WriteLine("It's a draw!");
                    return;
                }

                // Switch turn to the next player
                currentPlayer = (currentPlayer == 1) ? 2 : 1;
                GetKey(); // Keep getting input in a loop
            }
            else
            {
                Console.SetCursorPosition(0, array[0, 1] + 3);
                Console.WriteLine("Invalid move. Cell already taken or invalid input. Try again.");
                GetKey(); // Keep getting input in a loop
            }
        }

        // Check for a winner by examining rows, columns, and diagonals
        static bool CheckForWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (array[i, 2] != 0 &&
                    (array[i, 2] == array[i + 3, 2] && array[i, 2] == array[i + 6, 2] || // Check columns
                    (i == 0 && array[i, 2] == array[i + 4, 2] && array[i, 2] == array[i + 8, 2]) || // Check diagonal from top-left to bottom-right
                    (i == 2 && array[i, 2] == array[i + 2, 2] && array[i, 2] == array[i + 4, 2]))) // Check diagonal from top-right to bottom-left
                {
                    return true;
                }

                if (array[i * 3, 2] != 0 && array[i * 3, 2] == array[i * 3 + 1, 2] && array[i * 3, 2] == array[i * 3 + 2, 2]) // Check rows
                {
                    return true;
                }
            }

            return false;
        }

        // Check if the board is full (a draw)
        static bool IsBoardFull()
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (array[i, 2] == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}