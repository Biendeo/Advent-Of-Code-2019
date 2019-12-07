using AdventOfCodeLib.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day05.Part1 {
	public static class Solution {
		public static int SolveFromInputFile(string inputFile) {
			var convertedProgram = new List<int>(File.ReadAllText(inputFile).Split(",").Select(c => int.Parse(c)));
			return Solve(convertedProgram);
		}

		private static int Solve(List<int> program) {
			var outputBuffer = new List<int>();
			var computer = new IntcodeComputer(program, new Queue<int>(new int[] { 1 }), outputBuffer);
			computer.RunProgram();
			return outputBuffer.Last();
		}
	}
}
