using AdventOfCodeLib.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day02.Part1 {
	public static class Solution {
		public static int SolveFromProgram(string program, bool add1202 = true) {
			return SolveFromProgramOutputProgram(program, add1202)[0];
		}
		public static List<int> SolveFromProgramOutputProgram(string program, bool add1202 = true) {
			var convertedProgram = new List<int>(program.Split(",").Select(c => int.Parse(c)));
			if (add1202) {
				convertedProgram[1] = 12;
				convertedProgram[2] = 2;
			}
			return Solve(convertedProgram);
		}

		public static int SolveFromInputFile(string inputFile, bool add1202 = true) {
			return SolveFromInputFileOutputProgram(inputFile, add1202)[0];
		}

		public static List<int> SolveFromInputFileOutputProgram(string inputFile, bool add1202 = true) {
			var convertedProgram = new List<int>(File.ReadAllText(inputFile).Split(",").Select(c => int.Parse(c)));
			if (add1202) {
				convertedProgram[1] = 12;
				convertedProgram[2] = 2;
			}
			return Solve(convertedProgram);
		}

		private static List<int> Solve(List<int> program) {
			var computer = new IntcodeComputer(program);
			computer.RunProgram();
			return computer.GetLastProgramState();
		}
	}
}
