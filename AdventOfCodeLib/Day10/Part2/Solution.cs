using AdventOfCodeLib.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day10.Part2 {
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

			var chosenAsteroid = asteroids.AsParallel().OrderByDescending(a => asteroids.Where(b => b != a).Select(b => Math.Atan2(b.Y - a.Y, b.X - a.X)).Distinct().Count()).First();

			asteroids.Remove(chosenAsteroid);

			var angles = asteroids.Select(b => ShortestIntegerStep(new Coordinate(b.X - chosenAsteroid.X, b.Y - chosenAsteroid.Y))).Distinct().OrderBy(c => Math.PI / 2.0 - (Math.Atan2(-c.Y, c.X) > (Math.PI / 2.0) ? Math.Atan2(-c.Y, c.X) - Math.PI * 2.0 : Math.Atan2(-c.Y, c.X))).ToList();

			var removedAsteroids = new List<Coordinate>();

			int i = 0;
			while (asteroids.Count > 0) {
				var currentCoordinate = chosenAsteroid.Clone();
				while (!asteroids.Contains(currentCoordinate) && currentCoordinate.X >= 0 &&  currentCoordinate.X < asteroidMap[0].Length && currentCoordinate.Y >= 0 && currentCoordinate.Y < asteroidMap.Length) {
					currentCoordinate += angles[i % angles.Count];
				}
				if (asteroids.Contains(currentCoordinate)) {
					removedAsteroids.Add(currentCoordinate);
					asteroids.Remove(currentCoordinate);
				}
				++i;
			}

			return removedAsteroids[199].X * 100 + removedAsteroids[199].Y;
		}

		private static Coordinate ShortestIntegerStep(Coordinate c) {
			int gcd = GCD(c.X, c.Y);
			return new Coordinate(c.X / gcd, c.Y / gcd);
		}

		private static int GCD(int a, int b) {
			if (a < 0) {
				return GCD(-a, b);
			} else if (b < 0) {
				return GCD(a, -b);
			} else if (a < b) {
				return GCD(b, a);
			} else if (b == 0) {
				return a;
			} else {
				return GCD(b, a % b);
			}
		}
	}
}
