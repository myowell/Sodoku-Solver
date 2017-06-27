/// <summary>
/// This class represents a Sudoku gameboard for the Sodouku solver main function.
/// Contains the actual game board as well as functions for its creation and output.
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
        public static int ROWS = 9;  // Number of rows in a Sudoku board
        public static int COLUMNS = 9; // Number of columns in a Sudoku board
        public static int SQUARES = 9; // Number of squares in a Sudoku board

        private int[,] gameBoard = new int[ROWS, COLUMNS]; // Two dimensional array that will hold each value on the given Sudoku puzzle

        /// <summary>
        /// Default constructor for SudokuBoard.
        /// </summary>
        /// <remarks>
        /// Made private to prevent the invalid creation of an empty board.
        /// </remarks>
        private SudokuBoard() { }

        /// <summary>
        /// Class constructor for SudokuBoard.
        /// </summary>
        /// <param name="puzzleFileName">A name and path of a puzzle file</param>
        public SudokuBoard(String puzzleFileName) {
            BuildPuzzleBoard(ParsePuzzleContents(puzzleFileName));
        }

        /// <summary>
        /// Returns the array containing the <see cref="SudokuBoard.gameBoard"/>.
        /// </summary>
        /// <returns><see cref="SudokuBoard.gameBoard"/></returns>
        public int[,] GetgameBoard() {
            return this.gameBoard;
        }

        /// <summary>
        /// Parses and verifies the contents of a file puzzle for insertion into the game board.
        /// </summary>
        /// <remarks>
        /// Opens a StreamReader for the given file name in the puzzle directory resource.
        /// Verifies that the puzzle file contains the correct (81) number of entries.
        /// Verifies that the puzzle file 
        /// </remarks>
        /// <exception cref="System.ArgumentException">Throw when an input file contains a charcter other than 1-9 OR 'X'.</exception>
        /// <exception cref="System.ArgumentException">Throw when an input file contains anything other than 81 characters.</exception>
        /// <param name="puzzleFileName"></param>
        /// <returns>A <see cref="List{char}"> containing the file contents.</returns>
        private List<char> ParsePuzzleContents(String puzzleFileName){
            List<char> charBuffer = new List<char>(); // Buffer to hold contents of a puzzle file
            StreamReader puzzleStream = new StreamReader(puzzleFileName); // Stream reader for the contents of a puzzle file

            // First load the file contents into a buffer and strip away all new line and return characters 
            // Verfies that file contents are either a single digit 1-9 or an 'X' to represent a blank space

            while (!puzzleStream.EndOfStream){
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

            return charBuffer;
        }

        /// <summary>
        /// Builds a new Sudoku board using the character buffer created with the <see cfref="SudokuBoard.ParsePuzzleContents"/> function.
        /// </summary>
        /// <remarks>
        /// Formats the raw character input of a puzzle file into the 9x9 <see cref="SudokuBoard.gameBoard"/>.
        /// Buffer contents will either be parsed into their integer values or made '0' to represent a blank space.
        /// Ensures the validity of a puzzle file's content.
        /// </remarks>
        /// <param name="charBuffer">A buffer containing the contents of a puzzle file</param>
        private void BuildPuzzleBoard(List<char> charBuffer) {
            for (int i = 0; i < ROWS * COLUMNS; i++) {
                int r = (i / 9 + 1) - 1; // Current Row
                int c = (i % 9 + 1) - 1; // Current Column
    
                gameBoard[r, c] = Char.IsNumber(charBuffer[i]) ? Int32.Parse(charBuffer[i].ToString()) : 0;
            }
        }

        /// <summary>
        /// Writes a <see cref="SudokuBoard.gameBoard"/> to a solution file.
        /// </summary>
        /// <param name="solutionsDirectory">The path to the solutions directory.</param>
        /// <param name="puzzleFileName">The path and name of a puzzle file.</param>
        public void WriteSolutionFile(string solutionsDirectory, string puzzleFileName) {
            string solutionFileExtension = ".sln.txt"; // File extenstion for a solution file.
            var fileName = puzzleFileName.Split('\\')[3]; // File name with the directory path removed
            string solutionFileName = solutionsDirectory + fileName.Split('.')[0] + solutionFileExtension; // Complete solution file name with the solutions directory path and extenstion

            StreamWriter solutionWriter = new StreamWriter(solutionFileName); // File stream write for the solution file

            for(int i = 0; i < ROWS; i++) {
                for(int j = 0; j < COLUMNS; j++){
                    solutionWriter.Write(gameBoard[i, j]);
                }
                solutionWriter.Write(Environment.NewLine);
            }
            solutionWriter.Close();
        }
    }
}