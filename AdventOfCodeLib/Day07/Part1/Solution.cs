﻿using AdventOfCodeLib.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day07.Part1 {
	public static class Solution {
		public static long SolveFromInputFile(string inputFile) {
			var convertedProgram = new List<long>(File.ReadAllText(inputFile).Split(",").Select(c => long.Parse(c)));
			return Solve(convertedProgram);
		}

		public static long Solve(List<long> program) {
			return new List<long> { 0, 1, 2, 3, 4 }.GetAllCombinations().AsParallel().Max(settings => {
				var lastOutput = new List<long> { 0 };
				foreach (long setting in settings) {
					var output = new List<long>();
					var computer = new IntcodeComputer(program, new Queue<long>(new long[] { setting, lastOutput.Single() }), output);
					computer.RunProgram();
					lastOutput = output;
				}
				return lastOutput.Single();
			});
		}

		private static IEnumerable<IEnumerable<T>> GetAllCombinations<T>(this List<T> l) {
			foreach (var x in l) {
				var newList = new List<T>(l);
				newList.Remove(x);
				if (newList.Count == 0) {
					yield return new List<T> { x };
				} else {
					foreach (var r in newList.GetAllCombinations()) {
						yield return new List<T> { x }.Concat(r);
					}
				}
			}
		}
	}
}
