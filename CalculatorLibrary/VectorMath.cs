using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary
{
    public class VectorMath
    {
        public static MathValue GetMagnitude(ref List<List<MathValue>> vector)
        {
            MathValue result = new MathValue(0);
            foreach (var item in vector)
            {
                result += item[0] * item[0];
            }
            result.SquareRoot();

            return result;
        }

        public static List<List<MathValue>> GetUnitVector(ref List<List<MathValue>> vector)
        {
            MathValue magnitude = GetMagnitude(ref vector);
            List<List<MathValue>> output = new();
            for (int i = 0; i < vector.Count; i++)
            {
                output.Add(new List<MathValue>());
                output[i].Add(vector[i][0] / magnitude);
            }

            return output;
        }

        public static MathValue DotProduct(ref List<List<MathValue>> v1, ref List<List<MathValue>> v2)
        {
            if (v2.Count != v1.Count) throw new ArgumentException("Vectors are not the same size!");
            
            MathValue output = new MathValue(0);
            for (int i = 0; i < v1.Count; i++)
            {
                output += (v1[i][0] * v1[i][0]);
            }

            return output;
        }

        public static MathValue GetAngleBetweenVectors(ref List<List<MathValue>> v1, ref List<List<MathValue>> v2)
        {
            if (v2.Count != v1.Count) throw new ArgumentException("Vectors are not the same size!");

            MathValue output;
            MathValue dot = DotProduct(ref v1, ref v2);
            
            output = dot / (GetMagnitude(ref v1) * GetMagnitude(ref v2));

            output.Arccos();

            return output;
        }
    }
}
