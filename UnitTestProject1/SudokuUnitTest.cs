using System;
using SudokuAssessment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1 {
    [TestClass]
    public class UnitTest1 {

        // Test a file with a valid puzzle

        [TestMethod]
        public void TestGoodFile() {
            string fileName = @"../../TestPuzzles/puzzle1.txt";
            SudokuBoard sb = new SudokuBoard(fileName);
            SudokuSolver ss = new SudokuSolver(sb);
            bool result = false;

            ss.Solve(ref result);

            Assert.AreEqual(result, true);
        }

        // Test a file with duplicate entries in the rows, columns and squares

        [TestMethod]
        public void TestBadFile() {
            string fileName = @"../../TestPuzzles/InvalidPuzzle.txt";
            SudokuBoard sb = new SudokuBoard(fileName);
            SudokuSolver ss = new SudokuSolver(sb);
            bool result = false;

            ss.Solve(ref result);

            Assert.AreEqual(result, false);
        }

        // Test a file with a puzzle not in 9x9 format

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBadFileFormat() {
            string fileName = @"../../TestPuzzles/InvalidPuzzleFormat.txt";
            SudokuBoard sb = new SudokuBoard(fileName);
            SudokuSolver ss = new SudokuSolver(sb);
            bool result = false;

            ss.Solve(ref result);
        }

        // Test a file with a puzzle containing invalid characters

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInvalidCharacters() {
            string fileName = @"../../TestPuzzles/InvalidCharacters.txt";
            SudokuBoard sb = new SudokuBoard(fileName);
            SudokuSolver ss = new SudokuSolver(sb);
            bool result = false;

            ss.Solve(ref result);
        }

        // Test a puzzle with a file containing a completely empty board

        [TestMethod]
        public void TestEmptyBoard() {
            string fileName = @"../../TestPuzzles/EmptyBoard.txt";
            SudokuBoard sb = new SudokuBoard(fileName);
            SudokuSolver ss = new SudokuSolver(sb);
            bool result = false;

            ss.Solve(ref result);

            Assert.AreEqual(result, true);
        }
    }
}
