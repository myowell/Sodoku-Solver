/// <summary>
/// This class represents a Sudoku gameboard for the Sodouku solver main function.
/// </summary>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuAssessment {
    public class SudokuBoard {
        private static int ROWS = 9;  // Number of rows in a Sudoku board
        private static int COLUMNS = 9; // Number of columns in a Sudoku board

        int[,] gameBoard = new int[ROWS, COLUMNS]; // Two dimensional array that will hold each value on the given Sudoku puzzle

        /// <summary>
        /// Default constructor for SudokuBoard
        /// </summary>
        /// <remarks>
        /// Made private to prevent the invalid creation of an empty board
        /// </remarks>
        private SudokuBoard() { }

        /// <summary>
        /// Class constructor for SudokuBoard
        /// </summary>
        /// <param name="puzzleContents"></param>
        public SudokuBoard(List<char> puzzleContents){

        }

    }
}
