/// <summary>
/// This class represents a Sudoku gameboard for the Sodouku solver main function.
/// </summary>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;

namespace SudokuAssessment {
    public class SudokuBoard {
        private static int ROWS = 9;  // Number of rows in a Sudoku board
        private static int COLUMNS = 9; // Number of columns in a Sudoku board

        private int[,] gameBoard = new int[ROWS, COLUMNS]; // Two dimensional array that will hold each value on the given Sudoku puzzle

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
        /// <param name="puzzleStream"></param>
        public SudokuBoard(String puzzleFileName) {
            BuildPuzzleBoard(puzzleFileName);
        }

        /// <summary>
        /// Builds a new Sudoku board using one of the files in the resource /puzzles directory
        /// </summary>
        /// <remarks>
        /// Opens a file stream for each file, loads their contents into a buffer, and fills in the actual game board.
        /// Ensures the validity of a puzzle file's content.
        /// </remarks>
        /// <exception cref="System.ArgumentException">Throw when an input file contains a charcter other than 1-9 OR 'X'.</exception>
        /// <exception cref="System.ArgumentException">Throw when an input file contains anything other than 81 characters.</exception>
        /// <param name="puzzleFileName"></param>

        private void BuildPuzzleBoard(String puzzleFileName) {
            List<char> charBuffer = new List<char>(); // Buffer to hold contents of a puzzle file
            StreamReader puzzleStream = new StreamReader(puzzleFileName); // Stream reader for the contents of a puzzle file

            // First load the file contents into a buffer and strip away all new line and return characters 
            // Verfies that file contents are either a single digit 1-9 or an 'X' to represent a blank space

            while (!puzzleStream.EndOfStream) {
                char c = (char)puzzleStream.Read(); // Current character read from the file stream
                if (c != '\n' && c != '\r')
                    switch (c) {
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                        case 'X':
                        charBuffer.Add(c);
                        break;
                        default:
                        throw new System.ArgumentException("Invalid Character In Puzzle File", String.Concat(puzzleFileName + " - " + c));
                    }
            }

            puzzleStream.Close();

            // The board's dimensions must be 9x9

            if (charBuffer.Count() != ROWS * COLUMNS)
                throw new System.ArgumentException("Invalid Game Board Size", puzzleFileName);

            System.Collections.IEnumerator en = charBuffer.GetEnumerator(); // Enumerator for the character buffer

            // With the character contents of the puzzle file verified, convert them into integers and fill out the contents of game board
            // Replace each "X" with a 0 to represent a blank space

            for (int i = 0; i < ROWS * COLUMNS; i++) {
                int r = (i / 9 + 1) - 1; // Current Row
                int c = (i % 9 + 1) - 1; // Current Column
    
                gameBoard[r, c] = Char.IsNumber(charBuffer[i]) ? Int32.Parse(charBuffer[i].ToString()) : 0;
            }

        }
    }
}