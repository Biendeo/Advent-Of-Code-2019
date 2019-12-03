using System;
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
			var convertedProgram = new List<int>(File.ReadAllText(inputFile).Split(",").Select(c => int.Parse(c))); if (add1202) {
				convertedProgram[1] = 12;
				convertedProgram[2] = 2;
			}
			return Solve(convertedProgram);
		}

		private static List<int> Solve(List<int> program) {
			var runningProgram = new List<int>(program);
			ExecuteProgram(runningProgram);
			return runningProgram;
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
