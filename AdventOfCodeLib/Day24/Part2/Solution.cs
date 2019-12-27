using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day24.Part2 {
	public static class Solution {

		public static int SolveFromInputFile(string inputFile) {
			return Solve(File.ReadAllLines(inputFile).ToList(), 200);
		}

		public static int Solve(List<string> initialState, int minutes) {
			int width = initialState[0].Length;
			int height = initialState.Count;

			bool[,] startingLayer = new bool[width, height];
			for (int y = 0; y < height; ++y) {
				for (int x = 0; x < width; ++x) {
					startingLayer[x, y] = initialState[y][x] == '#';
				}
			}

			var state = new List<bool[,]>();
			// This overcompensates but it's probably fine.
			for (int i = 0; i < minutes + 1; ++i) {
				if (i == minutes / 2) {
					state.Add(startingLayer);
				} else {
					state.Add(new bool[width, height]);
				}
			}

			for (int i = 0; i < minutes; ++i) {
				state = RunGeneration(state);
			}

			return CountBugs(state);
		}

		public static int CountBugs(List<bool[,]> state) {
			int bugs = 0;
			for (int z = 0; z < state.Count; ++z) {
				for (int y = 0; y < state[z].GetLength(1); ++y) {
					for (int x = 0; x < state[z].GetLength(0); ++x) {
						if (state[z][x, y]) ++bugs;
					}
				}
			}
			return bugs;
		}

		public static List<bool[,]> RunGeneration(List<bool[,]> state) {
			var newState = state.Select(l => (l.Clone() as bool[,])!).ToList();
			int width = state[0].GetLength(0);
			int height = state[1].GetLength(1);
			int middle = width / 2;

			for (int z = 0; z < state.Count; ++z) {
				for (int y = 0; y < height; ++y) {
					for (int x = 0; x < width; ++x) {
						if (x == middle && y == middle) continue;
						int neighbours = 0;
						if (z > 0 && y == 0 && state[z - 1][middle, middle - 1]) ++neighbours;
						if (z > 0 && x == 0 && state[z - 1][middle - 1, middle]) ++neighbours;
						if (z > 0 && y == height - 1 && state[z - 1][middle, middle + 1]) ++neighbours;
						if (z > 0 && x == width - 1 && state[z - 1][middle + 1, middle]) ++neighbours;
						if (z < state.Count - 1 && y == middle - 1 && x == middle) {
							for (int innerX = 0; innerX < width; ++innerX) {
								if (state[z + 1][innerX, 0]) ++neighbours;
							}
						}
						if (z < state.Count - 1 && y == middle + 1 && x == middle) {
							for (int innerX = 0; innerX < width; ++innerX) {
								if (state[z + 1][innerX, height - 1]) ++neighbours;
							}
						}
						if (z < state.Count - 1 && y == middle && x == middle - 1) {
							for (int innerY = 0; innerY < height; ++innerY) {
								if (state[z + 1][0, innerY]) ++neighbours;
							}
						}
						if (z < state.Count - 1 && y == middle&& x == middle + 1) {
							for (int innerY = 0; innerY < width; ++innerY) {
								if (state[z + 1][width - 1, innerY]) ++neighbours;
							}
						}
						if (y > 0 && state[z][x, y - 1]) ++neighbours;
						if (y < height - 1 && state[z][x, y + 1]) ++neighbours;
						if (x > 0 && state[z][x - 1, y]) ++neighbours;
						if (x < width - 1 && state[z][x + 1, y]) ++neighbours;
						if (state[z][x, y] && neighbours != 1) newState[z][x, y] = false;
						if (!state[z][x, y] && (neighbours == 1 || neighbours == 2)) newState[z][x, y] = true;
					}
				}
			}

			return newState;
		}
	}
}
