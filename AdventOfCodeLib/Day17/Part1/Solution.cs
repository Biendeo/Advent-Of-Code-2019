using AdventOfCodeLib.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day17.Part1 {
	public static class Solution {
		public static int SolveFromInputFile(string inputFile) {
			var convertedProgram = new List<long>(File.ReadAllText(inputFile).Split(",").Select(c => long.Parse(c)));
			return Solve(convertedProgram);
		}

		private static int Solve(List<long> program) {
			var inputBuffer = new Queue<long>();
			var outputBuffer = new List<long>();

			var computer = new IntcodeComputer(program, inputBuffer, outputBuffer);

			computer.RunProgram();

			var map = GetMapFromOutput(outputBuffer);

			return GetAlignmentParameterSum(map);
		}

		private static List<List<char>> GetMapFromOutput(List<long> outputBuffer) {
			var r = new List<List<char>> {
				new List<char>()
			};
			foreach (char x in outputBuffer) {
				if (x == '\n') {
					r.Add(new List<char>());
				} else {
					r.Last().Add(x);
				}
			}
			return r;
		}

		private static int GetAlignmentParameterSum(List<List<char>> map) {
			int sum = 0;
			for (int y = 1; y < map.Count - 1; ++y) {
				for (int x = 1; x < map[y].Count - 1; ++x) {
					if (map[y][x] == '#' && map[y][x - 1] == '#' && map[y][x + 1] == '#' && map[y - 1][x] == '#' && map[y + 1][x] == '#') {
						sum += y * x;
					}
				}
			}
			return sum;
		}
	}
}