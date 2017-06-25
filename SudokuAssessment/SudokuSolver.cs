/// <summary>
/// This class is for the implementation of the logic and grouping of functions used to solve a Sudoku puzzle.
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuAssessment {
    class SudokuSolver {

        private SudokuBoard sudokuBoard; // The Sudoku board that the Sudoku solver will attempt to solve.

        /// <summary>
        /// Default constructor for SudokuSolver.
        /// </summary>
        /// <remarks>
        /// Made private as the SudokuSolver requires a board to operate.
        /// </remarks>
        private SudokuSolver() { }
    
        /// <summary>
        /// Constructor for SudokuSolver.
        /// </summary>
        /// <param name="sb"></param>
        public SudokuSolver(SudokuBoard sb) {
            this.sudokuBoard = sb;
        }

        public SudokuBoard gGtSudokuBoard() {
            return this.sudokuBoard;
        }

        /// <summary>
        /// Checks a game board for any duplicate
        /// </summary>
        /// <remarks>Checks the Sudoku game board for any duplicates prior to attempting to solve.</remarks>
        /// <returns>True if the game board contains no duplicates. False if the game board contains duplicates</returns>
        private bool ValidateBoard(){
            int currentValue; // Current value being checked for duplication

            for (int i = 0; i < SudokuBoard.ROWS; i++) {
                 for (int j = 0; j < SudokuBoard.COLUMNS; j++) {
                    currentValue = sudokuBoard.GetgameBoard()[i, j];

                    // Only check non-empty spaces (represented as 0s') for duplicates.

                    if (currentValue != 0)
                        if (CheckRow(currentValue, i) || CheckColumn(currentValue, j) || CheckSquare(currentValue, i, j))
                            return false;
                }
            }

           return true;
        }

        public bool Solve(){
            if (!ValidateBoard())
                return false;

            return Solve(sudokuBoard);
        }

        private bool Solve(SudokuBoard sb) {
            int[] indicies = new int[2]{0, 0};
            
            if (!CheckBlankSpaces(sb, indicies)){
                this.sudokuBoard.SetgameBoard(sb.GetgameBoard());
                return true;
            }

            for(int i = 1; i <= SudokuBoard.ROWS; i++) {
                if (!(CheckRow(i, indicies[0]) || CheckColumn(i, indicies[1]) || CheckSquare(i, indicies[0], indicies[1]))){
                    sb.GetgameBoard()[indicies[0], indicies[1]] = i;

                    if (Solve(sb))
                        return true;

                    sb.GetgameBoard()[indicies[0], indicies[1]] = 0;
                }
            }

            return false;
        }

        private bool CheckBlankSpaces(SudokuBoard sb, int[] indicies) {
            for (indicies[0] = 0; indicies[0] < SudokuBoard.ROWS; indicies[0]++)
                for (indicies[1] = 0; indicies[1] < SudokuBoard.COLUMNS; indicies[1]++)
                    if (sb.GetgameBoard()[indicies[0], indicies[1]] == 0)
                        return true;
            return false;
        }

        /// <summary>
        /// Checks a Sudokuboard to see if a duplicate value exists in the current 3x3 square.
        /// </summary>
        /// <param name="value">The value being searched for</param>
        /// <param name="row">The row index of the value being searched for.</param>
        /// <param name="column">The column index of the value being searched for.</param>
        /// <returns>True if duplicate value found. False if no duplicate found.</returns>    
        private bool CheckSquare(int value, int row, int column) {
            int startingRow = row - row % 3; // Starting row of the square
            int startingColumn = column - column % 3; // Starting column of the square

            // Check ahead in the 3x3 square for duplicate values
            
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (sudokuBoard.GetgameBoard()[row - startingRow, column - startingColumn] == value)
                        return true;

            return false;
        }

        /// <summary>
        /// Checks a Sudokuboard to see if a duplicate value exists in the current row.
        /// </summary>
        /// <param name="value">The value being searched for</param>
        /// <param name="row">The row index of the value being searched for.</param>
        /// <returns>True if duplicate value found. False if no duplicate found.</returns>       
        private bool CheckRow(int value, int row) {
            // Check ahead in the current row for duplicate values
            
            for (int i = row + 1; i < SudokuBoard.ROWS; i++)
                if (sudokuBoard.GetgameBoard()[row, i] == value)
                    return true;

            return false;
        }

        /// <summary>
        /// Checks a Sudokuboard to see if a duplicate valie exists in the current column.
        /// </summary>
        /// <param name="value">The value being searched for</param>
        /// <param name="column"> The column index of the value being searched for.</param>
        /// <returns>True if duplicate value found. False if no duplicate found.</returns>   
        private bool CheckColumn(int value, int column) {
            // Check down the current column for duplicate values

            for (int i = column + 1; i < SudokuBoard.COLUMNS; i++)
                if (sudokuBoard.GetgameBoard()[i, column] == value)
                    return true;

            return false;
        }
    }
}
