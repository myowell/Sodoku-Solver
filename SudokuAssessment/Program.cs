/// <summary> 
///  Entry point for the Sodoku solver application for Gavant Software.
/// </summary>
/// <remarks>
/// This program will attempt to solve each 9x9 Sudoku puzzle present in the 'puzzles' directory and represented as a .txt file.
/// Solzed puzzles will be placed in the 'solutions' directory.
/// Each solution file will be saved in the format of {0}.sln.txt where {0} is the name of the original puzzle file.
/// It is assumed that each puzzle input will be 9x9.
/// Author: Michael Yowell - michael.yowell@gmail.com
/// </remarks>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuAssessment {
    class Program {
        static void Main(string[] args) {
            String puzzleDirectory = Properties.Resources.puzzleDirectoryPath; // Name of the directory that holds each Sudoku puzzle
            String solutionsDirectory = Properties.Resources.solutionsDirectoryPath; // Name of the directory that will hold each solution file
            String[] puzzleEntries; // Listing of file names found in the puzzle directory
            List<char> charBuffer = new List<char>(); // Buffer to hold contents of a puzzle file

            try {

                // Load the name of each file in the puzzles directory

                puzzleEntries = Directory.GetFiles(puzzleDirectory);

                foreach(String fileName in puzzleEntries) {
                    StreamReader puzzleStream = new StreamReader(fileName);

                    // Read all contents minus returns and newlines into the string buffer

                    while(puzzleStream.Peek() > 0){
                        char c = (char)puzzleStream.Read();
                        if (c != '\n' && c != '\r')
                            charBuffer.Add(c);
                    }

                    charBuffer.ForEach(Console.WriteLine);
                }
                
            } catch(Exception e) {
                Console.Error.Write("Error accessing or reading puzzle files: {0}\n", e.Message);
            }

            Console.Read();
        }
    }
}
