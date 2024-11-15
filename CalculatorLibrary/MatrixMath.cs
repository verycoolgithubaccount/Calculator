using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary
{
    public class MatrixMath
    {
        public static MathValue GetDeterminant(ref List<List<MathValue>> matrix)
        {
            int rowCount = matrix.Count;
            int columnCount = matrix.ElementAt(0).Count;

            if (rowCount != columnCount) { throw new ArgumentException("Must be a square matrix!"); }

            if (rowCount == 1) return matrix.ElementAt(0).ElementAt(0);

            MathValue determinant = new MathValue(0);

            for (int i = 0; i < columnCount; i++)
            {
                List<List<MathValue>> minor = GetMinor(ref matrix, 0, i);
                determinant += ((decimal)Math.Pow(-1, (i + 2d)) * matrix.ElementAt(0).ElementAt(i) * GetDeterminant(ref minor));
            }

            return determinant;
        }


        public static List<List<MathValue>> GetMinor(ref List<List<MathValue>> matrix, int row, int column)
        {
            int rowCount = matrix.Count;
            int columnCount = matrix.ElementAt(0).Count;

            if (rowCount != columnCount) { throw new ArgumentException("Must be a square matrix!"); }

            if (rowCount == 1) { return matrix; }

            List<List<MathValue>> minorMatrix = new();

            for (int i = 0; i < rowCount; i++)
            {
                if (i != row)
                {
                    List<MathValue> minorRow = new();
                    for (int j = 0; j < columnCount; j++)
                    {
                        if (j != column) minorRow.Add(matrix.ElementAt(i).ElementAt(j));
                    }

                    minorMatrix.Add(minorRow);
                }
            }

            return minorMatrix;
        }

        public static void PrintMatrix<T>(ref List<List<T>> matrix)
        {
            int currentRow = 1;
            foreach (List<T> row in matrix)
            {
                string rowText = "Row " + currentRow + ":   ";
                foreach (T item in row)
                {
                    rowText += item + ",  ";
                }
                Console.WriteLine(rowText);
                currentRow++;
            }
        }

        public static void PrintMatrixDecimal(ref List<List<MathValue>> matrix)
        {
            int currentRow = 1;
            foreach (List<MathValue> row in matrix)
            {
                string rowText = "Row " + currentRow + ":   ";
                foreach (MathValue item in row)
                {
                    rowText += item.ToStringDecimal() + ",  ";
                }
                Console.WriteLine(rowText);
                currentRow++;
            }
        }

        public static List<List<MathValue>> ScalarMultiply(ref List<List<MathValue>> matrix, MathValue scalar)
        {
            List<List<MathValue>> output = new();

            foreach (List<MathValue> row in matrix)
            {
                List<MathValue> outputRow = new();
                foreach (MathValue item in row)
                {
                    outputRow.Add(item * scalar);
                }
                output.Add(outputRow);
            }
            return output;
        }

        public static List<List<MathValue>> AddMatrix(ref List<List<MathValue>> matrix1, ref List<List<MathValue>> matrix2, bool subtract = false)
        {
            int rowCount = matrix1.Count;
            int columnCount = matrix1.ElementAt(0).Count;

            if (rowCount != matrix2.Count || columnCount != matrix2.ElementAt(0).Count) { throw new ArgumentException("Matrices must have equal dimensions!"); }

            List<List<MathValue>> output = new();

            for (int i = 0; i < rowCount; i++)
            {
                List<MathValue> outputRow = new();
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

        public static List<List<T>> Transpose<T>(ref List<List<T>> matrix)
        {
            int rowCount = matrix.Count;
            int columnCount = matrix.ElementAt(0).Count;

            List<List<T>> output = new();

            for (int i = 0; i < columnCount; i++)
            {
                List<T> outputRow = new();
                foreach (List<T> row in matrix)
                {
                    outputRow.Add(row.ElementAt(i));
                }
                output.Add(outputRow);
            }
            return output;
        }

        public static List<List<MathValue>> MultiplyMatrix(ref List<List<MathValue>> matrix1, ref List<List<MathValue>> matrix2)
        {
            int rowCount1 = matrix1.Count;
            int columnCount1 = matrix1.ElementAt(0).Count;

            int rowCount2 = matrix2.Count;
            int columnCount2 = matrix2.ElementAt(0).Count;

            if (columnCount1 != rowCount2) { throw new ArgumentException("Matrix 1's columns and Matrix 2's columns must be equal!"); }

            List<List<MathValue>> output = new();

            for (int i = 0; i < rowCount1; i++)
            {
                List<MathValue> outputRow = new();
                for (int j = 0; j < columnCount2; j++)
                {
                    MathValue item = new MathValue(0);
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

        public static List<List<MathValue>> MatrixOfCofactors(ref List<List<MathValue>> matrix)
        {
            int rowCount = matrix.Count;
            int columnCount = matrix.ElementAt(0).Count;

            if (rowCount != columnCount) { throw new ArgumentException("Must be a square matrix!"); }

            List<List<MathValue>> output = new();

            for (int i = 0; i < rowCount; i++)
            {
                List<MathValue> outputRow = new();
                for (int j = 0; j < columnCount; j++)
                {
                    List<List<MathValue>> minor = GetMinor(ref matrix, i, j);
                    outputRow.Add((decimal)Math.Pow(-1, (i + j + 2)) * GetDeterminant(ref minor));
                }
                output.Add(outputRow);
            }
            return output;
        }

        public static List<List<MathValue>> Inverse(ref List<List<MathValue>> matrix)
        {
            int rowCount = matrix.Count;
            int columnCount = matrix.ElementAt(0).Count;

            if (rowCount != columnCount) { throw new ArgumentException("Must be a square matrix!"); }

            MathValue determinant = GetDeterminant(ref matrix);

            if (determinant == 0) { throw new ArgumentException("Determinant is 0!"); }

            List<List<MathValue>> output = new();

            output = MatrixOfCofactors(ref matrix);
            output = Transpose(ref output);
            output = ScalarMultiply(ref output, (1 / determinant));

            return output;
        }

        public static List<List<MathValue>> Cramer(ref List<List<MathValue>> matrix, ref List<List<MathValue>> solutions)
        {
            int rowCountM = matrix.Count;
            int columnCountM = matrix.ElementAt(0).Count;

            int rowCountS = solutions.Count;
            int columnCountS = solutions.ElementAt(0).Count;

            if (rowCountM != columnCountM) { throw new ArgumentException("Must be a square matrix!"); }
            if (rowCountM != rowCountS) { throw new ArgumentException("Matrices must have same number of rows!"); }
            if (columnCountS != 1) { throw new ArgumentException("Solution matrix must have only one column!"); }

            MathValue determinant = GetDeterminant(ref matrix);

            if (determinant == 0) { throw new ArgumentException("Determinant is 0!"); }

            List<List<MathValue>> output = new();

            for (int i = 0; i < columnCountM; i++)
            {
                List<MathValue> row = new();
                List<List<MathValue>> tempMatrix = new();

                foreach (List<MathValue> copyingRow in matrix)
                {
                    List<MathValue> copiedRow = new();
                    foreach (MathValue copyingColumn in copyingRow) copiedRow.Add(copyingColumn);
                    tempMatrix.Add(copiedRow);
                }

                for (int j = 0; j < rowCountM; j++)
                {
                    tempMatrix.ElementAt(j)[i] = solutions.ElementAt(j).ElementAt(0);
                }

                MathValue dx = GetDeterminant(ref tempMatrix);

                row.Add(dx / determinant);
                output.Add(row);
            }

            return output;
        }
    }
}
