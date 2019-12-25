using AdventOfCodeLib.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day19.Part1 {
	public static class Solution {
		public static int SolveFromInputFile(string inputFile) {
			var convertedProgram = new List<long>(File.ReadAllText(inputFile).Split(",").Select(c => long.Parse(c)));
			return Solve(convertedProgram);
		}

		private static int Solve(List<long> program) {
			var outputBuffer = new List<long>();

			for (int y = 0; y < 50; ++y) {
				for (int x = 0; x < 50; ++x) {
					new IntcodeComputer(program, new Queue<long>(new long[] { x, y }), outputBuffer).RunProgram();
				}
			}
			
			return (int)outputBuffer.Sum();
		}
	}
}