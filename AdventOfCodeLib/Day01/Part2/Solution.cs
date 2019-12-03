using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day01.Part2 {
	public static class Solution {
		public static int SolveFromMass(int mass) {
			return Solve(new List<int> { mass });
		}

		public static int SolveFromInputFile(string inputFile) {
			return Solve(new List<int>(File.ReadAllLines(inputFile).Select(l => int.Parse(l))));
		}

		private static int Solve(List<int> masses) {
			return masses.Sum(m => GetFuelCount(m));
		}

		private static int GetFuelCount(int mass) {
			return mass > 6 ? GetImmediateFuelCount(mass) + GetFuelCount(GetImmediateFuelCount(mass)) : 0;
		}

		private static int GetImmediateFuelCount(int mass) {
			return Math.Max(mass / 3 - 2, 0);
		}
	}
}
