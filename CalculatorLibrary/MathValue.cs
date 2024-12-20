﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary
{
    public struct MathValue
    { 
        public MathValue(decimal numerator = 0, decimal denominator = 1)
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
            Reduce();
        }

        private decimal numerator;
        private decimal denominator;

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

        public static decimal GCD(decimal m, decimal n)
        {
            if (m == 0) return Math.Abs(n);
            return GCD(n % m, m);
        }

        public static MathValue operator + (MathValue a, MathValue b)
        {
            MathValue value;

            if (a.Denominator == b.Denominator) value = new MathValue(a.Numerator + b.Numerator, a.Denominator);
            else value = new MathValue((a.Numerator * b.Denominator) + (b.Numerator * a.Denominator), a.Denominator * b.Denominator);

            value.Reduce();
            return value;
        }

        public static MathValue operator - (MathValue a, MathValue b)
        {
            MathValue value;

            if (a.Denominator == b.Denominator) value = new MathValue(a.Numerator - b.Numerator, a.Denominator);
            else value = new MathValue((a.Numerator * b.Denominator) - (b.Numerator * a.Denominator), a.Denominator * b.Denominator);

            value.Reduce();
            return value;
        }


        public static MathValue operator * (MathValue a, MathValue b)
        {
            MathValue value = new MathValue(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
            value.Reduce();
            return value;
        }

        public static MathValue operator * (decimal a, MathValue b)
        {
            MathValue value = new MathValue(a * b.Numerator, b.Denominator);
            value.Reduce();
            return value;
        }

        public static MathValue operator *(MathValue a, decimal b)
        {
            MathValue value = new MathValue(b * a.Numerator, a.Denominator);
            value.Reduce();
            return value;
        }

        public static MathValue operator / (MathValue a, MathValue b)
        {
            MathValue value = new MathValue(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
            value.Reduce();
            return value;
        }

        public static MathValue operator / (decimal a, MathValue b)
        {
            MathValue value = new MathValue(a * b.Denominator, b.Numerator);
            value.Reduce();
            return value;
        }

        public static bool operator == (MathValue a, MathValue b)
        {
            a.Reduce();
            b.Reduce();

            return (a.Numerator == b.Numerator && a.Denominator == b.Denominator);
        }

        public static bool operator ==(MathValue a, decimal b)
        {
            return ((a.Numerator / a.Denominator) == b);
        }

        public static bool operator != (MathValue a, MathValue b)
        {
            a.Reduce();
            b.Reduce();

            return (a.Numerator != b.Numerator || a.Denominator != b.Denominator);
        }

        public static bool operator !=(MathValue a, decimal b)
        {
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

        public void SquareRoot()
        {
            Numerator = (decimal) Math.Sqrt((double) Numerator);
            Denominator = (decimal) Math.Sqrt((double) Denominator);
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
            if (Numerator == 0 || Denominator == 1) return Numerator.ToString();
            return Numerator + "/" + Denominator;
        }

        public string ToStringDecimal()
        {
            decimal value = (Numerator / Denominator);

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
