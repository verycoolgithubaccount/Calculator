using System.Numerics;

internal class Program
{
    private static void Main(string[] args)
    {
        var matrix1 = CalculatorLibrary.IO.PromptForMatrix("Enter matrix: ");
        var matrix2 = CalculatorLibrary.IO.PromptForMatrix("Enter matrix: ");

        var sum = CalculatorLibrary.Matrix.AddMatrix(ref matrix1, ref matrix2);

        CalculatorLibrary.Matrix.PrintMatrix(ref sum);
        Console.WriteLine();
    }
}