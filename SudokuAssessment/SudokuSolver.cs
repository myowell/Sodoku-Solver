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
        private const int BLANK = 0; // The number representing a blank space

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
        /// <param name="sb">An instance of <see cref="SudokuBoard"/></param>
        public SudokuSolver(SudokuBoard sb) {
            this.sudokuBoard = sb;
        }

        /// <summary>
        /// Return the <see cref="SudokuBoard"/> belonging to this instance of <see cref="SudokuSolver"/>.
        /// </summary>
        /// <returns><see cref="SudokuBoard"/></returns>
        public SudokuBoard GetSolvedSudokuBoard() {
            return this.sudokuBoard;
        }

        /// <summary>
        /// Return the result of <see cref="SudouSolver.Solve()"/>.
        /// </summary>
        /// <remarks>
        /// Public version of the overloaded <see cref="SudokuSolver.Solve(SudokuBoard)"/> function.
        /// </remarks>
        /// <param name="ret"> A pointer to a boolean value that will contain the result of an attempt to solve a Sudoku puzzle.</param>
        public void Solve(ref bool ret) {
            ret = Solve(this.sudokuBoard);
        }

        /// <summary>
        /// Function to solve a Sudoku Puzzle.
        /// </summary>
        /// <remarks>
        /// Private version of the overloaded <see cref="SudokuSolver.Solve"/> function.
        /// Attempts to solve a Sudoku puzzle by the board with each unassigned value in each blank space.
        /// Makes recursive calls to the Solve() function with each version of the board until a solution is found.
        /// If any iteration at a blank space is found to not be a correct solution, it is discarded and the space reset to blank
        /// </remarks>
        /// <param name="sb">The <see cref="SudokuSolver"/>'s instance of <see cref="SudokuBoard"/></param>
        /// <returns>
        /// True if no blank spaces are found - meaning the puzzle has been solved.
        /// False if a puzzle cannot been solved.
        /// </returns>
        public bool Solve(SudokuBoard sb) {
            int row = 0; // Row index for blank space
            int column = 0; // Column index for blank space

            // If no blank spaces are found in the current iteration of the board, it is solved
            // If a blank space is found, the row and column values will now point to its location

            if (!CheckBlankSpaces(sb, ref row, ref column))
                return true;

            // For values 1-9

            for(int i = 1; i <= SudokuBoard.ROWS; i++) {
                
                // If the given value is not present in the row, column, and 9x9 square set the current blank space to its value

                if (!CheckRow(i, row) && !CheckColumn(i, column) && !CheckSquare(i, row, column)){
                    sb.GetgameBoard()[row, column] = i;

                    // If each subsequent recursive call returns true, the board is solved

                    if (Solve(sb))
                        return true;

                    // If the value not viable in the solution set the space to blank once more

                    sb.GetgameBoard()[row, column] = BLANK;
                }
            }

            // Return false to reset a blank space in the event of an invalid solution attempt

            return false;
        }


        /// <summary>
        /// Check a <see cref="SudokuBoard"/> for any blank spaces.
        /// </summary>
        /// <param name="sb">An instance of <see cref="SudokuBoard"/>.</param>
        /// <param name="row">A pointer to a row index.</param>
        /// <param name="column">A pointer to a column index.</param>
        /// <returns>
        /// True if a blank space is found.
        /// False if no blank spaces are found.
        /// </returns>
        private bool CheckBlankSpaces(SudokuBoard sb, ref int row, ref int column) {
            for (row = 0; row < SudokuBoard.ROWS; row++)
                for (column = 0; column < SudokuBoard.COLUMNS; column++)
                    if (sb.GetgameBoard()[row, column] == BLANK)
                        return true;
            return false;
        }

        /// <summary>
        /// Checks a Sudoku board to see if a duplicate already value exists in the current 3x3 square.
        /// </summary>
        /// <param name="value">The value being searched for.</param>
        /// <param name="row">The row index of the value being searched for.</param>
        /// <param name="column">The column index of the value being searched for.</param>
        /// <returns>True if duplicate value found. False if no duplicate found.</returns>    
        private bool CheckSquare(int value, int row, int column) {
            int startingRow = row - row % 3; // Starting row of the 3x3 square
            int startingColumn = column - column % 3; // Starting column of the 3x3 square
            
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (sudokuBoard.GetgameBoard()[i + startingRow, j + startingColumn] == value)
                        return true;

            return false;
        }

        /// <summary>
        /// Checks a Sudokuboard to see if a duplicate value already exists in the current row.
        /// </summary>
        /// <param name="value">The value being searched for</param>
        /// <param name="row">The row index of the value being searched for.</param>
        /// <param name="column">The column index of the value being searched for.</param>
        /// <returns>True if duplicate value found. False if no duplicate value found.</returns>       
        private bool CheckRow(int value, int row) {
            for (int i = 0; i < SudokuBoard.ROWS; i++)
                if (sudokuBoard.GetgameBoard()[row, i] == value)
                    return true;

            return false;
        }

        /// <summary>
        /// Checks a Sudokuboard to see if a duplicate valie exists in the current column.
        /// </summary>
        /// <param name="value">The value being searched for</param>
        /// <param name="row">The row index of the value being searched for.</param>
        /// <param name="column"> The column index of the value being searched for.</param>
        /// <returns>True if duplicate value found. False if no duplicate found.</returns>   
        private bool CheckColumn(int value, int column) {
            for (int i = 0; i < SudokuBoard.COLUMNS; i++)
                if (sudokuBoard.GetgameBoard()[i, column] == value)
                    return true;

            return false;
        }
    }
}