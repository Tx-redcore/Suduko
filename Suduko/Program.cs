using System;
using System.IO;

namespace Suduko
{
    class Program
    {
        static void Main(string[] args)
        {
            SudokuList sudokuList = new SudokuList();
            sudokuList.ReadSudoku();

            for (int i = 0; i < (sudokuList.convertedSudokus.Length/2); i++)
            {
                sudokuList.convertedSudokus[i, 0].sudokuHolderSolved = (int[,])sudokuList.convertedSudokus[i, 0].sudokuHolder.Clone();
                sudokuList.convertedSudokus[i, 0].recursiveSolve();
                sudokuList.convertedSudokus[i, 0].PrintSoduko(sudokuList.convertedSudokus[i, 0].sudokuHolderSolved);
                Console.WriteLine(" " + i);
            }     
        }
    }

}
