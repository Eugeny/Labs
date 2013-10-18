using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Pankov.Lab1.RationalLab
{
    public class Rational : IComparable<Rational>, IEquatable<Rational>
    {
        public int Numerator { get; private set; }
        public int Denominator { get; private set; }

        /// <summary>
        /// Construct default rational with value of 1
        /// </summary>
        public Rational()
        {
            Numerator = 0;
            Denominator = 1;
        }


        /// <summary>
        /// Construct rational with given numerator and denominator
        /// </summary>
        /// <param name="n">Numerator</param>
        /// <param name="d">Denominator</param>
        public Rational(int n, int d)
        {
            Numerator = n;
            Denominator = d;
        }

        /// <summary>
        /// Construct rational with given numerator and denominator, checking for zero denominator
        /// </summary>
        /// <param name="n">Numerator</param>
        /// <param name="d">Denominator</param>
        public static Rational CreateAndVerify(int n, int d)
        {
            if (d == 0) return null;
            return new Rational(n, d);
        }

        /// <summary>
        /// Compares this rational against another one
        /// </summary>
        /// <param name="other">Other rational</param>
        /// <returns>Comparison result</returns>
        public int CompareTo(Rational other)
        {
            int lcm = Util.LCM(Denominator, other.Denominator);
            return (Numerator * lcm / Denominator).CompareTo(other.Numerator * lcm / other.Denominator);
        }

        /// <summary>
        /// Checks the equity between rationals
        /// </summary>
        /// <param name="other">Other rational</param>
        /// <returns>Equity value</returns>
        public bool Equals(Rational other)
        {
            return CompareTo(other) == 0;
        }

        /// <summary>
        /// Checks the equity between this rational and another (Rational) object
        /// </summary>
        /// <param name="obj">Another object</param>
        /// <returns>Equity status</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Rational)) return false;
            return Equals((Rational)obj);
        }

        /// <summary>
        /// Gets hashcode for this instance
        /// </summary>
        /// <returns>Hashcode</returns>
        public override int GetHashCode()
        {
            return Numerator.GetHashCode() + Denominator.GetHashCode();
        }

        #region Math
        /// <summary>
        /// Reduces rational
        /// </summary>
        /// <returns>Reduced rational</returns>
        public Rational Reduce()
        {
            int gcd = Util.GCD(Numerator, Denominator);
            return new Rational(Numerator / gcd, Denominator / gcd);
        }

        public static Rational operator +(Rational a, Rational b)
        {
            int lcm = Util.LCM(a.Denominator, b.Denominator);
            return new Rational(a.Numerator * lcm / a.Denominator + b.Numerator * lcm / b.Denominator, lcm).Reduce();
        }

        public static Rational operator -(Rational x)
        {
            return new Rational(-x.Numerator, x.Denominator);
        }

        public static Rational operator -(Rational a, Rational b)
        {
            return a + (-b);
        }

        public static Rational operator *(Rational a, Rational b)
        {
            return new Rational(a.Numerator * b.Numerator, a.Denominator * b.Denominator).Reduce();
        }

        public static Rational operator /(Rational a, Rational b)
        {
            return new Rational(a.Numerator * b.Denominator, a.Denominator * b.Numerator).Reduce();
        }

        //private static bool Equals(Rational a, Rational b)
        //{
        //    EqualityComparer<Rational>.Default.Equals(a, b);
        //}

        public static bool operator ==(Rational a, Rational b)
        {
            //return Equals(a, b);
            if ((object)a == null || (object)b == null) return false;
            return a.Equals(b);
        }

        public static bool operator !=(Rational a, Rational b)
        {
            return !(a == b);
        }

        public static bool operator >(Rational a, Rational b)
        {
            return a.CompareTo(b) > 0;
        }

        public static bool operator <(Rational a, Rational b)
        {
            return a.CompareTo(b) < 0;
        }
        #endregion

        #region Conversions
        public static implicit operator Rational(int x)
        {
            return new Rational(x, 1);
        }

        public static implicit operator Rational(double x)
        {
            string[] parts = x.ToString("0.#########").Split('.', ',');
            return new Rational(int.Parse(parts[0] + parts[1]), (int)Math.Pow(10, parts[1].Length)).Reduce();
        }

        public static implicit operator double(Rational x)
        {
            return ((double)x.Numerator) / x.Denominator;
        }
        #endregion

        #region String methods
        /// <summary>
        /// Tries to parse Rational in 0.00, 0.0(0) and 0/0 formats
        /// </summary>
        /// <param name="s">Input string</param>
        /// <returns>Rational</returns>
        public static Rational Parse(string s)
        {
            if (Regex.IsMatch(s, @"^\d+[.,]\d*\(\d+\)$"))
            {
                string[] items = s.Split(',', '.', '(');
                Rational cons = new Rational(int.Parse(items[0] + items[1]), (int)Math.Pow(10, items[1].Length));
                items[2] = items[2].TrimEnd(')');
                int den = 0;
                for (int i = 0; i < items[2].Length; i++)
                    den = den * 10 + 9;
                den *= (int)Math.Pow(10, items[1].Length);
                Rational rep = new Rational(int.Parse(items[2]), den);
                return cons + rep;
            }
            if (Regex.IsMatch(s, @"^\d+/\d+"))
            {
                string[] items = s.Split('/');
                return new Rational(int.Parse(items[0]), int.Parse(items[1]));
            }
            if (Regex.IsMatch(s, @"^\d+$"))
            {
                return new Rational(int.Parse(s), 1);
            }
            if (Regex.IsMatch(s, @"^\d+[,.]\d+$"))
            {
                return (Rational)double.Parse(s);
            }
            throw new FormatException("Wrong rational string");
        }

        /// <summary>
        /// Stringifies Rational as a ratio string
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return String.Format("{0}/{1}", Numerator, Denominator);
        }

        /// <summary>
        /// Stringifies Rational as a decimal number
        /// </summary>
        /// <returns>String</returns>
        public string ToDecimalString()
        {
            return ((double)Numerator / Denominator).ToString();
        }
        #endregion
    }
}
