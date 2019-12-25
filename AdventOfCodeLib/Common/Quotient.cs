using System;

namespace AdventOfCodeLib.Common {
	internal class Quotient {
		public long Numerator { get; private set; }
		public long Denominator { get; private set; }

		public Quotient(long numerator, long denominator) {
			Numerator = numerator;
			Denominator = denominator;
		}

		private static Quotient Normalise(Quotient q) {
			long gcd = GCD(Math.Abs(q.Numerator), Math.Abs(q.Denominator));
			return new Quotient(q.Numerator / gcd, q.Denominator / gcd);
		}

		private static long GCD(long a, long b) {
			if (a < b) {
				return GCD(b, a);
			} else if (b == 0) {
				return a;
			} else {
				return GCD(b, a % b);
			}
		}

		private static long LCM(long a, long b) {
			return a * b / GCD(a, b);
		}

		public static Quotient operator -(Quotient q) {
			return new Quotient(-q.Numerator, q.Denominator);
		}

		public static Quotient operator +(Quotient a, Quotient b) {
			long lcm = LCM(Math.Abs(a.Denominator), Math.Abs(b.Denominator));
			return Normalise(new Quotient(a.Numerator * lcm / a.Denominator + b.Numerator * lcm / b.Denominator, lcm));
		}

		public static Quotient operator -(Quotient a, Quotient b) {
			return a + -b;
		}

		public static Quotient operator *(Quotient a, Quotient b) {
			return Normalise(new Quotient(a.Numerator * b.Numerator, a.Denominator * b.Denominator));
		}

		public static Quotient operator /(Quotient a, Quotient b) {
			return a * new Quotient(b.Denominator, b.Numerator);
		}

		public static explicit operator long(Quotient q) {
			return q.Numerator / q.Denominator;
		}
	}
}
