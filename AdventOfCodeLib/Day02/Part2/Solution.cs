using AdventOfCodeLib.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day02.Part2 {
	public static class Solution {
		public static int SolveFromProgram(string program) {
			var convertedProgram = new List<long>(program.Split(",").Select(c => long.Parse(c)));
			return Solve(convertedProgram);
		}

		public static int SolveFromInputFile(string inputFile) {
			return Solve(new List<long>(File.ReadAllText(inputFile).Split(",").Select(c => long.Parse(c))));
		}

		private static int Solve(List<long> program) {
			int result = -1;
			Enumerable.Range(0, 10000).AsParallel().ForAll(x => {
				if (result == -1) {
					int noun = x / 100;
					int verb = x % 100;
					var modifiedProgram = new List<long>(program) {
						[1] = noun,
						[2] = verb
					};
					var computer = new IntcodeComputer(modifiedProgram);
					computer.RunProgram();
					if (computer.GetLastProgramState()[0] == 19690720) {
						result = x;
					}
				}
			});

			return result;
		}
	}
}
