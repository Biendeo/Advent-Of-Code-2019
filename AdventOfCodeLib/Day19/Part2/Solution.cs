using AdventOfCodeLib.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day19.Part2 {
	public static class Solution {
		public static int SolveFromInputFile(string inputFile) {
			var convertedProgram = new List<long>(File.ReadAllText(inputFile).Split(",").Select(c => long.Parse(c)));
			return Solve(convertedProgram);
		}

		private static int Solve(List<long> program) {
			int distance = 0;
			int foundX = -1;
			int foundY = -1;
			
			while (foundX == -1 && foundY == -1) {
				Enumerable.Range(0, distance).AsParallel().ForAll(squareY => {
				var outputBuffer = new List<long>();
					int squareX = distance - squareY;
					bool yFit = true;
					bool xFit = true;
					for (int y = 0; y < 100 && yFit; ++y) {
						new IntcodeComputer(program, new Queue<long>(new long[] { squareX, squareY + y }), outputBuffer).RunProgram();
						yFit &= outputBuffer.Single() == 1;
						outputBuffer.Clear();
					}
					for (int x = 1; x < 100 && xFit; ++x) {
						new IntcodeComputer(program, new Queue<long>(new long[] { squareX + x, squareY }), outputBuffer).RunProgram();
						xFit &= outputBuffer.Single() == 1;
						outputBuffer.Clear();
					}
					if (xFit && yFit) {
						foundX = squareX;
						foundY = squareY;
					}
				});
				++distance;
			}

			return foundX * 10000 + foundY;
		}
	}
}
