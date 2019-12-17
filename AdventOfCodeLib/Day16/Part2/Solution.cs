using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day16.Part2 {
	public static class Solution {
		public static string SolveFromInputFile(string inputFile) {
			return Solve(File.ReadAllText(inputFile).Trim().Select(c => c - '0').ToList(), 10000, 100);
		}

		public static string Solve(List<int> signal, int inputSignalRepeats, int phases) {
			if (inputSignalRepeats == 1) {
				int offset = int.Parse(string.Join("", signal.Take(7)));
				// Just substring the bit that is needed.
				var condensedSignal = signal.Skip(offset).ToList();
				for (int i = 0; i < phases; ++i) {
					Console.WriteLine($"Phase {i + 1}: {string.Join("", condensedSignal).Substring(0, 8)}");
					PerformPhase(condensedSignal);
				}
				return string.Join("", condensedSignal).Substring(0, 8);
			} else {
				return Solve(RepeatList(signal, inputSignalRepeats), 1, phases);
			}
		}

		private static void PerformPhase(List<int> signal) {
			// I'm cheating because I know my offset is more than halfway through my original signal.
			for (int i = signal.Count - 2; i >= 0; --i) {
				signal[i] = (signal[i] + signal[i + 1]) % 10;
			}
		}

		private static List<T> RepeatList<T>(List<T> input, int repeats) {
			var r = new List<T>(input.Count * repeats);

			for (int i = 0; i < repeats; ++i) {
				r.AddRange(input);
			}

			return r;
		}
	}
}
