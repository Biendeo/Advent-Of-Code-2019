using System;

namespace AdventOfCodeLib.Common {
	public class Coordinate : IComparable<Coordinate> {
		public int X { get; set; }
		public int Y { get; set; }

		public Coordinate(int x, int y) {
			X = x;
			Y = y;
		}

		public int Manhatten() {
			return Math.Abs(X) + Math.Abs(Y);
		}

		public int CompareTo(Coordinate other) {
			if (X < other.X) {
				return -1;
			} else if (X > other.X) {
				return 1;
			} else if (Y < other.Y) {
				return -1;
			} else if (Y > other.Y) {
				return 1;
			} else {
				return 0;
			}
		}

		public static bool operator ==(Coordinate a, Coordinate b) {
			return a.CompareTo(b) == 0;
		}

		public static bool operator !=(Coordinate a, Coordinate b) {
			return a.CompareTo(b) != 0;
		}

		public static bool operator <(Coordinate a, Coordinate b) {
			return a.CompareTo(b) < 0;
		}

		public static bool operator <=(Coordinate a, Coordinate b) {
			return a.CompareTo(b) <= 0;
		}

		public static bool operator >(Coordinate a, Coordinate b) {
			return a.CompareTo(b) > 0;
		}

		public static bool operator >=(Coordinate a, Coordinate b) {
			return a.CompareTo(b) >= 0;
		}

		public Coordinate Clone() {
			return new Coordinate(X, Y);
		}

		public override bool Equals(object? obj) {
			if (obj == null) {
				return false;
			} else {
				if (obj is Coordinate) {
					var o = (obj as Coordinate)!;
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
			return X * 65536 + Y;
		}
	}
}
