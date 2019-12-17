using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day16.Part1 {
	public static class Solution {
		public static string SolveFromInputFile(string inputFile) {
			return Solve(File.ReadAllText(inputFile).Trim().Select(c => c - '0').ToList(), 100);
		}
		public static string Solve(List<int> signal, int phases) {
			for (int i = 0; i < phases; ++i) {
				signal = PerformPhase(signal);
			}
			return string.Join("", signal).Substring(0, 8);
		}


		private static List<int> PerformPhase(List<int> signal) => Enumerable.Range(0, signal.Count).Select(i => {
			return Math.Abs(Enumerable.Range(0, signal.Count).Sum(j => {
				int modulo = (j + 1) / (i + 1) % 4;
				int value = modulo == 1 ? 1 : modulo == 3 ? -1 : 0;
				return signal[j] * value;
			})) % 10;
		}).ToList();
	}
}