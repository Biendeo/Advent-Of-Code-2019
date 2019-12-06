using AdventOfCodeLib.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day03.Part1 {
	public static class Solution {
		public static int SolveFromPaths(string path1, string path2) {
			return Solve(path1.Split(",").ToList(), path2.Split(",").ToList());
		}

		public static int SolveFromInputFile(string inputFile) {
			string[] input = File.ReadAllLines(inputFile);
			return SolveFromPaths(input[0], input[1]);
		}

		private static int Solve(List<string> path1, List<string> path2) {
			var path1Coordinates = GetPathCoordinates(path1);
			var path2Coordinates = GetPathCoordinates(path2);
			path1Coordinates.IntersectWith(path2Coordinates);
			return path1Coordinates.AsParallel().Min(c => c.Manhatten());
		}

		private static HashSet<Coordinate> GetPathCoordinates(List<string> path) {
			var coordinates = new HashSet<Coordinate>();
			var currentCoordinate = new Coordinate(0, 0);
			foreach (string step in path) {
				char direction = step[0];
				int count = int.Parse(step.Substring(1));
				while (count > 0) {
					switch (direction) {
						case 'R':
							++currentCoordinate.X;
							break;
						case 'L':
							--currentCoordinate.X;
							break;
						case 'U':
							++currentCoordinate.Y;
							break;
						case 'D':
							--currentCoordinate.Y;
							break;
					}
					coordinates.Add(currentCoordinate.Clone());
					--count;
				}
			}
			return coordinates;
		}
	}
}
