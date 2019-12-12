using AdventOfCodeLib.Common;
using System;
using System.Text.RegularExpressions;

namespace AdventOfCodeLib.Day12 {
	internal class Moon : IComparable<Moon> {
		public Coordinate3 Position { get; set; }

		public Coordinate3 Velocity { get; set; }

		private Moon(Coordinate3 position, Coordinate3 velocity) {
			Position = position.Clone();
			Velocity = velocity.Clone();
		}

		public Moon(string inputString) {
			var regex = new Regex("^<x=(-?[0-9]+), y=(-?[0-9]+), z=(-?[0-9]+)>$");
			var match = regex.Match(inputString);
			Position = new Coordinate3(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value));
			Velocity = new Coordinate3(0, 0, 0);
		}

		public int PotentialEnergy => Position.Manhatten();

		public int KineticEnergy => Velocity.Manhatten();

		public int TotalEnergy => PotentialEnergy * KineticEnergy;

		public int CompareTo(Moon other) {
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

		public static bool operator ==(Moon a, Moon b) {
			return a.CompareTo(b) == 0;
		}

		public static bool operator !=(Moon a, Moon b) {
			return a.CompareTo(b) != 0;
		}

		public static bool operator <(Moon a, Moon b) {
			return a.CompareTo(b) < 0;
		}

		public static bool operator <=(Moon a, Moon b) {
			return a.CompareTo(b) <= 0;
		}

		public static bool operator >(Moon a, Moon b) {
			return a.CompareTo(b) > 0;
		}

		public static bool operator >=(Moon a, Moon b) {
			return a.CompareTo(b) >= 0;
		}

		public Moon Clone() {
			return new Moon(Position, Velocity);
		}

		public override bool Equals(object? obj) {
			if (obj == null) {
				return false;
			} else {
				if (obj is Moon) {
					var o = (obj as Moon)!;
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
