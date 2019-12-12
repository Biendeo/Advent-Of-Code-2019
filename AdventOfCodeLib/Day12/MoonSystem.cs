using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCodeLib.Day12 {
	internal class MoonSystem : IComparable<MoonSystem> {
		public List<Moon> Moons { get; }

		private MoonSystem(List<Moon> moons) {
			Moons = moons;
		}

		public MoonSystem(string[] inputs) {
			Moons = inputs.Select(s => new Moon(s)).ToList();
		}

		public int PotentialEnergy => Moons.Sum(m => m.PotentialEnergy);

		public int KineticEnergy => Moons.Sum(m => m.KineticEnergy);

		public int TotalEnergy => Moons.Sum(m => m.TotalEnergy);

		public void Step() {
			for (int i = 0; i < Moons.Count; ++i) {
				for (int j = i + 1; j < Moons.Count; ++j) {
					var moonA = Moons[i];
					var moonB = Moons[j];

					if (moonA.Position.X < moonB.Position.X) {
						++moonA.Velocity.X;
						--moonB.Velocity.X;
					} else if (moonA.Position.X > moonB.Position.X) {
						--moonA.Velocity.X;
						++moonB.Velocity.X;
					}

					if (moonA.Position.Y < moonB.Position.Y) {
						++moonA.Velocity.Y;
						--moonB.Velocity.Y;
					} else if (moonA.Position.Y > moonB.Position.Y) {
						--moonA.Velocity.Y;
						++moonB.Velocity.Y;
					}

					if (moonA.Position.Z < moonB.Position.Z) {
						++moonA.Velocity.Z;
						--moonB.Velocity.Z;
					} else if (moonA.Position.Z > moonB.Position.Z) {
						--moonA.Velocity.Z;
						++moonB.Velocity.Z;
					}
				}
			}

			foreach (var moon in Moons) {
				moon.Position += moon.Velocity;
			}
		}

		public int CompareTo(MoonSystem other) {
			if (Moons[0] < other.Moons[0]) {
				return -1;
			} else if (Moons[0] > other.Moons[0]) {
				return 1;
			} else if (Moons[1] < other.Moons[1]) {
				return -1;
			} else if (Moons[1] > other.Moons[1]) {
				return 1;
			} else if (Moons[2] < other.Moons[2]) {
				return -1;
			} else if (Moons[2] > other.Moons[2]) {
				return 1;
			} else if (Moons[3] < other.Moons[3]) {
				return -1;
			} else if (Moons[3] > other.Moons[3]) {
				return 1;
			} else {
				return 0;
			}
		}

		public static bool operator ==(MoonSystem a, MoonSystem b) {
			return a.CompareTo(b) == 0;
		}

		public static bool operator !=(MoonSystem a, MoonSystem b) {
			return a.CompareTo(b) != 0;
		}

		public static bool operator <(MoonSystem a, MoonSystem b) {
			return a.CompareTo(b) < 0;
		}

		public static bool operator <=(MoonSystem a, MoonSystem b) {
			return a.CompareTo(b) <= 0;
		}

		public static bool operator >(MoonSystem a, MoonSystem b) {
			return a.CompareTo(b) > 0;
		}

		public static bool operator >=(MoonSystem a, MoonSystem b) {
			return a.CompareTo(b) >= 0;
		}

		public MoonSystem Clone() {
			return new MoonSystem(Moons.Select(m => m.Clone()).ToList());
		}

		public override bool Equals(object? obj) {
			if (obj == null) {
				return false;
			} else {
				if (obj is MoonSystem) {
					var o = (obj as MoonSystem)!;
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
			return Moons[0].GetHashCode() << 24 + Moons[1].GetHashCode() << 16 + Moons[2].GetHashCode() << 8 + Moons[3].GetHashCode();
		}
	}
}
