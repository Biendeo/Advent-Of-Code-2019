using AdventOfCodeLib.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day02.Part2 {
	public static class Solution {
		public static int SolveFromProgram(string program) {
			var convertedProgram = new List<int>(program.Split(",").Select(c => int.Parse(c)));
			return Solve(convertedProgram);
		}

		public static int SolveFromInputFile(string inputFile) {
			return Solve(new List<int>(File.ReadAllText(inputFile).Split(",").Select(c => int.Parse(c))));
		}

		private static int Solve(List<int> program) {
			for (int noun = 0; noun < 100; ++noun) {
				for (int verb = 0; verb < 100; ++verb) {
					var modifiedProgram = new List<int>(program) {
						[1] = noun,
						[2] = verb
					};
					var computer = new IntcodeComputer(modifiedProgram, new List<int>());
					computer.RunProgram();
					if (computer.GetLastProgramState()[0] == 19690720) {
						return noun * 100 + verb;
					}
				}
			}

			return -1;
		}
	}
}
