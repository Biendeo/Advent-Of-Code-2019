﻿using AdventOfCodeLib.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day05.Part1 {
	public static class Solution {
		public static long SolveFromInputFile(string inputFile) {
			var convertedProgram = new List<long>(File.ReadAllText(inputFile).Split(",").Select(c => long.Parse(c)));
			return Solve(convertedProgram);
		}

		private static long Solve(List<long> program) {
			var outputBuffer = new List<long>();
			var computer = new IntcodeComputer(program, new Queue<long>(new long[] { 1 }), outputBuffer);
			computer.RunProgram();
			return outputBuffer.Last();
		}
	}
}
