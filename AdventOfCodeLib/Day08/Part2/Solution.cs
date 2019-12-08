using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day08.Part2 {
	public static class Solution {
		public static string SolveFromInputFile(string inputFile) {
			var result = Solve(new List<int>(File.ReadAllText(inputFile).Trim().Select(d => int.Parse(new char[] { d }))), 25, 6);
			return string.Join(Environment.NewLine, result.Select(l => string.Join("", l))).Replace('0', ' ').Replace('1', '#');
		}

		public static List<List<int>> Solve(List<int> imagePixels, int width, int height) {
			int layers = imagePixels.Count / (width * height);
			int layerSize = width * height;

			int[,,] image = new int[layers, width, height];
			foreach (int layer in Enumerable.Range(0, layers)) {
				foreach (int x in Enumerable.Range(0, width)) {
					foreach (int y in Enumerable.Range(0, height)) {
						image[layer, x, y] = imagePixels[layer * layerSize + x * height + y];
					}
				}
			}

			int[,] result = new int[width, height];
			foreach (int x in Enumerable.Range(0, width)) {
				foreach (int y in Enumerable.Range(0, height)) {
					result[x, y] = 2;
				}
			}

			foreach (int layer in Enumerable.Range(0, layers)) {
				foreach (int x in Enumerable.Range(0, width)) {
					foreach (int y in Enumerable.Range(0, height)) {
						if (result[x, y] == 2) result[x, y] = image[layer, x, y];
					}
				}
			}

			return result.Cast<int>().Select((x, i) => new { x, index = i / width }).GroupBy(x => x.index).Select(x => x.Select(s => s.x).ToList()).ToList();
		}
	}
}
