﻿/// <summary> 
///  Entry point for the Sodoku solver application for Gavant Software. Handles the input and output of the puzzle and solutions files.
/// </summary>
/// <remarks>
/// This program will attempt to solve each 9x9 Sudoku puzzle present in the 'puzzles' directory and represented as a .txt file.
/// Solved puzzles will be placed in the 'solutions' directory.
/// Each solution file will be saved in the format of {0}.sln.txt where {0} is the name of the original puzzle file.
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
            bool solveReturn = false; // Return value from an attempt so solve a puzzle

            try {
                // Load the name of each file in the puzzles directory

                puzzleEntries = Directory.GetFiles(puzzleDirectory);

                foreach(String puzzleFileName in puzzleEntries) {
                    try {
                        Console.WriteLine("Attempting to solve {0}...", puzzleFileName);

                       SudokuSolver ss = new SudokuSolver(new SudokuBoard(puzzleFileName));
                       var task = Task.Run(() => ss.Solve(ref solveReturn));

                        // Give the SudokuSolver 30 seconds to solve each puzzle
                        // If an attempted solve is taking too long, abort the attempt

                        if (! task.Wait(TimeSpan.FromSeconds(30))){
                            Console.WriteLine("Attempt to solve {0} taking too long, abandoning attempt...", puzzleFileName);
                        } else if (solveReturn) {
                            Console.WriteLine("Sudoku puzzle solved! Writing solution to solutions folder...\n");
                            ss.GetSolvedSudokuBoard().WriteSolutionFile(solutionsDirectory, puzzleFileName);
                        }                        
                        else
                           Console.WriteLine("Could not find a solution for {0}\n", puzzleFileName);
                        
                    } catch(Exception e) {
                        Console.Error.WriteLine("Error in puzzle file {0}: {1}\n", puzzleFileName, e.Message);
                    }

                }
                
            } catch(Exception e) {
                Console.Error.Write("Error accessing or reading puzzle file: {0}\n", e.Message);
            }

            Console.Write("No more puzzles to solve, press any key to exit...\n");
            Console.Read();
        }
    }
}
