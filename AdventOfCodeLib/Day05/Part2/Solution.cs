using AdventOfCodeLib.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCodeLib.Day05.Part2 {
	public static class Solution {
		public static int SolveFromInputFile(string inputFile) {
			var convertedProgram = new List<int>(File.ReadAllText(inputFile).Split(",").Select(c => int.Parse(c)));
			return Solve(convertedProgram);
		}

		private static int Solve(List<int> program) {
			var computer = new IntcodeComputer(program, new List<int> { 5 });
			computer.RunProgram();
			return computer.GetLastOutputBuffer().Last();
		}
	}
}
