using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary
{
    public class Matrix
    {
        public static decimal GetDeterminant(ref List<List<decimal>> matrix)
        {
            int rowCount = matrix.Count;
            int columnCount = matrix.ElementAt(0).Count;

            if (rowCount != columnCount) { throw new ArgumentException("Must be a square matrix!"); }

            if (rowCount == 1) return matrix.ElementAt(0).ElementAt(0);

            decimal determinant = 0;

            for (int i = 0; i < columnCount; i++)
            {
                List<List<decimal>> minor = GetMinor(ref matrix, 0, i);
                determinant += ((decimal)Math.Pow(-1, (i + 2d)) * matrix.ElementAt(0).ElementAt(i) * GetDeterminant(ref minor));
            }

            return determinant;
        }

        public static List<List<decimal>> GetMinor(ref List<List<decimal>> matrix, int row, int column)
        {
            int rowCount = matrix.Count;
            int columnCount = matrix.ElementAt(0).Count;

            if (rowCount != columnCount) { throw new ArgumentException("Must be a square matrix!"); }

            if (rowCount == 1) { throw new ArgumentException("Matrix has only one row and column!"); }

            List<List<decimal>> minorMatrix = new();

            for (int i = 0; i < rowCount; i++)
            {
                if (i != row)
                {
                    List<decimal> minorRow = new();
                    for (int j = 0; j < columnCount; j++)
                    {
                        if (j != column) minorRow.Add(matrix.ElementAt(i).ElementAt(j));
                    }

                    minorMatrix.Add(minorRow);
                }
            }

            return minorMatrix;
        }

        public static void PrintMatrix(ref List<List<decimal>> matrix)
        {
            int currentRow = 1;
            foreach (List<decimal> row in matrix)
            {
                string rowText = "Row " + currentRow + ":   ";
                foreach (decimal item in row)
                {
                    rowText += item + ",  ";
                }
                Console.WriteLine(rowText);
                currentRow++;
            }
        }
    }
}
