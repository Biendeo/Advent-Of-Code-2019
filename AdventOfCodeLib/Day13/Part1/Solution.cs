using AdventOfCodeLib.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day13.Part1 {
	public static class Solution {
		public static int SolveFromInputFile(string inputFile) {
			var convertedProgram = new List<long>(File.ReadAllText(inputFile).Split(",").Select(c => long.Parse(c)));
			return Solve(convertedProgram);
		}

		private static int Solve(List<long> program) {
			var outputBuffer = new List<long>();
			var inputBuffer = new Queue<long>();

			var computer = new IntcodeComputer(program, inputBuffer, outputBuffer);

			computer.RunProgram();

			int maxX = -1;
			int maxY = -1;
			var map = new List<List<int>>();

			for (int i = 0; i < outputBuffer.Count; i += 3) {
				int x = (int)outputBuffer[i];
				int y = (int)outputBuffer[i + 1];
				int id = (int)outputBuffer[i + 2];

				maxX = Math.Max(maxX, x);
				maxY = Math.Max(maxY, y);
				while (map.Count <= maxY) {
					map.Add(new List<int>());
				}
				foreach (var l in map) {
					while (l.Count <= maxX) {
						l.Add(0);
					}
				}

				map[y][x] = id;
			}

			return map.Sum(c => c.Count(id => id == 2));
		}
	}
}