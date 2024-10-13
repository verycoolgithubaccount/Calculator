using System.Numerics;

internal class Program
{
    private static void Main(string[] args)
    {
        var matrix = CalculatorLibrary.IO.PromptForMatrix("Enter matrix: ");

        CalculatorLibrary.Matrix.PrintMatrix(ref matrix);

        Console.WriteLine(CalculatorLibrary.Matrix.GetDeterminant(ref matrix));
    }
}