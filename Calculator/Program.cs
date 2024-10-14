using System.Numerics;

internal class Program
{
    private static void Main(string[] args)
    {
        var matrix = CalculatorLibrary.IO.PromptForMatrix("Enter matrix: ");

        var result = CalculatorLibrary.Matrix.Inverse(ref matrix);

        CalculatorLibrary.Matrix.PrintMatrix(ref result);

        Console.WriteLine();
    }
}