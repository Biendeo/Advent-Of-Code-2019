using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day24.Part1 {
	public static class Solution {

		public static int SolveFromInputFile(string inputFile) {
			return Solve(File.ReadAllLines(inputFile).ToList());
		}

		public static int Solve(List<string> initialState) {
			int width = initialState[0].Length;
			int height = initialState.Count;

			var seenStates = new HashSet<long>();

			bool[,] state = new bool[width, height];
			for (int y = 0; y < height; ++y) {
				for (int x = 0; x < width; ++x) {
					state[x, y] = initialState[y][x] == '#';
				}
			}

			while (!seenStates.Contains(GetRating(state))) {
				seenStates.Add(GetRating(state));
				state = RunGeneration(state);
			}

			return GetRating(state);
		}

		public static bool[,] RunGeneration(bool[,] state) {
			bool[,] newState = (state.Clone() as bool[,])!;

			for (int y = 0; y < state.GetLength(1); ++y) {
				for (int x = 0; x < state.GetLength(0); ++x) {
					int neighbours = 0;
					if (y > 0 && state[x, y - 1]) ++neighbours;
					if (y < state.GetLength(1) - 1 && state[x, y + 1]) ++neighbours;
					if (x > 0 && state[x - 1, y]) ++neighbours;
					if (x < state.GetLength(0) - 1 && state[x + 1, y]) ++neighbours;
					if (state[x, y] && neighbours != 1) newState[x, y] = false;
					if (!state[x, y] && (neighbours == 1 || neighbours == 2)) newState[x, y] = true;
				}
			}

			return newState;
		}

		public static int GetRating(bool[,] state) {
			int rating = 0;
			for (int i = 0; i < state.Length; ++i) {
				if (state[i % state.GetLength(0), i / state.GetLength(1)]) rating |= (1 << i);
			}
			return rating;
		}
	}
}