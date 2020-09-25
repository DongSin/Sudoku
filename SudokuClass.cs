using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuClass
{
    /// <summary>
    /// A class used to store sodoku data and perform sodoku functions
    /// </summary>
    public class Sudoku
    {
        private int[,] sdk;

        public Sudoku()
        {
            sdk = new int[9, 9];
        }

        public Sudoku(int[,] sdk)
        {
            if (sdk.Rank == 2 && sdk.GetLength(0) == 9 && sdk.GetLength(1) == 9)
                this.sdk = sdk;
        }

        public int[,] getSudoku()
        {
            return sdk;
        }

        public bool setSudoku(int[,] sdk)
        {
            bool set = false;

            if (sdk.Rank == 2 && sdk.GetLength(0) == 9 && sdk.GetLength(1) == 9)
            {
                this.sdk = sdk;
                set = true;
            }
            return set;
        }

        public bool placeNumber(int row, int col, int num)
        {
            if (sdk[row, col] != 0 || num < 1 || num > 9) return false;
            bool correct = true;
            //check row
            for (int i = 0; i < sdk.GetLength(0); i++)
            {
                if (sdk[row, i] == num)
                {
                    correct = false;
                    break;
                }

            }

            if (correct)
            {
                for (int i = 0; i < sdk.GetLength(1); i++)
                {
                    if (sdk[i, col] == num)
                    {
                        correct = false;
                        break;
                    }

                }
            }


            int rowRank = row / 3;
            int colRank = col / 3;

            for (int i = rowRank * 3; correct && i < (rowRank + 1) * 3; i++)
            {
                for (int j = colRank * 3; correct && j < (colRank + 1) * 3; j++)
                {
                    if (sdk[i, j] == num)
                    {
                        correct = false;
                    }

                }

            }


            if (correct) sdk[row, col] = num;
            return correct;
        }

        public override string ToString()
        {
            string str = "";

            for (int row = 0; row < sdk.GetLength(1); row++)
            {
                if (row % 3 == 0) str += "+---------+---------+---------+\n";

                for (int col = 0; col < sdk.GetLength(0); col++)
                {
                    if (col % 3 == 0) str += "|";
                    str += string.Format(" {0} ", sdk[row, col]);
                }

                str += "|\n";

            }

            str += "+---------+---------+---------+\n";

            return str;
        }

        public bool solve()
        {
            return solve(0, 0);
        }

        private bool solve(int row, int col)
        {
            bool solved = true;
            for (int i = 1; i < 10; i++)
            {
                if (placeNumber(row, col, i))
                {
                    Console.WriteLine(this.ToString());
                    if (col >= 8 && row >= 8) {
                        solved = true;
                        break;
                    }
                    int nrow = row, ncol = col;
                    do
                    {
                        if (ncol < 8)
                        {
                            ncol += 1;
                        }
                        else if (nrow < 8)
                        {
                            nrow += 1;
                            ncol = 0;
                        }

                    } while (sdk[nrow, ncol] != 0);
                    if (solved = solve(nrow, ncol)) break;
                    else
                    {
                        sdk[row, col] = 0;
                        solved = false;
                    }
                }
                else if (i == 9)
                {
                    solved = false;
                }
            }
            return solved;

        }
    }
}
