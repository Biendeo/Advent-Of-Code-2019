using AdventOfCodeLib.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day10.Part1 {
	public static class Solution {
		public static int SolveFromInputFile(string inputFile) {
			return Solve(File.ReadAllLines(inputFile));
		}

		public static int Solve(string[] asteroidMap) {
			var asteroids = new HashSet<Coordinate>();
			for (int y = 0; y < asteroidMap.Length; ++y) {
				for (int x = 0; x < asteroidMap[y].Length; ++x) {
					if (asteroidMap[y][x] == '#') {
						asteroids.Add(new Coordinate(x, y));
					}
				}
			}

			return asteroids.AsParallel().Max(a => asteroids.Where(b => b != a).Select(b => Math.Atan2(b.Y - a.Y, b.X - a.X)).Distinct().Count());
		}
	}
}