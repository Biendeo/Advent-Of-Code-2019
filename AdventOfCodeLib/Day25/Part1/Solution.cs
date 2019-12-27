using AdventOfCodeLib.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCodeLib.Day25.Part1 {
	public static class Solution {
		public static int SolveFromInputFile(string inputFile) {
			var convertedProgram = new List<long>(File.ReadAllText(inputFile).Split(",").Select(c => long.Parse(c)));
			return Solve(convertedProgram);
		}

		private static int Solve(List<long> program) {
			var inputBuffer = new Queue<long>();
			var outputBuffer = new List<long>();

			"north\ntake mouse\nnorth\ntake pointer\nsouth\nsouth\nwest\ntake monolith\nnorth\nwest\ntake food ration\nsouth\ntake space law space brochure\nnorth\neast\nsouth\nsouth\ntake sand\nsouth\nwest\ntake asterisk\nsouth\ntake mutex\nnorth\neast\nnorth\nnorth\neast\nsouth\nsouth\nwest\nsouth\neast\n".ToList().ForEach(c => inputBuffer.Enqueue(c));

			var items = new List<string> {
				"mouse",
				"pointer",
				"monolith",
				"food ration",
				"space law space brochure",
				"sand",
				"asterisk",
				"mutex"
			};

			for (int i = 0; i < (1 << items.Count); ++i) {
				for (int j = 0; j < items.Count; ++j) {
					if ((i & (1 << j)) != 0) {
						$"drop {items[j]}\n".ToList().ForEach(c => inputBuffer.Enqueue(c));
					}
				}
				"east\n".ToList().ForEach(c => inputBuffer.Enqueue(c));
				for (int j = 0; j < items.Count; ++j) {
					if ((i & (1 << j)) != 0) {
						$"take {items[j]}\n".ToList().ForEach(c => inputBuffer.Enqueue(c));
					}
				}
			}

			new IntcodeComputer(program, () => {
				if (inputBuffer.Count == 0) {
					outputBuffer.ForEach(c => Console.Write((char)c));
					outputBuffer.Clear();
					string input = Console.ReadLine();
					foreach (char c in input) {
						inputBuffer.Enqueue(c);
					}
					inputBuffer.Enqueue('\n');
				}
				return inputBuffer.Dequeue();
			}, (x) => outputBuffer.Add(x)).RunProgram();

			var regex = new Regex("([0-9]+) on the keypad at the main airlock.\"");
			return int.Parse(regex.Match(string.Concat(outputBuffer.Select(c => (char)c))).Groups[1].Value);
		}
	}
}