// Index.cshtml.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace XOGame.Pages
{
    public class IndexModel : PageModel
    {
        public static char[,] Board { get; set; } = new char[3, 3];
        public char CurrentPlayer { get; set; } = 'X'; // Initial player
        public bool GameEnded { get; set; } = false;
        public string GameResultMessage { get; set; } = "";

        public void OnGet(int row, int col,char current)
        {
            CurrentPlayer = current;
            //Board = board;
            //CurrentPlayer = current;
            if (Board[row, col] == '\0') // Check if the cell is empty
            {
                Board[row, col] = CurrentPlayer; // Set player's mark

                // Check for win or draw
                if (CheckForWin() || CheckForDraw())
                {
                    GameEnded = true;
                    GameResultMessage = (CheckForWin()) ? $"Player {CurrentPlayer} wins!" : "It's a draw!";
                }
                else
                {
                    // Switch players
                    CurrentPlayer = (CurrentPlayer == 'X') ? 'O' : 'X';
                }
            }
            // Other game logic
        }

        public IActionResult OnPostRestartGame()
        {
            Board = new char[3, 3];
            CurrentPlayer = 'X';
            GameEnded = false;
            GameResultMessage = "";
            return RedirectToPage("/Index");
        }

        private bool CheckForWin()
        {
            // Implement win condition checking logic (e.g., rows, columns, diagonals)
            // Return true if a player wins, false otherwise

            // Example: Check rows for a win
            for (int row = 0; row < 3; row++)
            {
                if (Board[row, 0] != '\0' && Board[row, 0] == Board[row, 1] && Board[row, 1] == Board[row, 2])
                {
                    return true;
                }
            }

            // Check columns for a win
            for (int col = 0; col < 3; col++)
            {
                if (Board[0, col] != '\0' && Board[0, col] == Board[1, col] && Board[1, col] == Board[2, col])
                {
                    return true;
                }
            }

            // Check diagonals for a win
            if (Board[0, 0] != '\0' && Board[0, 0] == Board[1, 1] && Board[1, 1] == Board[2, 2])
            {
                return true;
            }
            if (Board[0, 2] != '\0' && Board[0, 2] == Board[1, 1] && Board[1, 1] == Board[2, 0])
            {
                return true;
            }

            return false;
        }

        private bool CheckForDraw()
        {
            // Check if the board is full (i.e., no empty cells)
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (Board[row, col] == '\0')
                    {
                        return false; // Empty cell found, game not drawn
                    }
                }
            }
            return true; // All cells filled, game drawn
        }
    }
}
