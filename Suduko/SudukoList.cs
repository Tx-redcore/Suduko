using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Suduko
{
    public class SudokuList
    {
        public string[,] sudokus = new string[102, 2]; //fix the hard coding here
        public Sudoku[,] convertedSudokus = new Sudoku[102, 2];

        public void ReadSudoku()
        {
            StreamReader fileReader = new StreamReader("C:\\Users\\Tawi\\Downloads\\sudoku-subset.csv"); //insert position of file
            int x = 0; //counter

            while (!fileReader.EndOfStream)
            {
                string sudokuLine = fileReader.ReadLine();
                string[] tempSudoku = sudokuLine.Split(','); //gives eithe 1 sudoku with 1 solution, or just a sudoku


                if(tempSudoku != null)
                {
                    if(tempSudoku[0].Contains('0'))
                    {
                        if (tempSudoku.Length >= 2)
                        {
                            // sets this if it has solution on right
                            sudokus[x, 0] = tempSudoku[0];
                            if(sudokus[x, 1] != " ")
                            {
                                sudokus[x, 1] = tempSudoku[1];
                            }
                        }
                        else if (tempSudoku.Length < 2 )
                        {
                            //sets this if it does not have solution
                            sudokus[x, 0] = tempSudoku[0];
                            sudokus[x, 1] = "";
                        }
                        x++;
                    }

                }
            }
            ConvertSudoku();
        }


        public void ConvertSudoku()
        {
            for(int i = 0; i < (sudokus.Length / 2); i++)
            {
                Sudoku sudoku = new Sudoku();
                Sudoku sudokuSolution = new Sudoku();

                
                char[] tempList = sudokus[i, 0].ToCharArray();
                int[] tempListInt = new int[81];
                int[] tempListIntSolution = new int[81];
                for (int b = 0; b < tempList.Length; b++) { tempListInt[b] = int.Parse(tempList[b].ToString()); } //this works

                if(sudokus[i, 1] != "")
                {
                    //means solution exists
                    char[] tempListSolution = sudokus[i, 1].ToCharArray();
                    for (int b = 0; b < tempListSolution.Length; b++) 
                    { 
                        tempListIntSolution[b] = int.Parse(tempListSolution[b].ToString()); 
                    } //this works
                }

                int counter = 0;
                //first x row of matrix
                for (int x = 0; x < 9; x++)
                {
                    for (int y = 0; y < 9; y++)
                    {
                        // her you get(x, y)
                        sudoku.sudokuHolder[x, y] = tempListInt[counter];
                        if(sudokus[i, 1] != "")
                        {
                            sudokuSolution.sudokuHolder[x, y] = tempListIntSolution[counter];
                        }
                        counter++;

                    }
                    
                }

                //temp, dosent convert solution yet
                convertedSudokus[i, 0] = sudoku;
                if(sudokus[i, 1] != "") { convertedSudokus[i, 1] = sudokuSolution; }
            }
        }
    }
}
