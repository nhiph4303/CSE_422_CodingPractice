using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingPracticeLab1.Problems
{
    internal class WordSearch
    {
        public bool Exist(char[][] board, string word)
        {
            int m = board.Length, n = board[0].Length;

            // Helper function for DFS
            bool Backtrack(int i, int j, int index)
            {
                // If we have matched all characters in the word
                if (index == word.Length)
                {
                    return true;
                }

                // If out of bounds or character doesn't match
                if (i < 0 || j < 0 || i >= m || j >= n || board[i][j] != word[index])
                {
                    return false;
                }

                // Mark the current cell as visited by temporarily changing the character
                char temp = board[i][j];
                board[i][j] = '#'; // Temporary marker

                // Explore the four possible directions: up, down, left, right
                bool result = Backtrack(i + 1, j, index + 1) ||
                 Backtrack(i - 1, j, index + 1) ||
                 Backtrack(i, j + 1, index + 1) ||
                 Backtrack(i, j - 1, index + 1);

                // Backtrack, restore the original character
                board[i][j] = temp;

                return result;
            }

            // Iterate over every cell in the grid
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    // Start the search from the current cell
                    if (board[i][j] == word[0] && Backtrack(i, j, 0))
                    {
                        return true;
                    }
                }
            }

            return false; // If the word wasn't found
        }
    }
}
