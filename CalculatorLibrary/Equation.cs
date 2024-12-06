using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary
{
    public enum OperatorType
    {
        PLUS,
        MINUS,
        DIVIDE,
        MULTIPLY,
        OPEN_PARENTHESIS,
        CLOSE_PARENTHESIS,
        POWER
    }

    public struct Operator : MathElement
    {
        public OperatorType type;
    }

    public class Equation : MathElement
    {
        public List<MathElement> equation = new();
    }
}
