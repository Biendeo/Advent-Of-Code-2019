using AdventOfCodeLib.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day21.Part1 {
	public static class Solution {
		public static int SolveFromInputFile(string inputFile) {
			var convertedProgram = new List<long>(File.ReadAllText(inputFile).Split(",").Select(c => long.Parse(c)));
			return Solve(convertedProgram);
		}

		private static int Solve(List<long> program) {
			var inputBuffer = new Queue<long>("NOT A T\nNOT C J\nOR T J\nAND D J\nWALK\n".Select(c => (long)c)); // My solution worked out by hand (and a bit of guessing).
			var outputBuffer = new List<long>();

			var computer = new IntcodeComputer(program, inputBuffer, outputBuffer);

			computer.RunProgram();

			return (int)outputBuffer.Last();
		}
	}
}