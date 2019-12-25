using System.Collections.Generic;
using System.Linq;

namespace AdventOfCodeLib.Day12.Part2 {
	internal class MoonSystem1D {
		public List<Moon1D> Moons { get; private set; }

		public MoonSystem1D(List<int> moons) {
			Moons = moons.Select(m => new Moon1D(m, 0)).ToList();
		}

		private MoonSystem1D() {
			Moons = new List<Moon1D>();
		}

		public void Step() {
			for (int i = 0; i < Moons.Count; ++i) {
				for (int j = i + 1; j < Moons.Count; ++j) {
					var moonA = Moons[i];
					var moonB = Moons[j];

					if (moonA.Position < moonB.Position) {
						++moonA.Velocity;
						--moonB.Velocity;
					} else if (moonA.Position > moonB.Position) {
						--moonA.Velocity;
						++moonB.Velocity;
					}
				}
			}

			for (int i = 0; i < Moons.Count; ++i) {
				Moons[i].Position += Moons[i].Velocity;
			}
		}

		public int CompareTo(MoonSystem1D other) {
			if (Moons.Count < other.Moons.Count) {
				return -1;
			} else if (Moons.Count > other.Moons.Count) {
				return 1;
			} else {
				for (int i = 0; i < Moons.Count; ++i) {
					if (Moons[i] < other.Moons[i]) {
						return -1;
					} else if (Moons[i] > other.Moons[i]) {
						return 1;
					}
				}
				return 0;
			}
		}

		public static bool operator ==(MoonSystem1D a, MoonSystem1D b) {
			return a.CompareTo(b) == 0;
		}

		public static bool operator !=(MoonSystem1D a, MoonSystem1D b) {
			return a.CompareTo(b) != 0;
		}

		public static bool operator <(MoonSystem1D a, MoonSystem1D b) {
			return a.CompareTo(b) < 0;
		}

		public static bool operator <=(MoonSystem1D a, MoonSystem1D b) {
			return a.CompareTo(b) <= 0;
		}

		public static bool operator >(MoonSystem1D a, MoonSystem1D b) {
			return a.CompareTo(b) > 0;
		}

		public static bool operator >=(MoonSystem1D a, MoonSystem1D b) {
			return a.CompareTo(b) >= 0;
		}

		public MoonSystem1D Clone() {
			return new MoonSystem1D {
				Moons = new List<Moon1D>(Moons.Select(m => m.Clone()).ToList())
			};
		}

		public override bool Equals(object? obj) {
			if (obj == null) {
				return false;
			} else {
				if (obj is MoonSystem1D) {
					var o = (obj as MoonSystem1D)!;
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
