using System;

namespace Week3ArraysSorting
{
    /// <summary>
    /// Board Game implementation for Assignment 2 Part A
    /// Demonstrates multi-dimensional arrays with interactive gameplay
    /// 
    /// Learning Focus: 
    /// Learning Focus: 
    /// - Multi-dimensional array manipulation (char[,])
    /// - Console rendering and user input
    /// - Game state management and win detection
    /// 
    /// Choose ONE game to implement:
    /// - Tic-Tac-Toe (3x3 grid)
    /// - Connect Four (6x7 grid with gravity)
    /// - Or something else creative using a 2D array! (I need to be able to understand the rules from your instructions)
    /// </summary>
    public class BoardGame
    {
        private char[,] board = new char[3, 3];
        
        private char currentPlayer = 'X';
        private bool gameOver = false;
        private int turnCount = 0;

        /// <summary>
        /// Constructor - Initialize the board game
        /// </summary>
        public BoardGame()
        {
            // For Tic-Tac-Toe or Connect Four, fill with empty spaces or dots
            // ❌ ⭕ -> use for Tic-Tac-Toe if you'd like for each square/player and the white box from array example
        }
        
        /// <summary>
        /// Main game loop - handles the complete game session
        /// </summary>
        public void StartGame()
        {
            Console.Clear();
            Console.WriteLine("=== BOARD GAME (Part A) ===");
            Console.WriteLine();
            
            DisplayInstructions();
            PlayOneGame();

            Console.WriteLine("Thanks for playing!");
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }
        
        /// <summary>
        /// Display game instructions and controls
        /// </summary>
        private void DisplayInstructions()
        {
            // Example for Tic-Tac-Toe:
            Console.WriteLine("TIC-TAC-TOE RULES:");
            Console.WriteLine("- Players take turns placing X and O");
            Console.WriteLine("- Enter row and column (0-2) when prompted");
            Console.WriteLine("- First to get 3 in a row wins!");
            
            Console.WriteLine();
        }
        
        /// <summary>
        /// Play one complete game until win/draw/quit
        /// </summary>
        private void PlayOneGame()
        {
            // Set game state
            gameOver = false;
            currentPlayer = 'X';
            turnCount = 0;
            initializeBoard();

            // Game loop structure:
            while (!gameOver)
            {
                RenderBoard();
                UpdateBoard(GetPlayerMove());
                CheckWinCondition();
                SwitchPlayer();
                turnCount++;
            }

            // If play again is true, start a new game
            if (AskPlayAgain()) PlayOneGame();
        }

        /// <summary>
        /// Update the board with the player's move
        /// </summary>
        private void UpdateBoard(int move)
        {
            var (row, col) = MapPlayermove(move);
            board[row, col] = currentPlayer;
        }

        /// <summary>
        /// Render the current board state to console
        /// </summary>
        private void RenderBoard()
        {
            // Render the board
            Console.WriteLine("----+---+----");
            for (int i = 0; i < 3; i++)
            {
                Console.Write("|");

                for (int j = 0; j < 3; j++)
                {
                    Console.Write($" {board[i, j]} |");
                }

                Console.WriteLine();

                Console.WriteLine("----+---+----");
            }
        }

        /// <summary>
        /// Initialize the board with position numbers
        /// </summary>
        private void initializeBoard()
        {
            // Fill the board with position numbers 1-9
            char position = '1';
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Assign the character and increment it for the next cell.
                    board[i, j] = position++;
                }
            }
        }

        /// <summary>
        /// Get and validate player move input
        /// </summary>
        private int GetPlayerMove()
        {
            while (true)
            {
                Console.Write($"Player {currentPlayer}, enter a position (1-9): ");
                string input = Console.ReadLine();

                // Parse and validate input
                // not null or empty
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 9.");
                    continue;
                }

                // is an integer
                if (!int.TryParse(input, out int move)) 
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 9.");
                    continue;
                }

                // in range 1-9
                if (move < 1 || move > 9)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 9.");
                    continue;
                }

                // position not already taken
                var (row, col) = MapPlayermove(move);
                if (!char.IsDigit(board[row, col]))
                {
                    Console.WriteLine("That position is already taken. Please choose another.");
                    continue;
                }

                return move;
            }
        }

        /// <summary>
        /// Map player move (1-9) to board coordinates (row, col)
        /// </summary>
        private (int row, int col) MapPlayermove(int move)
        {
            // Convert move (1-9) to (row, col) for 3x3 board
            int row = (move - 1) / 3;
            int col = (move - 1) % 3;
            return (row, col);
        }

        /// <summary>
        /// Check if current board state has a winner or draw
        /// </summary>
        private void CheckWinCondition()
        {
            // Check rows for a win
            for (int i = 0; i < 3; i++)
            {
                if(board[i,0] == board[i,1] && board[i,1] == board[i,2] && !char.IsDigit(board[i,0]))
                {
                    gameOver = true;
                    Console.WriteLine($"Player {board[i,0]} wins!");
                    return;
                }
            }

            // Check columns for a win
            for (int j = 0; j < 3; j++)
            {
                if(board[0,j] == board[1,j] && board[1,j] == board[2,j] && !char.IsDigit(board[0,j]))
                {
                    gameOver = true;
                    Console.WriteLine($"Player {board[0,j]} wins!");
                    return;
                }
            }

            // Check diagonals for a win
            if((board[0,0] == board[1,1] && board[1,1] == board[2,2] && !char.IsDigit(board[0,0])) ||
               (board[0,2] == board[1,1] && board[1,1] == board[2,0] && !char.IsDigit(board[0,2])))
            {
                gameOver = true;
                Console.WriteLine($"Player {board[1,1]} wins!");
                return;
            }

            // All positions filled
            if (turnCount == 8)
            {
                gameOver = true;
                Console.WriteLine("It's a draw!");
                return;
            }
        }
        
        /// <summary>
        /// Ask player if they want to play another game
        /// </summary>
        private bool AskPlayAgain()
        {
            // Prompt to play again
            Console.Write("Play again? (y/n): ");
            string input = Console.ReadLine();

            // Validate input
            if (input != null && (input.ToLower() == "y" || input.ToLower() == "yes")) return true;
            else return false;
        }
        
        /// <summary>
        /// Switch to the next player's turn
        /// </summary>
        private void SwitchPlayer()
        {
            if(currentPlayer == 'X') currentPlayer = 'O';
            else currentPlayer = 'X';
        }
    }
}