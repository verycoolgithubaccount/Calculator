using CalculatorLibrary;
using System.Numerics;

internal class Program
{
    private static List<List<decimal>>[] savedMatrices = new List<List<decimal>>[10];

    private static void Main(string[] args)
    {
        List<List<decimal>> outputMatrix = new();
        do
        {
            try
            {
                Console.Clear();
                IEnumerable<string> en = [
                    "Get the determinant of a matrix",
                    "Transpose a matrix",
                    "Multiply a matrix by a scalar", 
                    "Add two matrices", 
                    "Multiply two matrices",
                    "Get a matrix's cofactors",
                    "Get a matrix's inverse", 
                    "Cramer's rule", 
                    "Solve by inverse", 
                    "Compare previous matrix"];
                int menuselection = IO.PromptForMenuSelection(en, true);

                Console.Clear();
                switch (menuselection)
                {
                    case 0: return;
                    case 1:
                        {
                            List<List<decimal>> matrix = UseSavedMatrix(null, true);
                            if (matrix == null)
                            {
                                int size = IO.PromptForInt("What is the size of the matrix? ", 0, int.MaxValue);
                                matrix = IO.PromptForMatrix("Enter the matrix: ", size, size);
                            }

                            Console.WriteLine("Input matrix: ");
                            Console.WriteLine();
                            Matrix.PrintMatrix(ref matrix);
                            Console.WriteLine();
                            Console.WriteLine("Determinant: " + Matrix.GetDeterminant(ref matrix));
                            Console.WriteLine();
                            Console.WriteLine("Press enter to continue!");
                            Console.ReadLine();

                            break;
                        }
                    case 2:
                        {
                            List<List<decimal>> matrix = UseSavedMatrix();
                            if (matrix == null)
                            {
                                matrix = IO.PromptForMatrix("Enter the matrix: ");
                            }

                            Console.WriteLine("Input matrix: ");
                            Console.WriteLine();
                            Matrix.PrintMatrix(ref matrix);
                            Console.WriteLine();
                            Console.WriteLine("Output matrix: ");
                            Console.WriteLine();

                            outputMatrix = Matrix.Transpose(ref matrix);

                            Matrix.PrintMatrix(ref outputMatrix);
                            Console.WriteLine();
                            DoneWithOperation(ref outputMatrix);
                            Console.Clear();

                            break;
                        }
                    case 3:
                        {
                            List<List<decimal>> matrix = UseSavedMatrix();
                            if (matrix == null)
                            {
                                matrix = IO.PromptForMatrix("Enter the matrix: ");
                            }
                            decimal scalar = IO.PromptForDecimal("Enter the scalar: ", decimal.MinValue, decimal.MaxValue);

                            Console.WriteLine("Input matrix: ");
                            Console.WriteLine();
                            Matrix.PrintMatrix(ref matrix);
                            Console.WriteLine();
                            Console.WriteLine("Output matrix: ");
                            Console.WriteLine();

                            outputMatrix = Matrix.ScalarMultiply(ref matrix, scalar);

                            Matrix.PrintMatrix(ref outputMatrix);
                            Console.WriteLine();
                            DoneWithOperation(ref outputMatrix);
                            Console.Clear();

                            break;
                        }
                    case 4:
                        {
                            List<List<decimal>> matrix1 = UseSavedMatrix("What would you like to do for the first matrix?");
                            List<List<decimal>> matrix2;
                            int rowCount;
                            int columnCount;
                            if (matrix1 == null)
                            {
                                rowCount = IO.PromptForInt("How many rows do the matrices have? ", 0, int.MaxValue);
                                columnCount = IO.PromptForInt("How many columns? ", 0, int.MaxValue);

                                matrix1 = IO.PromptForMatrix("Enter the first matrix: ", rowCount, columnCount);
                                matrix2 = UseSavedMatrix("What would you like to do for the second matrix?", false, rowCount, columnCount);
                                if (matrix2 == null)
                                {
                                    matrix2 = IO.PromptForMatrix("Enter the second matrix: ", rowCount, columnCount);
                                }
                            }
                            else
                            {
                                matrix2 = UseSavedMatrix("What would you like to do for the second matrix?", false, matrix1.Count, matrix1[0].Count);
                                if (matrix2 == null)
                                {
                                    matrix2 = IO.PromptForMatrix("Enter the second matrix: ", matrix1.Count, matrix1[0].Count);
                                }
                            }

                            bool add = IO.PromptForBool("Add or subtract? ", "add", "subtract");

                            Console.WriteLine("Matrix 1: ");
                            Console.WriteLine();
                            Matrix.PrintMatrix(ref matrix1);
                            Console.WriteLine();
                            Console.WriteLine("Matrix 2: ");
                            Console.WriteLine();
                            Matrix.PrintMatrix(ref matrix2);
                            Console.WriteLine();
                            Console.WriteLine("Output matrix: ");
                            Console.WriteLine();

                            outputMatrix = Matrix.AddMatrix(ref matrix1, ref matrix2, !add);

                            Matrix.PrintMatrix(ref outputMatrix);
                            Console.WriteLine();
                            DoneWithOperation(ref outputMatrix);
                            Console.Clear();

                            break;
                        }
                    case 5:
                        {
                            List<List<decimal>> matrix1 = UseSavedMatrix("What would you like to do for the first matrix?");
                            List<List<decimal>> matrix2;
                            int rowCount;
                            int columnCount1;
                            int columnCount2;
                            if (matrix1 == null)
                            {
                                rowCount = IO.PromptForInt("How many rows does the first matrix have? ", 0, int.MaxValue);
                                columnCount1 = IO.PromptForInt("How many columns? ", 0, int.MaxValue);

                                matrix1 = IO.PromptForMatrix("Enter the first matrix: ", rowCount, columnCount1);
                                matrix2 = UseSavedMatrix("What would you like to do for the second matrix?", false, columnCount1);
                                if (matrix2 == null)
                                {
                                    columnCount2 = IO.PromptForInt("How many columns does the second matrix have? ", 0, int.MaxValue);
                                    matrix2 = IO.PromptForMatrix("Enter the second matrix: ", columnCount1, columnCount2);
                                }
                            }
                            else
                            {
                                matrix2 = UseSavedMatrix("What would you like to do for the second matrix?", false, matrix1[0].Count);
                                if (matrix2 == null)
                                {
                                    columnCount2 = IO.PromptForInt("How many columns does the second matrix have? ", 0, int.MaxValue);
                                    matrix2 = IO.PromptForMatrix("Enter the second matrix: ", matrix1[0].Count, columnCount2);
                                }
                            }

                            Console.WriteLine("Matrix 1: ");
                            Console.WriteLine();
                            Matrix.PrintMatrix(ref matrix1);
                            Console.WriteLine();
                            Console.WriteLine("Matrix 2: ");
                            Console.WriteLine();
                            Matrix.PrintMatrix(ref matrix2);
                            Console.WriteLine();
                            Console.WriteLine("Output matrix: ");
                            Console.WriteLine();

                            outputMatrix = Matrix.MultiplyMatrix(ref matrix1, ref matrix2);

                            Matrix.PrintMatrix(ref outputMatrix);
                            Console.WriteLine();
                            DoneWithOperation(ref outputMatrix);
                            Console.Clear();

                            break;
                        }
                    case 6:
                        {
                            List<List<decimal>> matrix = UseSavedMatrix(null, true);
                            if (matrix == null)
                            {
                                int size = IO.PromptForInt("What is the size of the matrix? ", 0, int.MaxValue);
                                matrix = IO.PromptForMatrix("Enter the matrix: ", size, size);
                            }

                            Console.WriteLine("Input matrix: ");
                            Console.WriteLine();
                            Matrix.PrintMatrix(ref matrix);
                            Console.WriteLine();
                            Console.WriteLine("Determinant: " + Matrix.GetDeterminant(ref matrix));
                            Console.WriteLine();
                            Console.WriteLine("Matrix of Cofactors: ");
                            Console.WriteLine();

                            outputMatrix = Matrix.MatrixOfCofactors(ref matrix);

                            Matrix.PrintMatrix(ref outputMatrix);
                            Console.WriteLine();
                            DoneWithOperation(ref outputMatrix);
                            Console.Clear();

                            break;
                        }
                    case 7:
                        {
                            List<List<decimal>> matrix = UseSavedMatrix(null, true);
                            if (matrix == null)
                            {
                                int size = IO.PromptForInt("What is the size of the matrix? ", 0, int.MaxValue);
                                matrix = IO.PromptForMatrix("Enter the matrix: ", size, size);
                            }

                            Console.WriteLine("Input matrix: ");
                            Console.WriteLine();
                            Matrix.PrintMatrix(ref matrix);
                            Console.WriteLine();
                            Console.WriteLine("Determinant: " + Matrix.GetDeterminant(ref matrix));
                            Console.WriteLine();
                            Console.WriteLine("Matrix of Cofactors: ");
                            Console.WriteLine();

                            var cof = Matrix.MatrixOfCofactors(ref matrix);

                            Matrix.PrintMatrix(ref cof);
                            Console.WriteLine();
                            Console.WriteLine("Inverse: ");
                            Console.WriteLine();

                            outputMatrix = Matrix.Inverse(ref matrix);

                            Matrix.PrintMatrix(ref outputMatrix);
                            Console.WriteLine();
                            DoneWithOperation(ref outputMatrix);
                            Console.Clear();

                            break;
                        }
                    case 8:
                        {
                            int size = IO.PromptForInt("How many variables are there? ", 0, int.MaxValue);

                            var matrix = IO.PromptForMatrix("Enter the matrix: ", size, size);
                            var solutions = IO.PromptForMatrix("Enter the solutions: ", size, 1);

                            Console.WriteLine("Input matrix: ");
                            Console.WriteLine();
                            Matrix.PrintMatrix(ref matrix);
                            Console.WriteLine();
                            Console.WriteLine("Solution matrix: ");
                            Console.WriteLine();
                            Matrix.PrintMatrix(ref solutions);
                            Console.WriteLine();
                            Console.WriteLine("Variable matrix: ");
                            Console.WriteLine();

                            outputMatrix = Matrix.Cramer(ref matrix, ref solutions);

                            Matrix.PrintMatrix(ref outputMatrix);
                            Console.WriteLine();
                            DoneWithOperation(ref outputMatrix);
                            Console.Clear();

                            break;
                        }
                    case 9:
                        {
                            int size = IO.PromptForInt("How many variables are there? ", 0, int.MaxValue);

                            var matrix = IO.PromptForMatrix("Enter the matrix: ", size, size);
                            var solutions = IO.PromptForMatrix("Enter the solutions: ", size, 1);

                            Console.WriteLine("Input matrix: ");
                            Console.WriteLine();
                            Matrix.PrintMatrix(ref matrix);
                            Console.WriteLine();
                            Console.WriteLine("Determinant: " + Matrix.GetDeterminant(ref matrix));
                            Console.WriteLine();
                            Console.WriteLine("Matrix of Cofactors: ");
                            Console.WriteLine();

                            var cof = Matrix.MatrixOfCofactors(ref matrix);

                            Matrix.PrintMatrix(ref cof);
                            Console.WriteLine();
                            Console.WriteLine("Inverse: ");
                            Console.WriteLine();

                            var inverseMatrix = Matrix.Inverse(ref matrix);

                            Matrix.PrintMatrix(ref inverseMatrix);
                            Console.WriteLine();
                            Console.WriteLine("Solution matrix: ");
                            Console.WriteLine();
                            Matrix.PrintMatrix(ref solutions);
                            Console.WriteLine();
                            Console.WriteLine("Variable matrix: ");
                            Console.WriteLine();

                            outputMatrix = Matrix.MultiplyMatrix(ref inverseMatrix, ref solutions);

                            Matrix.PrintMatrix(ref outputMatrix);
                            Console.WriteLine();
                            DoneWithOperation(ref outputMatrix);
                            Console.Clear();

                            break;
                        }
                    case 10:
                        {
                            if (outputMatrix.Count < 1) break;
                            var matrix = IO.PromptForStringMatrix("Enter the matrix you want to compare: ", outputMatrix.Count, outputMatrix.ElementAt(0).Count);

                            Console.WriteLine("Original matrix: ");
                            Console.WriteLine();
                            Matrix.PrintMatrix(ref outputMatrix);
                            Console.WriteLine();
                            Console.WriteLine("Input matrix: ");
                            Console.WriteLine();
                            Matrix.PrintStringMatrix(ref matrix);
                            Console.WriteLine();
                            Console.WriteLine("Comparison: ");
                            Console.WriteLine();

                            var compare = Matrix.Compare(ref outputMatrix, ref matrix);

                            Matrix.PrintStringMatrix(ref compare);
                            Console.WriteLine();
                            DoneWithOperation(ref outputMatrix);
                            Console.Clear();

                            break;
                        }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        } while (true);
    }

    private static void DoneWithOperation(ref List<List<decimal>> outputMatrix)
    {
        IEnumerable<string> en = [
                    "Continue",
                    "Save output matrix"
        ];
        if (IO.PromptForMenuSelection(en, false) == 1) return;

        Console.Clear();

        IEnumerable<string> en2 = [
            "Cancel",
            "Slot 1 - " + ((savedMatrices[0] == null || savedMatrices[0].Count == 0) ? "Empty" : "Full"),
            "Slot 2 - " + ((savedMatrices[1] == null || savedMatrices[1].Count == 0) ? "Empty" : "Full"),
            "Slot 3 - " + ((savedMatrices[2] == null || savedMatrices[2].Count == 0) ? "Empty" : "Full"),
            "Slot 4 - " + ((savedMatrices[3] == null || savedMatrices[3].Count == 0) ? "Empty" : "Full"),
            "Slot 5 - " + ((savedMatrices[4] == null || savedMatrices[4].Count == 0) ? "Empty" : "Full"),
            "Slot 6 - " + ((savedMatrices[5] == null || savedMatrices[5].Count == 0) ? "Empty" : "Full"),
            "Slot 7 - " + ((savedMatrices[6] == null || savedMatrices[6].Count == 0) ? "Empty" : "Full"),
            "Slot 8 - " + ((savedMatrices[7] == null || savedMatrices[7].Count == 0) ? "Empty" : "Full"),
            "Slot 9 - " + ((savedMatrices[8] == null || savedMatrices[8].Count == 0) ? "Empty" : "Full"),
            "Slot 10 - " + ((savedMatrices[9] == null || savedMatrices[9].Count == 0) ? "Empty" : "Full")
            ];

        int selectedItem = IO.PromptForMenuSelection(en2, false, "Which save slot?");

        if (selectedItem == 1) return;
        else savedMatrices[selectedItem - 2] = outputMatrix;
    }


    private static List<List<decimal>> UseSavedMatrix(string prompt = null, bool mustBeSquare = false, int numRows = -1, int numColumns = -1)
    {
        List<int> validMatrices = new List<int>();
        for (int i = 0; i < savedMatrices.Length; i++)
        {
            if (savedMatrices[i] == null || savedMatrices[i].Count == 0) continue;
            if (mustBeSquare && savedMatrices[i].Count != savedMatrices[i][0].Count) continue;
            if ((numRows == -1 || savedMatrices[i].Count == numRows) && (numColumns == -1 || savedMatrices[i][0].Count == numColumns)) validMatrices.Add(i);
        }

        if (validMatrices.Count() == 0) return null;

        IEnumerable<string> useSaved = [
                    "Enter new matrix",
                    "Use a saved matrix"
        ];

        if (IO.PromptForMenuSelection(useSaved, false, prompt) == 1)
        {
            Console.Clear();
            return null;
        }

        IEnumerable<string> pickMatrix = [
                    "Cancel",
        ];

        foreach (int i in validMatrices) pickMatrix = pickMatrix.Append("Save " + (i + 1));

        int chosenValue = IO.PromptForMenuSelection(pickMatrix, false);
        if (chosenValue == 1) return null;
        Console.Clear();
        return savedMatrices[chosenValue - 2];
    }
}