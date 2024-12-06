using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary
{
    public interface MathElement
    {
    }

    public struct Variable
    {
        public Variable(char variable, decimal power = 1)
        {
            this.variable = variable;
            this.power = power;
        }

        public char variable;
        public decimal power;

        public static bool operator == (Variable a, Variable b)
        {
            return (a.variable == b.variable && a.power == b.power);
        }

        public static bool operator !=(Variable a, Variable b)
        {
            return (a.variable != b.variable || a.power != b.power);
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj == null) return false;
            return this == (Variable) obj;
        }

        public override string ToString()
        {
            if (power == 0) return "1";
            return variable.ToString() + ((power == 1) ? "" : "^" + power);
        }
        public string ToStringAbs()
        {
            if (power == 0) return "1";
            return variable.ToString() + ((Math.Abs(power) == 1) ? "" : "^" + Math.Abs(power));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public struct MathValue : MathElement
    {
        public MathValue(decimal numerator = 0, decimal denominator = 1, List<Variable> variable = null)
        {
            if (denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }

            this.numerator = numerator;
            Numerator = numerator;
            this.denominator = denominator;
            Denominator = denominator;

            List<Variable> copiedVariables = new();

            if (variable != null)
            {
                foreach (Variable v in variable) copiedVariables.Add(v);
            }

            this.variable = copiedVariables;
            Variables = copiedVariables;
            Reduce();
        }

        private decimal numerator;
        private decimal denominator;
        private List<Variable> variable;

        public decimal Numerator
        {
            get { return numerator; }
            set { numerator = value; }
        }

        public decimal Denominator
        {
            get { return denominator; }
            set
            {
                if (value == 0) throw new ArgumentException("Denominator cannot be 0!");
                denominator = value;
            }
        }
        public List<Variable> Variables
        {
            get { return variable; }
            set
            {
                List<Variable> copiedVariables = new();

                if (value != null)
                {
                    foreach (Variable v in value) copiedVariables.Add(v);
                }

                variable = copiedVariables;
            }
        }

        public static decimal GCD(decimal m, decimal n)
        {
            if (m == 0) return Math.Abs(n);
            return GCD(n % m, m);
        }

        public static MathValue Reciprocal(MathValue input)
        {
            if (input.numerator == 0) return new MathValue(0);

            List<Variable> combinedVariable = new();
            foreach (Variable v in input.variable)
            {
                combinedVariable.Add(new Variable(v.variable, -v.power));
            }

            return new MathValue(input.denominator, input.numerator, combinedVariable);
        }

        public static MathValue operator + (MathValue a, MathValue b)
        {
            MathValue value;

            if (a.Denominator == b.Denominator) value = new MathValue(a.Numerator + b.Numerator, a.Denominator, a.Variables);
            else value = new MathValue((a.Numerator * b.Denominator) + (b.Numerator * a.Denominator), a.Denominator * b.Denominator, a.Variables);

            value.Reduce();
            return value;
        }

        public static MathValue operator - (MathValue a, MathValue b)
        {
            MathValue value;

            if (a.Denominator == b.Denominator) value = new MathValue(a.Numerator - b.Numerator, a.Denominator, a.Variables);
            else value = new MathValue((a.Numerator * b.Denominator) - (b.Numerator * a.Denominator), a.Denominator * b.Denominator, a.Variables);

            value.Reduce();
            return value;
        }


        public static MathValue operator * (MathValue a, MathValue b)
        {
            List<Variable> combinedVariable = new();
            List<int> bAdded = new();

            foreach (Variable variable1 in a.variable)
            {
                bool found = false;
                for (int i = 0; i < a.variable.Count; i++)
                {
                    if (variable1.variable == b.variable[i].variable)
                    {
                        found = true;
                        bAdded.Add(i);
                        if (variable1.power + b.variable[i].power != 0)
                        {
                            combinedVariable.Add(new Variable(variable1.variable, variable1.power + b.variable[i].power));
                        }
                        break;
                    }
                }
                if (!found) combinedVariable.Add(variable1);
            }

            if (bAdded.Count < b.variable.Count)
            {
                for (int i = 0; i < b.variable.Count; i++)
                {
                    if (!bAdded.Contains(i)) combinedVariable.Add(b.variable[i]);
                }
            }

            MathValue value = new MathValue(a.Numerator * b.Numerator, a.Denominator * b.Denominator, combinedVariable);
            value.Reduce();
            return value;
        }

        public static MathValue operator * (decimal a, MathValue b)
        {
            MathValue value;

            if (a * b.Numerator != 0)
            {
                value = new MathValue(a * b.Numerator, b.Denominator, b.Variables);
            }
            else value = new MathValue(0);

            value.Reduce();
            return value;
        }

        public static MathValue operator *(MathValue a, decimal b)
        {
            MathValue value;

            if (b * a.Numerator != 0)
            {
                value = new MathValue(b * a.Numerator, a.Denominator, a.Variables);
            }
            else value = new MathValue(0);

            value.Reduce();
            return value;
        }

        public static MathValue operator / (MathValue a, MathValue b)
        {
            return a * Reciprocal(b);
        }

        public static MathValue operator / (decimal a, MathValue b)
        {
            MathValue value = new MathValue(a * b.Denominator, b.Numerator, b.Variables);
            value.Reduce();
            return value;
        }

        public static bool operator == (MathValue a, MathValue b)
        {
            if (a.variable.Count != b.variable.Count) return false;

            for (int i = 0; i < a.variable.Count; i++)
            {
                if (a.variable[i] != b.variable[i]) return false;
            }

            a.Reduce();
            b.Reduce();

            return (a.Numerator == b.Numerator && a.Denominator == b.Denominator);
        }

        public static bool operator ==(MathValue a, decimal b)
        {
            if (a.variable.Count > 0) return false;
            return ((a.Numerator / a.Denominator) == b);
        }

        public static bool operator != (MathValue a, MathValue b)
        {
            if (a.variable.Count != b.variable.Count) return true;

            for (int i = 0; i < a.variable.Count; i++)
            {
                if (a.variable[i] != b.variable[i]) return true;
            }

            a.Reduce();
            b.Reduce();

            return (a.Numerator != b.Numerator || a.Denominator != b.Denominator);
        }

        public static bool operator !=(MathValue a, decimal b)
        {
            if (a.variable.Count > 0) return true;
            return ((a.Numerator / a.Denominator) != b);
        }

        public void Reduce()
        {
            if (denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }
            decimal factor = GCD(Numerator, Denominator);
            if (factor != Denominator)
            {
                Numerator = Numerator / factor;
                Denominator = Denominator / factor;
            }
            if (Numerator == Denominator)
            {
                Numerator = 1;
                Denominator = 1;
            }
            if (Denominator > 1000000000000)
            {
                Numerator = Numerator / Denominator;
                Denominator = 1;
            }
            try
            {
                if ((int) (Numerator * 10000000000) == 0)
                {
                    Numerator = 0;
                    Denominator = 1;
                }
            }
            catch (Exception e) {}
        }

        public MathValue Power(this MathValue baseValue, double power)
        {
            if (power == 0) return new MathValue(1);
            double value = (double) baseValue.Numerator / (double)baseValue.Denominator;
            value = Math.Pow(value, power);

            List<Variable> copiedVariables = new();
            foreach (Variable v in baseValue.Variables) copiedVariables.Add(new Variable(v.variable, v.power * (decimal) power));

            MathValue returnValue = new MathValue((decimal) value, 1, copiedVariables);
            returnValue.Reduce();

            return returnValue;
        }

        public void SquareRoot()
        {
            Numerator = (decimal) Math.Sqrt((double) Numerator);
            Denominator = (decimal) Math.Sqrt((double) Denominator);

            List<Variable> copiedVariables = new();
            foreach (Variable v in variable) copiedVariables.Add(new Variable(v.variable, v.power / 2));
            Variables = copiedVariables;

            Reduce();
        }

        public void Sine()
        {
            Numerator = (decimal)Math.Sin((double)(Numerator / Denominator) * (Math.PI / 180));
            Denominator = 1;
            Reduce();
        }

        public void Arcsin()
        {
            Numerator = (decimal) (Math.Asin((double)(Numerator / Denominator)) * (180 / Math.PI));
            Denominator = 1;
            Reduce();
        }

        public void Cosine()
        {
            Numerator = (decimal) Math.Cos((double) (Numerator / Denominator) * (Math.PI / 180));
            Denominator = 1;
            Reduce();
        }

        public void Arccos()
        {
            Numerator = (decimal) (Math.Acos((double)(Numerator / Denominator)) * (180 / Math.PI));
            Denominator = 1;
            Reduce();
        }

        public void Tangent()
        {
            Numerator = (decimal)Math.Tan((double)(Numerator / Denominator) * (Math.PI / 180));
            Denominator = 1;
            Reduce();
        }

        public void Arctan()
        {
            Numerator = (decimal) (Math.Atan((double)(Numerator / Denominator)) * (180 / Math.PI));
            Denominator = 1;
            Reduce();
        }

        public override string ToString()
        {
            if (Numerator == 0) return "0";

            string numeratorVariables = "";
            string denominatorVariables = "";

            foreach(Variable v in variable)
            {
                if (v.power > 0) numeratorVariables = numeratorVariables + v.ToString();
                else denominatorVariables = denominatorVariables + v.ToStringAbs();
            }

            string numeratorString = (Numerator == 1 && numeratorVariables.Length > 0) ? numeratorVariables : Numerator + numeratorVariables;

            if (Denominator == 1 && denominatorVariables.Length == 0) return numeratorString;

            string denominatorString = (Denominator == 1 && numeratorVariables.Length > 0) ? numeratorVariables : Denominator + numeratorVariables;
            return numeratorString + "/" + denominatorString;
        }

        public string ToStringDecimal()
        {
            decimal value = (Numerator / Denominator);

            if (value == 0) return "0";

            if ((ulong)(Math.Abs(value) * 100000) - ((ulong)(Math.Abs(value) * 10000) * 10) > 4)
            {
                value = (value > 0) ? value + (decimal)0.0001 : value - (decimal)0.0001;
            }

            string untruncated = value.ToString();
            string[] outputStrings = untruncated.Split(".");

            if (outputStrings.Length == 1 ) return outputStrings[0];

            if (outputStrings[1].Length > 4) outputStrings[1] = outputStrings[1].Substring(0, 4);

            string truncated = outputStrings[0] + "." + outputStrings[1];

            if (decimal.Parse(truncated) == (int)(decimal.Parse(truncated))) truncated = outputStrings[0];

            foreach (Variable v in variable)
            {
                truncated = truncated + v.ToString();
            }

            return truncated;
        }

        public decimal ToDecimal()
        {
            return (decimal) Numerator / (decimal) Denominator;
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override readonly bool Equals(object obj)
        {
            if (obj == null) return false;
            return this == (MathValue)obj;
        }
    }

    
}
