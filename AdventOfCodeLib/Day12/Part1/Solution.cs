using System.IO;

namespace AdventOfCodeLib.Day12.Part1 {
	public static class Solution {
		public static int SolveFromInputFile(string inputFile) {
			return Solve(File.ReadAllLines(inputFile), 1000);
		}

		public static int Solve(string[] inputs, int steps) {
			var system = new MoonSystem(inputs);

			for (int step = 0; step < steps; ++step) {
				system.Step();
			}

			return system.TotalEnergy;
		}
	}
}