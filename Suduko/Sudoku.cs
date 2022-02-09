using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Suduko
{
    public class Sudoku
    {
        public int[ , ] sudokuHolder = new int[9,9];
        public int[ , ] sudokuHolderSolved = new int[9, 9];

        public void PrintSoduko(int[,] matrix)
        {
            for(int x = 0; x < 9; x++)
            {
                string line = "";
                for(int y = 0; y < 9; y++)
                {
                    line += matrix[x, y].ToString() + " ";
                }
                Console.WriteLine(line);
            }
        }


        public bool validNumber(int positionX, int positionY, int value)
        {
            for (int i = 0; i < 9; i++)
            {
                if(sudokuHolderSolved[positionX, i] == value)
                {
                    //checks horizontally for number you try to insert
                    return false;
                }
            }
            for(int i = 0; i < 9; i++)
            {
                if(sudokuHolderSolved[i, positionY] == value)
                {
                    //checks vertically
                    return false;
                }
            }

            //checks for which block it resides in by diving by 3 in the X, if x is 6 dividing by 3 will make it in the second to the right
            int xVal = (int)(Math.Floor((double)positionX / 3)) * 3;
            int yVal = (int)(Math.Floor((double)positionY / 3)) * 3;

            for(int i = 0; i < 3; i++)
            {
                for (int l = 0; l < 3; l++)
                {
                    if(sudokuHolderSolved[xVal + i, yVal + l] == value)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool recursiveSolve() 
        {

            for (int x = 0; x<9; x++) 
            {
                for (int y = 0; y < 9; y++) { 

                    if (sudokuHolderSolved[x, y] == 0) 
                    {
                        //position is empty
                        for (int i = 1; i <= 9; i++) 
                        {
                            if (validNumber(x, y, i))
                            {
                                //if number is valid at this position
                                //insert number at position
                                sudokuHolderSolved[x, y] = i; 
                                //now recurse
                                if (recursiveSolve())
                                {
                                    return true;
                                }else
                                {
                                    sudokuHolderSolved[x, y] = 0;
                                }
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        public bool ValidSolution(int[,] matrix)
        {
            for (int x = 0; x < 9; x++)
            {
                List<int> testRow = new List<int>();
                for(int i = 0; i< 9; i++) { testRow.Add(matrix[x, i]); }

                //is there duplicate
                IEnumerable<int> duplicates = testRow.GroupBy(x => x)
                                        .Where(g => g.Count() > 1)
                                        .Select(x => x.Key);
                if(duplicates.Count() > 0)
                {
                    return false;
                }
            }

            for (int y = 0; y < 9; y++)
            {
                List<int> testCol = new List<int>();
                for (int i = 0; i < 9; i++) { testCol.Add(matrix[i, y]); }

                //is there duplicate
                IEnumerable<int> duplicates = testCol.GroupBy(x => x)
                                        .Where(g => g.Count() > 1)
                                        .Select(x => x.Key);
                if (duplicates.Count() > 0)
                {
                    return false;
                }
            }

            //checks for which block it resides in by diving by 3 in the X, if x is 6 dividing by 3 will make it in the second to the right
            for (int a = 0; a<9; a++)
            {
                for (int b = 0; b<9; b++)
                {
                    int xVal = (int)(Math.Floor((double)a / 3)) * 3; //this is subsquare position in x direction
                    int yVal = (int)(Math.Floor((double)b / 3)) * 3; //this is subsquare position in y direction

                    List<int> subSquare = new List<int>();

                    for (int i = 0; i < 3; i++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            //add into list
                            subSquare.Add(matrix[xVal + i, yVal + l]);
                        }
                    }

                    IEnumerable<int> duplicates = subSquare.GroupBy(x => x)
                                        .Where(g => g.Count() > 1)
                                        .Select(x => x.Key);
                    if (duplicates.Count() > 0)
                    {
                        return false;
                    }

                }
            }

            return true;
        }
    }
}
