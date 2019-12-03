using System;
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
					ExecuteProgram(modifiedProgram);
					if (modifiedProgram[0] == 19690720) {
						return noun * 100 + verb;
					}
				}
			}

			return -1;
		}

		private static void ExecuteProgram(List<int> program) {
			int currentIndex = 0;

			while (currentIndex < program.Count) {
				if (program[currentIndex] == 1) {
					int num1 = program[program[currentIndex + 1]];
					int num2 = program[program[currentIndex + 2]];
					program[program[currentIndex + 3]] = num1 + num2;
					currentIndex += 4;
				} else if (program[currentIndex] == 2) {
					int num1 = program[program[currentIndex + 1]];
					int num2 = program[program[currentIndex + 2]];
					program[program[currentIndex + 3]] = num1 * num2;
					currentIndex += 4;
				} else if (program[currentIndex] == 99) {
					break;
				} else {
					throw new ArgumentException($"Program encountered an opcode of {program[currentIndex]}");
				}
			}
		}
	}
}
