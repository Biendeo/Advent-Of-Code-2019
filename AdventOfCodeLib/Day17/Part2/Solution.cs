using AdventOfCodeLib.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day17.Part2 {
	public static class Solution {
		public static int SolveFromInputFile(string inputFile) {
			var convertedProgram = new List<long>(File.ReadAllText(inputFile).Split(",").Select(c => long.Parse(c)));
			return Solve(convertedProgram);
		}

		private static int Solve(List<long> program) {
			program[0] = 2;
			var inputBuffer = new Queue<long>("B,C,B,A,C,B,A,C,B,A\nL,6,L,10,L,10,L,6\nL,6,L,4,R,12\nL,6,R,12,R,12,L,8\nn\n".Select(c => (long)c)); // My solution worked out by hand.
			var outputBuffer = new List<long>();

			var computer = new IntcodeComputer(program, inputBuffer, outputBuffer);

			computer.RunProgram();

			int score = (int)outputBuffer.Last();

			return score;
		}
	}
}
