using Microsoft.VisualStudio.TestTools.UnitTesting;
using Suduko;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SudokuTests
{
    [TestClass]
    public class SudokuTest
    {
        [TestMethod]
        public void Sudoku_Compare_To_Solution()
        {
            //this checks if the solved sudokus are equal to their solution if given
            //can throw an error if its inputted with a sudoku that is not a true soduka and has multiple solutions, because then the given solutions might not be equal
            SudokuList list = new SudokuList();
            list.ReadSudoku();
            List<bool> checkList = new List<bool>();

            for (int i = 0; i < (list.sudokus.Length) / 2; i++)
            {
                if (list.sudokus[i, 1] != "")
                {
                    list.convertedSudokus[i, 0].sudokuHolderSolved = (int[,])list.convertedSudokus[i, 0].sudokuHolder.Clone();
                    list.convertedSudokus[i, 0].recursiveSolve();

                    int[,] actual = list.convertedSudokus[i, 0].sudokuHolderSolved;
                    int[,] expected = list.convertedSudokus[i, 1].sudokuHolder;

                    bool isEqual =
                        actual.Rank == expected.Rank &&
                        Enumerable.Range(0, actual.Rank).All(dimension => actual.GetLength(dimension) == expected.GetLength(dimension)) &&
                        actual.Cast<int>().SequenceEqual(expected.Cast<int>());

                    checkList.Add(isEqual);

                    if (!isEqual)
                    {
                        //Can be if its false that the sudoku has more than 1 answer
                        Console.WriteLine("not true at position: " + i);
                    }

                }
            }

            bool allaretrue = !checkList.Contains(false);
            Assert.IsTrue(allaretrue, "all objects in list does not go through");
        }


        [TestMethod]
        public void Sudoku_Valid_Solutions() 
        {
            //this check if all solved sodukos that are solved are valid
            SudokuList list = new SudokuList();
            list.ReadSudoku();

            List<bool> checkList = new List<bool>();

            for (int i = 0; i < (list.sudokus.Length) / 2; i++)
            {
                
                    list.convertedSudokus[i, 0].sudokuHolderSolved = (int[,])list.convertedSudokus[i, 0].sudokuHolder.Clone();
                    list.convertedSudokus[i, 0].recursiveSolve();

                    bool isEqual = list.convertedSudokus[i, 0].ValidSolution(list.convertedSudokus[i, 0].sudokuHolderSolved);

                    checkList.Add(isEqual);

                    if (!isEqual)
                    {
                        Console.WriteLine("not true at position: " + i);
                    }
            }

            bool allaretrue = !checkList.Contains(false);
            Assert.IsTrue(allaretrue, "all objects does not have valid solution");
        }

    }
}


    
