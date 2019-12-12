using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace AdventOfCodeLib.Day12.Part2 {
	public static class Solution {
		public static long SolveFromInputFile(string inputFile) {
			return Solve(File.ReadAllLines(inputFile));
		}

		public static long Solve(string[] inputs) {
			var system = new MoonSystem(inputs);

			var threads = new List<Thread>();
			long stepsX = 0;
			long stepsY = 0;
			long stepsZ = 0;
			threads.Add(new Thread(() => {
				var systemX = new MoonSystem1D(system.Moons.Select(m => m.Position.X).ToList());
				var startingSystemX = systemX.Clone();
				stepsX = 0;
				do {
					systemX.Step();
					++stepsX;
				} while (systemX != startingSystemX);
			}));
			threads.Add(new Thread(() => {
				var systemY = new MoonSystem1D(system.Moons.Select(m => m.Position.Y).ToList());
				var startingSystemY = systemY.Clone();
				stepsY = 0;
				do {
					systemY.Step();
					++stepsY;
				} while (systemY != startingSystemY);
			}));
			threads.Add(new Thread(() => {
				var systemZ = new MoonSystem1D(system.Moons.Select(m => m.Position.Z).ToList());
				var startingSystemZ = systemZ.Clone();
				stepsZ = 0;
				do {
					systemZ.Step();
					++stepsZ;
				} while (systemZ != startingSystemZ);
			}));

			threads.ForEach(t => t.Start());
			threads.ForEach(t => t.Join());

			return LCM(LCM(stepsX, stepsY), stepsZ);
		}

		private static long LCM(long a, long b) {
			return a * b / GCD(a, b);
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
	}
}
