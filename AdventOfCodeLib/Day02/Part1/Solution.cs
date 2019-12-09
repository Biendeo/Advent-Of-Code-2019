using AdventOfCodeLib.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day02.Part1 {
	public static class Solution {
		public static long SolveFromProgram(string program, bool add1202 = true) {
			return SolveFromProgramOutputProgram(program, add1202)[0];
		}
		public static List<long> SolveFromProgramOutputProgram(string program, bool add1202 = true) {
			var convertedProgram = new List<long>(program.Split(",").Select(c => long.Parse(c)));
			if (add1202) {
				convertedProgram[1] = 12;
				convertedProgram[2] = 2;
			}
			return Solve(convertedProgram);
		}

		public static long SolveFromInputFile(string inputFile, bool add1202 = true) {
			return SolveFromInputFileOutputProgram(inputFile, add1202)[0];
		}

		public static List<long> SolveFromInputFileOutputProgram(string inputFile, bool add1202 = true) {
			var convertedProgram = new List<long>(File.ReadAllText(inputFile).Split(",").Select(c => long.Parse(c)));
			if (add1202) {
				convertedProgram[1] = 12;
				convertedProgram[2] = 2;
			}
			return Solve(convertedProgram);
		}

		private static List<long> Solve(List<long> program) {
			var computer = new IntcodeComputer(program);
			computer.RunProgram();
			return computer.GetLastProgramState();
		}
	}
}
