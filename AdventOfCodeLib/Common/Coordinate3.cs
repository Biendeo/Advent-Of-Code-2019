using System;

namespace AdventOfCodeLib.Common {
	public class Coordinate3 : IComparable<Coordinate3> {
		public int X { get; set; }
		public int Y { get; set; }
		public int Z { get; set; }

		public Coordinate3(int x, int y, int z) {
			X = x;
			Y = y;
			Z = z;
		}

		public int Manhatten() {
			return Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
		}

		public int CompareTo(Coordinate3 other) {
			if (X < other.X) {
				return -1;
			} else if (X > other.X) {
				return 1;
			} else if (Y < other.Y) {
				return -1;
			} else if (Y > other.Y) {
				return 1;
			} else if (Z < other.Z) {
				return -1;
			} else if (Z > other.Z) {
				return 1;
			} else {
				return 0;
			}
		}

		public static bool operator ==(Coordinate3 a, Coordinate3 b) {
			return a.CompareTo(b) == 0;
		}

		public static bool operator !=(Coordinate3 a, Coordinate3 b) {
			return a.CompareTo(b) != 0;
		}

		public static bool operator <(Coordinate3 a, Coordinate3 b) {
			return a.CompareTo(b) < 0;
		}

		public static bool operator <=(Coordinate3 a, Coordinate3 b) {
			return a.CompareTo(b) <= 0;
		}

		public static bool operator >(Coordinate3 a, Coordinate3 b) {
			return a.CompareTo(b) > 0;
		}

		public static bool operator >=(Coordinate3 a, Coordinate3 b) {
			return a.CompareTo(b) >= 0;
		}

		public static Coordinate3 operator +(Coordinate3 a, Coordinate3 b) {
			return new Coordinate3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}

		public static Coordinate3 operator -(Coordinate3 a, Coordinate3 b) {
			return new Coordinate3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}

		public Coordinate3 Clone() {
			return new Coordinate3(X, Y, Z);
		}

		public override bool Equals(object? obj) {
			if (obj == null) {
				return false;
			} else {
				if (obj is Coordinate3) {
					var o = (obj as Coordinate3)!;
					if (o == this) {
						return true;
					} else {
						return CompareTo(o) == 0;
					}
				} else {
					return false;
				}
			}
		}

		public override int GetHashCode() {
			return X << 22 + Y << 11 + Z;
		}
	}
}
