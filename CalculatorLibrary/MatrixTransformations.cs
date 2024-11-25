using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary
{
    public class MatrixTransformations
    {
        public static List<List<MathValue>> AddDimensions(ref List<List<MathValue>> matrix, int dimensions)
        {
            List<List<MathValue>> output = new();
            foreach (var row in matrix)
            {
                output.Add(new List<MathValue>(row));
            }

            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < output.Count; j++)
                {
                    output[j].Add(new MathValue(0));
                }
                List<MathValue> newRow = new List<MathValue>();
                for (int j = 0; j < output[0].Count - 1; j++) newRow.Add(new MathValue(0));

                newRow.Add(new MathValue(1));

                output.Add(newRow);
            }

            return output;
        }
    }
}
