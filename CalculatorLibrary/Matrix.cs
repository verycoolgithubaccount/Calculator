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

            if (rowCount == 1) { return matrix; }

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

        public static void PrintStringMatrix(ref List<List<string>> matrix)
        {
            int currentRow = 1;
            foreach (List<string> row in matrix)
            {
                string rowText = "Row " + currentRow + ":   ";
                foreach (string item in row)
                {
                    rowText += item + ",  ";
                }
                Console.WriteLine(rowText);
                currentRow++;
            }
        }

        public static List<List<decimal>> ScalarMultiply(ref List<List<decimal>> matrix, decimal scalar)
        {
            List<List<decimal>> output = new();

            foreach (List<decimal> row in matrix)
            {
                List<decimal> outputRow = new();
                foreach (decimal item in row)
                {
                    outputRow.Add(item * scalar);
                }
                output.Add(outputRow);
            }
            return output;
        }

        public static List<List<decimal>> AddMatrix(ref List<List<decimal>> matrix1, ref List<List<decimal>> matrix2, bool subtract = false)
        {
            int rowCount = matrix1.Count;
            int columnCount = matrix1.ElementAt(0).Count;

            if (rowCount != matrix2.Count || columnCount != matrix2.ElementAt(0).Count) { throw new ArgumentException("Matrices must have equal dimensions!"); }

            List<List<decimal>> output = new();

            for (int i = 0; i < rowCount; i++)
            {
                List<decimal> outputRow = new();
                for (int j = 0; j < columnCount; j++)
                {
                    if (subtract)
                    {
                        outputRow.Add(matrix1.ElementAt(i).ElementAt(j) - matrix2.ElementAt(i).ElementAt(j));
                    } 
                    else outputRow.Add(matrix1.ElementAt(i).ElementAt(j) + matrix2.ElementAt(i).ElementAt(j));
                }
                output.Add(outputRow);
            }
            return output;
        }

        public static List<List<decimal>> Transpose(ref List<List<decimal>> matrix)
        {
            int rowCount = matrix.Count;
            int columnCount = matrix.ElementAt(0).Count;

            List<List<decimal>> output = new();

            for (int i = 0; i < columnCount; i++)
            {
                List<decimal> outputRow = new();
                foreach (List<decimal> row in matrix)
                {
                    outputRow.Add(row.ElementAt(i));
                }
                output.Add(outputRow);
            }
            return output;
        }

        public static List<List<decimal>> MultiplyMatrix(ref List<List<decimal>> matrix1, ref List<List<decimal>> matrix2)
        {
            int rowCount1 = matrix1.Count;
            int columnCount1 = matrix1.ElementAt(0).Count;

            int rowCount2 = matrix2.Count;
            int columnCount2 = matrix2.ElementAt(0).Count;

            if (columnCount1 != rowCount2) { throw new ArgumentException("Matrix 1's columns and Matrix 2's columns must be equal!"); }

            List<List<decimal>> output = new();

            for (int i = 0; i < rowCount1; i++)
            {
                List<decimal> outputRow = new();
                for (int j = 0; j < columnCount2; j++)
                {
                    decimal item = 0;
                    for (int k = 0; k < columnCount1; k++)
                    {
                        item += (matrix1.ElementAt(i).ElementAt(k) * matrix2.ElementAt(k).ElementAt(j));
                    }
                    outputRow.Add(item);
                }
                output.Add(outputRow);
            }
            return output;
        }

        public static List<List<decimal>> MatrixOfCofactors(ref List<List<decimal>> matrix)
        {
            int rowCount = matrix.Count;
            int columnCount = matrix.ElementAt(0).Count;

            if (rowCount != columnCount) { throw new ArgumentException("Must be a square matrix!"); }

            List<List<decimal>> output = new();

            for (int i = 0; i < rowCount; i++)
            {
                List<decimal> outputRow = new();
                for (int j = 0; j < columnCount; j++)
                {
                    List<List<decimal>> minor = GetMinor(ref matrix, i, j);
                    outputRow.Add((decimal) Math.Pow(-1, (i + j + 2)) * GetDeterminant(ref minor));
                }
                output.Add(outputRow);
            }
            return output;
        }

        public static List<List<decimal>> Inverse(ref List<List<decimal>> matrix)
        {
            int rowCount = matrix.Count;
            int columnCount = matrix.ElementAt(0).Count;

            if (rowCount != columnCount) { throw new ArgumentException("Must be a square matrix!"); }

            decimal determinant = GetDeterminant(ref matrix);

            if (determinant == 0) { throw new ArgumentException("Determinant is 0!"); }

            List<List<decimal>> output = new();

            output = MatrixOfCofactors(ref matrix);
            output = Transpose(ref output);
            output = ScalarMultiply(ref output, (1 / determinant));

            return output;
        }

        public static List<List<decimal>> Cramer(ref List<List<decimal>> matrix, ref List<List<decimal>> solutions)
        {
            int rowCountM = matrix.Count;
            int columnCountM = matrix.ElementAt(0).Count;

            int rowCountS = solutions.Count;
            int columnCountS = solutions.ElementAt(0).Count;

            if (rowCountM != columnCountM) { throw new ArgumentException("Must be a square matrix!"); }
            if (rowCountM != rowCountS) { throw new ArgumentException("Matrices must have same number of rows!"); }
            if (columnCountS != 1) { throw new ArgumentException("Solution matrix must have only one column!"); }

            decimal determinant = GetDeterminant(ref matrix);

            if (determinant == 0) { throw new ArgumentException("Determinant is 0!"); }

            List<List<decimal>> output = new();

            for (int i = 0; i < columnCountM; i++)
            {
                List<decimal> row = new();
                List<List<decimal>> tempMatrix = new();

                foreach(List<decimal> copyingRow in matrix)
                {
                    List<decimal> copiedRow = new();
                    foreach(decimal copyingColumn in copyingRow) copiedRow.Add(copyingColumn);
                    tempMatrix.Add(copiedRow);
                }

                for (int j = 0; j < rowCountM; j++)
                {
                    tempMatrix.ElementAt(j)[i] = solutions.ElementAt(j).ElementAt(0);
                }

                decimal dx = GetDeterminant(ref tempMatrix);

                row.Add(dx / determinant);
                output.Add(row);
            }

            return output;
        }

        public static List<List<string>> Compare(ref List<List<decimal>> matrix1, ref List<List<string>> matrix2)
        {
            if (matrix1.Count != matrix2.Count || matrix1.ElementAt(0).Count != matrix2.ElementAt(0).Count) throw new ArgumentException("Matrices must have the same dimensions!");

            List<List<string>> output = new();

            for (int i = 0; i < matrix1.Count; i++)
            {
                List<string> row = new();
                for (int j = 0; j < matrix1.ElementAt(0).Count; j++)
                {
                    string[] numAndDom = matrix2.ElementAt(i)[j].Split('/');
                    decimal num = decimal.Parse(numAndDom[0]);
                    decimal dom = (numAndDom.Length > 1) ? decimal.Parse(numAndDom[1]) : 1;

                    if (num / dom == matrix1.ElementAt(i)[j]) row.Add("_");
                    else row.Add("X");
                }
                output.Add(row);
            }
            return output;
        }
    }
}
