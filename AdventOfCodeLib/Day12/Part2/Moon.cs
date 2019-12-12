using System;

namespace AdventOfCodeLib.Day12 {
	internal class Moon1D : IComparable<Moon1D> {
		public int Position { get; set; }

		public int Velocity { get; set; }

		public Moon1D(int position, int velocity) {
			Position = position;
			Velocity = velocity;
		}

		public int PotentialEnergy => Position;

		public int KineticEnergy => Velocity;

		public int TotalEnergy => PotentialEnergy * KineticEnergy;

		public int CompareTo(Moon1D other) {
			if (Position < other.Position) {
				return -1;
			} else if (Position > other.Position) {
				return 1;
			} else if (Velocity < other.Velocity) {
				return -1;
			} else if (Velocity > other.Velocity) {
				return 1;
			} else {
				return 0;
			}
		}

		public static bool operator ==(Moon1D a, Moon1D b) {
			return a.CompareTo(b) == 0;
		}

		public static bool operator !=(Moon1D a, Moon1D b) {
			return a.CompareTo(b) != 0;
		}

		public static bool operator <(Moon1D a, Moon1D b) {
			return a.CompareTo(b) < 0;
		}

		public static bool operator <=(Moon1D a, Moon1D b) {
			return a.CompareTo(b) <= 0;
		}

		public static bool operator >(Moon1D a, Moon1D b) {
			return a.CompareTo(b) > 0;
		}

		public static bool operator >=(Moon1D a, Moon1D b) {
			return a.CompareTo(b) >= 0;
		}

		public Moon1D Clone() {
			return new Moon1D(Position, Velocity);
		}

		public override bool Equals(object? obj) {
			if (obj == null) {
				return false;
			} else {
				if (obj is Moon1D) {
					var o = (obj as Moon1D)!;
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
			return Position.GetHashCode() << 16 + Velocity.GetHashCode();
		}
	}
}
